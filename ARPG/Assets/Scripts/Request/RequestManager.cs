using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RequestManager : BaseManager
{
    public RequestManager(GameFacade facade) : base(facade) { }

    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestDict.Add(actionCode, baseRequest);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }

    public void HandleReponse(ActionCode actionCode, string data)
    {
        //Debug.Log(data);
        BaseRequest request = null;
        requestDict.TryGetValue(actionCode, out request);
        if (request == null)
        {
            Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的request类");
        }
        request.OnResponse(data);
    }
}
