using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using Common;

public class ClientManager : BaseManager {

    private const string IP = "127.0.0.1";
    private const int PORT = 6688;

    private Socket clientSocket;
    private Message msg = new Message();

    public ClientManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        base.OnInit();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            clientSocket.BeginReceive(msg.Date, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法连接到服务器端，请检查网络。" + e);
        }
    }

    //从服务端接受消息并处理
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false)
                return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessMessage);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        clientSocket.BeginReceive(msg.Date, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
    }

    private void OnProcessMessage(ActionCode actionCode, string data)
    {
        GameFacade.Instance.HandleReponse(actionCode, data);
    }

    //发送消息给服务器
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
        //clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, null, null);
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭跟服务器端的连接" + e);
        }
    }

}
