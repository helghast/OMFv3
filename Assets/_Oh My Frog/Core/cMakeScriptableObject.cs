using UnityEngine;
using System.Collections;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MakeScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Shop")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ScriptableObject_Shop>();
    }
#endif
}
