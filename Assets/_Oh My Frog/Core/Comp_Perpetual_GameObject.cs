using UnityEngine;
using System.Collections;

public class Comp_Perpetual_GameObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
