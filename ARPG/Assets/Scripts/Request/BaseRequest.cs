using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour {
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;

    protected GameFacade facade;

    protected GameFacade Facade
    {
        get
        {
            if (facade == null)
            {
                facade = GameFacade.Instance;
            }
            return facade;
        }
    }

    public virtual void Awake()
    {
        GameFacade.Instance.AddRequest(actionCode, this);
        facade = GameFacade.Instance;
    }

    protected void SendRequest(string data)
    {
        GameFacade.Instance.SendRequest(requestCode, actionCode, data);
    }

    public virtual void SendRequest() { }
    public virtual void OnResponse(string data)
    {
        Debug.Log("BaseRequest");
    }

    public virtual void OnDestroy()
    {
        if (facade != null)
            facade.RemoveRequest(actionCode);
    }
}
