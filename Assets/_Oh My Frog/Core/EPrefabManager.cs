using UnityEngine;
using System.Collections;

public static class EPrefabManager
{
    static EPrefabManager()
    {

    }

    public static GameObject LoadPrefab(string path)
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>(path));
        go.SetActive(false);
        return go;
    }

    public static GameObject LoadPrefab(string path, Transform t)
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>(path));
        go.SetActive(false);
        go.transform.parent = t;
        return go;
    }
}
