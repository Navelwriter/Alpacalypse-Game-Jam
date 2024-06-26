using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallBack();
public delegate void CallBack<T>(T arg);
public delegate void CallBack<T, X>(T arg1, X arg2);
public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);
public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
public delegate void CallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);

public class EventBus
{
    private static Dictionary<EventTypes, Delegate> m_EventTable = new Dictionary<EventTypes, Delegate>();

    private static void OnListenerAdding(EventTypes eventType, Delegate callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }

        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("Attempting to add a different type of delegate for event {0}. The current delegate associated with the event is {1}, the type of delegate to add is {2}", eventType,
                d.GetType(), callBack.GetType()));
        }
    }

    private static void OnListenerRemoving(EventTypes eventType, Delegate callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("Removal of listener error: Event {0} has no corresponding delegate", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("Removal of listener error: Trying to remove a different type of delegate for event {0}. The current delegate type is {1}, the type of delegate to remove is {2}", eventType,
                    d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("Removal of listener error: No event code {0}", eventType));
        }
    }

    private static void OnListenerRemoved(EventTypes eventType)
    {
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }

    //no parameters
    public static void AddListener(EventTypes eventType, CallBack callBack)
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;
    }

    //Single parameter
    public static void AddListener<T>(EventTypes eventType, CallBack<T> callBack)
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;
    }

    //two parameters
    public static void AddListener<T, X>(EventTypes eventType, CallBack<T, X> callBack)
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X>)m_EventTable[eventType] + callBack;
    }

    //three parameters
    public static void AddListener<T, X, Y>(EventTypes eventType, CallBack<T, X, Y> callBack)
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y>)m_EventTable[eventType] + callBack;
    }

    //four parameters
    public static void AddListener<T, X, Y, Z>(EventTypes eventType, CallBack<T, X, Y, Z> callBack)
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z>)m_EventTable[eventType] + callBack;
    }

    //five parameters
    public static void AddListener<T, X, Y, Z, W>(EventTypes eventType, CallBack<T, X, Y, Z, W> callBack)
    {
        OnListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z, W>)m_EventTable[eventType] + callBack;
    }

    //no parameters
    public static void RemoveListener(EventTypes eventType, CallBack callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    //single parameter
    public static void RemoveListener<T>(EventTypes eventType, CallBack<T> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    //two parameters
    public static void RemoveListener<T, X>(EventTypes eventType, CallBack<T, X> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    //three parameters
    public static void RemoveListener<T, X, Y>(EventTypes eventType, CallBack<T, X, Y> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    //four parameters
    public static void RemoveListener<T, X, Y, Z>(EventTypes eventType, CallBack<T, X, Y, Z> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    //five parameters
    public static void RemoveListener<T, X, Y, Z, W>(EventTypes eventType, CallBack<T, X, Y, Z, W> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T, X, Y, Z, W>)m_EventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }


    //no parameters
    public static void Broadcast(EventTypes eventType)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack callBack = d as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("Broadcast event error: Event {0} corresponds to a delegate of a different type", eventType));
            }
        }
    }

    //single parameter
    public static void Broadcast<T>(EventTypes eventType, T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("Broadcast event error: Event {0} corresponds to a delegate of a different type", eventType));
            }
        }
    }

    //two parameters
    public static void Broadcast<T, X>(EventTypes eventType, T arg1, X arg2)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X> callBack = d as CallBack<T, X>;
            if (callBack != null)
            {
                callBack(arg1, arg2);
            }
            else
            {
                throw new Exception(string.Format("Broadcast event error: Event {0} corresponds to a delegate of a different type", eventType));
            }
        }
    }

    //three parameters
    public static void Broadcast<T, X, Y>(EventTypes eventType, T arg1, X arg2, Y arg3)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y> callBack = d as CallBack<T, X, Y>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3);
            }
            else
            {
                throw new Exception(string.Format("Broadcast event error: Event {0} corresponds to a delegate of a different type", eventType));
            }
        }
    }

    //four parameters
    public static void Broadcast<T, X, Y, Z>(EventTypes eventType, T arg1, X arg2, Y arg3, Z arg4)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y, Z> callBack = d as CallBack<T, X, Y, Z>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new Exception(string.Format("Broadcast event error: Event {0} corresponds to a delegate of a different type", eventType));
            }
        }
    }

    //five parameters
    public static void Broadcast<T, X, Y, Z, W>(EventTypes eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T, X, Y, Z, W> callBack = d as CallBack<T, X, Y, Z, W>;
            if (callBack != null)
            {
                callBack(arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
                throw new Exception(string.Format("Broadcast event error: Event {0} corresponds to a delegate of a different type", eventType));
            }
        }
    }
}
