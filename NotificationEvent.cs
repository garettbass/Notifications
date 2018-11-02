using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class NotificationEvent<TMessage>
: UnityEvent<TMessage>
{ }

public static class NotificationEventExtensions
{

    public static bool Send<TMessage>(
        this NotificationEvent<TMessage> notificationEvent,
        TMessage message)
    {
        var notNull = notificationEvent != null;
        if (notNull) notificationEvent.Invoke(message);
        return notNull;
    }

}