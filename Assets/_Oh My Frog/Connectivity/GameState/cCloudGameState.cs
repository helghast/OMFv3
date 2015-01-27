using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class cCloudGameState 
{
    public int total_mangos;
    public int total_cocktails;
    public int total_frogs;
    public int total_meters;
    public List<CloudItem> list_CloudItems;

    public cCloudGameState()
    {
        list_CloudItems = new List<CloudItem>();
        total_cocktails = 0;
        total_mangos = 0;
        total_meters = 0;
        total_frogs = 0;
    }
}
