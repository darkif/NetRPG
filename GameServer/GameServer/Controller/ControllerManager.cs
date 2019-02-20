using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        private void InitController()
        {
            //根据requestCode把对应处理响应的controller加入的controllerDict
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(RequestCode.None, defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
            controllerDict.Add(RequestCode.Game, new GameController());
        }

        //处理从客户端发来的请求
        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            BaseController controller = null;
            //通过requestCode获得对应处理的controller
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                Console.WriteLine("无法得到[" + requestCode.ToString() + "]所对应的controller,无法处理请求");
                return;
            }

            //将枚举类型的actionCode转换为字符串
            //然后通过反射在controller里找到名为methodName的方法
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[warning]在controller[" + controller.GetType() + "]中没有对应的处理方法:[" + methodName + "]");
            }
            //调用methodName方法
            object[] parameters = new object[] { data, client, server };//方法的参数列表
            object o = mi.Invoke(controller, parameters);    //返回值为object类型

            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }

            //如果o不为空，把结果发送回给客户端
            server.SendResponse(client, actionCode, o as string);
        }

    }
}
