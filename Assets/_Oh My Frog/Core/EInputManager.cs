using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public static class EInputManager
{
    private static bool isTouchOverUIElement;
    public static bool IsTouchOverUIElement
    {
        get
        {
            return isTouchOverUIElement;
        }
    }

    static EInputManager()
    {
        isTouchOverUIElement = false;
    }

    public static void UpdateInputTouch()
    {
        if (Input.touchCount > 0)
        {
            isTouchOverUIElement = false;
            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && EventSystem.current.currentSelectedGameObject != null)
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                isTouchOverUIElement = true;
            }
        }
    }
}
