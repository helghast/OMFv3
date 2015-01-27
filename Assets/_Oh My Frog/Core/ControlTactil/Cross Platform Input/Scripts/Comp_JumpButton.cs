using UnityEngine;

[RequireComponent(typeof(GUIElement))]
public class Comp_JumpButton : MonoBehaviour
{                  
    public bool pairedWithInputManager;
    public Rect pixelInset;
    private AbstractButton button;
    private GUITexture guiTexture;

    void TypeSpecificOnEnable()
    {
        guiTexture = GetComponent<GUITexture>();
        guiTexture.pixelInset = pixelInset;
    }

    void OnEnable()
    {
        button = ButtonFactory.GetPlatformSpecificButtonImplementation();
        button.Enable("Jump", pairedWithInputManager, GetComponent<GUIElement>().GetScreenRect());

        TypeSpecificOnEnable();
    }

    void OnDisable()
    {
        // remove the button from the cross platform input manager
        button.Disable();
    }

    void Update()
    {
        button.Update();
    }
}
