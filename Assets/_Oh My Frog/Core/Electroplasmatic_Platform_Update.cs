using UnityEngine;
using System.Collections;

//namespace Electroplasmatic_Platform
//{
    public class Electroplasmatic_Platform_Update : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            EInputManager.UpdateInputTouch();
        }
    }
//}
