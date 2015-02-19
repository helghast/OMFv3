using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Comp_RenderCamOnUI : MonoBehaviour {

    public Texture2D texture;
    public Material screenshotMat;
    public bool repeat = false;
    public Camera kappa_camera;

    void Awake() {
        texture = new Texture2D(Mathf.RoundToInt(kappa_camera.GetScreenHeight()), Mathf.RoundToInt(kappa_camera.GetScreenHeight()), TextureFormat.ARGB32, false);
    }

    // Use this for initialization
    void Start() {
        //kappa_camera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(kappa_camera.gameObject.activeSelf) {
            if(Input.GetKeyDown(KeyCode.F)) {
                StartCoroutine(screenshotFunc());
                Debug.Log("F");
            }
            if(Input.GetKeyDown(KeyCode.R)) {
                repeat = true;
                Debug.Log("R");
            }

            if(repeat) {
                StartCoroutine(screenshotFunc());
            }
        }
    }

    IEnumerator screenshotFunc() {
        yield return new WaitForEndOfFrame();
        texture.ReadPixels(new Rect(0, 0, kappa_camera.GetScreenHeight(), kappa_camera.GetScreenHeight()), 0, 0, false);
        texture.Apply();
        //screenshotMat.mainTexture = texture;
        gameObject.GetComponent<RawImage>().texture = texture;
    }
}
