using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PostData : MonoBehaviour
{
    private string message;
    private Action<string> callback;

    public string Message
    {
        get { return message; }
        set { message = value; }
    }

    public Action<string> Callback
    {
        get { return callback; }
        set { callback = value; }
    }

    public PostData(string message, Action<string> callback)
    {
        // TODO: Complete member initialization
        this.message = message;
        this.callback = callback;
    }
}
