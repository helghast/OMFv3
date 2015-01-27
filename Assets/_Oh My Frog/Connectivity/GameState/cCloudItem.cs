using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class CloudItem
{
    public bool status;
    public int amount;
    public int level;
    public string local_id;

    public CloudItem()
    {
        status = false;
        amount = 0;
        level = 0;
        local_id = "default_id";
    }
}
