using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_Coin_Group : MonoBehaviour
{
    public List<Transform> positions;
    public string name;

	void Awake()
    {
        positions = new List<Transform>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        positions.AddRange(transforms);
        positions.RemoveAt(0);
	}
}
