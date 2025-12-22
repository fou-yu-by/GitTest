using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine.Events;




public class EventManager:SingleMonoBase<EventManager>
{
    private Dictionary<string, EventHandler> handlerDic = new Dictionary<string, EventHandler>();


    /// <summary>
    /// 添加一个事件监听
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="handler"></param>
    public void AddListener(string eventName, EventHandler handler)
    {
        if (handlerDic.ContainsKey(eventName))
        {
            handlerDic[eventName] += handler;
        }
        else
        {
            handlerDic.Add(eventName, handler);
        }
    }

    public void RemoveListener(string eventName, EventHandler handler)
    {
        if (handlerDic.ContainsKey(eventName))
        {
            handlerDic[eventName] -= handler;
        }
    }


    /// <summary>
    /// 触发事件(无参数)
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="sender"></param>
    public void TriggerEvent(string eventName, Object sender)
    {
        if (handlerDic.ContainsKey(eventName))
        {
            handlerDic[(eventName)].Invoke(sender, EventArgs.Empty);
        }
    }




    /// <summary>
    /// 触发事件(有参数)
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void TriggerEvent(string eventName,Object sender, EventArgs args)
    {
        if (handlerDic.ContainsKey(eventName))
        {
            handlerDic[(eventName)].Invoke(sender, args);
        }
    }

    public void Clear()
    {
        handlerDic.Clear();
    }
  
}

/// <summary>
/// 便于触发事件的拓展类
/// </summary>
public static class EventTriggerExt
{

    /// <summary>
    /// 触发事件(无参数)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    public static void TriggerEvent(this Object sender,string eventName)
    {
        EventManager.Instance.TriggerEvent(eventName,sender);
    }


    /// <summary>
    /// 触发事件(有参数)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public static void TriggerEvent(this Object sender,string eventName,EventArgs args)
    {
        EventManager.Instance.TriggerEvent(eventName, sender, args);
    }


}









