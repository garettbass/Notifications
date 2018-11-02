using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class NotificationReceiver<
    TMessage,
    TNotification,
    TNotificationEvent>
: MonoBehaviour
where TNotification : Notification<TMessage>
where TNotificationEvent : NotificationEvent<TMessage>
{

    public TNotification notification;

    public TNotificationEvent actions;

    private void OnEnable()
    {
        if (notification)
            notification.receivers += OnNotificationReceived;
    }

    private void OnDisable()
    {
        if (notification)
            notification.receivers -= OnNotificationReceived;
    }

    private void OnNotificationReceived(TMessage message)
    {
        var a = actions;
        if (a != null)
            a.Invoke(message);
    }

}