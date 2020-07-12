using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Message
{

}

public class MessagesBroker : Singleton<MessagesBroker>
{
    private static Dictionary<Message, Delegate> _messageList = new Dictionary<Message, Delegate>();

    protected override void OnSetSingleton()
    {
        DontDestroyOnLoad(this);
    }

    public void AddListener(Message message, Action action)
    {
        AddListenerGeneric(message, action);
    }

    public void AddListener<T>(Message message, Action<T> action)
    {
        AddListenerGeneric(message, action);
    }

    private void AddListenerGeneric(Message message, Delegate action)
    {
        if (_messageList.ContainsKey(message))
        {
            _messageList[message] = Delegate.Combine(_messageList[message], action);
        }
        else
            _messageList.Add(message, action);
    }

    public void RemoveListener(Message message, Action action)
    {
        RemoveListenerDelegate(message, action);
    }

    public void RemoveListener<T>(Message message, Action<T> action)
    {
        RemoveListenerDelegate(message, action);
    }

    private void RemoveListenerDelegate(Message message, Delegate action)
    {
        bool success = false;

        success |= TryRemoveListener(message, action);

        if (!success)
        {
            Debug.LogError("Trying to remove a listener that has not been added to the broker: " + message);
        }
    }

    private bool TryRemoveListener(Message messageType, Delegate handler)
    {
        if (!_messageList.ContainsKey(messageType))
        {
            return false;
        }

        _messageList[messageType] = Delegate.Remove(_messageList[messageType], handler);

        if (_messageList[messageType] == null)
        {
            _messageList.Remove(messageType);
        }

        return true;
    }

    public bool CheckList(Message messageType)
    {
        if (_messageList.ContainsKey(messageType))
        {
            return true;
        }
        else return false;
    }

    public void SendMessage(Message message)
    {
        if (!_messageList.ContainsKey(message))
        {
            Debug.LogWarning("MessagesBroker does not contain message: " + message + ", this might not be a problem if no one is listening for the message.");
            return;
        }

        (_messageList[message] as Action)?.Invoke();
    }

    public void SendMessage<T>(Message message, T param)
    {
        if (!_messageList.ContainsKey(message))
        {
            Debug.LogError("MessagesBroker does not contain message: " + message);
            return;
        }

        (_messageList[message] as Action<T>)?.Invoke(param);
    }
}
