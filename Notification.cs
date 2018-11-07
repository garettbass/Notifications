using UnityEngine;

/*
    consider renaming:

    Notification -> MessageChannel, MessageRoute, MessageName, MessageType?
    NotificationReceiver -> MessageReceiver, MessageSubscriber?

    Instead of delegate Receiver, consider IMessageReceiver<T> interface added
    to a List<IMessageReceiver<T>>
 */

public abstract class Notification : ScriptableObject
{
    public abstract void Invoke(object message);
}

public abstract class Notification<TMessage> : Notification
{

    [SerializeField, Multiline(10)]
    private string m_description;

    public delegate void Receiver(TMessage message);

    public event Receiver receivers;

    public void Invoke(TMessage message)
    {
        var r = receivers;
        if (r != null)
            r(message);
    }

    public sealed override void Invoke(object message)
    {
        Invoke((TMessage)message);
    }

}

public static class NotificationExtensions
{

    public static bool Send<TMessage>(
        this Notification<TMessage> notification,
        TMessage message)
    {
        var notNull = notification != null;
        if (notNull) notification.Invoke(message);
        return notNull;
    }

}