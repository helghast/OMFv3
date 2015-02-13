using UnityEngine;
using System.Collections;

public class Comp_Camera_AspectRatio : MonoBehaviour
{

    public float _wantedAspectRatio = 1.3333333f;
    static float wantedAspectRatio;
    static Camera cam;
    static Camera backgroundCam;

    void Awake()
    {
        //obtenemos la camara de este gameobject mediante el acceso rapido "camera"
        cam = camera;
        if(!cam)
        {
            cam = Camera.main;
        }
        if(!cam)
        {
            Debug.LogError("No camera avaliable");
            return;
        }
        wantedAspectRatio = _wantedAspectRatio;
        SetCamera();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetCamera()
    {
        float currentAspectRatio = (float) Screen.width / Screen.height;

        //en caso de que el aspect ratio actual sea aproximadamente igual/cercano al deseado aspect ratio usar un Rect fullscreen.
        if((int) (currentAspectRatio * 100) / 100.0f == (int) (wantedAspectRatio * 100) / 100.0f)
        {
            cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            if(backgroundCam)
            {
                Destroy(backgroundCam.gameObject);
            }
        }

        //pillarbox
        if(currentAspectRatio > wantedAspectRatio)
        {
            float inset = 1.0f - wantedAspectRatio / currentAspectRatio;
            cam.rect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
        }
        //letterbox
        else
        {
            float inset = 1.0f - currentAspectRatio / wantedAspectRatio;
            cam.rect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset);
        }

        if(!backgroundCam)
        {
            //crear nueva camara detras de la camara normal que se vea negro.
            backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).camera;
            backgroundCam.depth = int.MinValue;
            backgroundCam.clearFlags = CameraClearFlags.SolidColor;
            backgroundCam.backgroundColor = Color.black;
            backgroundCam.cullingMask = 0;
        }
    }

    public static int screenHeight
    {
        get
        {
            return (int) (Screen.height * cam.rect.height);
        }
    }

    public static int screenWidth
    {
        get
        {
            return (int) (Screen.width * cam.rect.width);
        }
    }

    public static int xOffset
    {
        get
        {
            return (int) (Screen.width * cam.rect.x);
        }
    }

    public static int yOffset
    {
        get
        {
            return (int) (Screen.height * cam.rect.y);
        }
    }

    public static Rect screenRect
    {
        get
        {
            return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width, cam.rect.height * Screen.height);
        }
    }

    public static Vector3 mousePosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.y -= (int) (cam.rect.y * Screen.height);
            mousePos.x -= (int) (cam.rect.x * Screen.width);
            return mousePos;
        }
    }

    public static Vector2 guiMousePosition
    {
        get
        {
            Vector2 mousePos = Event.current.mousePosition;
            mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height, cam.rect.y * Screen.height + cam.rect.height * Screen.height);
            mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width, cam.rect.x * Screen.width + cam.rect.width * Screen.width);
            return mousePos;
        }
    }
}
