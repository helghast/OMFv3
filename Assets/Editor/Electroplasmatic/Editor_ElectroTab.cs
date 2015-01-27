using UnityEngine;
using UnityEditor;
using System.Threading;
using System.Collections.Generic;
using System.IO;

enum MENU_STATE { SCENE_TOOLS = 0, SCENE_SELECTOR, EXPORTERS, DATABASE }

public class ElectroTab : EditorWindow
{
    string[] buttons = { "SCENE", "SCENE SELECTOR", "EXPORTERS", "DATABASE" };
    static int selected_menu_button;
    MENU_STATE menu_state;
    string myString = "";
    bool groupEnabled;

    EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
    Vector2 scrollPosition = Vector2.one;

    Mesh a_mesh;
    Material mat;
    private GameObject[] database_objs;
    private List<Texture2D> database_previews;
    static private ElectroTab window;


    // Add menu named "My Window" to the Window menu
    [MenuItem("Electroplasmatic/Electroplasmatic")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        window = (ElectroTab)EditorWindow.GetWindow(typeof(ElectroTab));
        selected_menu_button = -1;
    }

    private Texture2D m_Logo = null;
    void OnEnable()
    {
        m_Logo = (Texture2D)Resources.Load("Electroplasmatic/wideLogo", typeof(Texture2D));
        //m_Logo.Resize(256, 128);
    }

    private void getAllSceneObjects()
    {
        database_objs = GameObject.FindObjectsOfType<GameObject>();
        database_previews = new List<Texture2D>();
        for (int i = 0; i < database_objs.Length; ++i)
        {
            database_previews.Add(AssetPreview.GetAssetPreview(database_objs[i]));
            //database_previews[i] = AssetPreview.GetAssetPreview(database_objs[i]);
            Thread.Sleep(100);
        }
    }

    //--------------------------------------------------------------------------------------------
    // DRAW_MENU_FUNCTIONS
    //--------------------------------------------------------------------------------------------
    /*
     * Dibuja los botones del menu principal en la tool del editor (los botones los coje del array "buttons"
     */
    private void draw_tool_buttons()
    {
        GUILayout.Label("Tools", EditorStyles.boldLabel);

        menu_state = (MENU_STATE)GUILayout.SelectionGrid((int)menu_state, buttons, 2);
        /*
        switch (menu_state)
        {
            case MENU_STATE.DATABASE: getAllSceneObjects();
                break;
        }*/

        //Graphics.DrawMeshNow(a_mesh, Vector3.zero, Quaternion.identity);
    }

    private void draw_build_config()
    {
        GUILayout.Label("Build config", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("  Version", myString);
    }

    private void draw_asset_database()
    {
        if (menu_state != MENU_STATE.DATABASE)
            return;

        GUILayout.BeginVertical();

        for (int i = 0; i < database_objs.Length; ++i)
        {
            GUILayout.Label(database_previews[i]);
        }
    }

    private void drawSceneSelector()
    {
        if (menu_state != MENU_STATE.SCENE_SELECTOR)
            return;

        GUILayout.Space(20);
        GUILayout.Label("Scenes");
        GUILayout.Space(10);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Height(300));
        foreach (EditorBuildSettingsScene sc in scenes)
        {
            //importar el namespace System.IO para usar la class Path.
            string cuteName = Path.GetFileNameWithoutExtension(sc.path);

            if (GUILayout.Button(new GUIContent(cuteName, sc.path), GUILayout.MinHeight(25)))
            {
                //Debug.Log(sc.path);
                EditorApplication.OpenScene(sc.path);
            }
        }
        GUILayout.EndScrollView();
        //EditorGUILayout.EndToggleGroup();
    }

    void OnGUI()
    {
        GUILayout.Label(m_Logo);

        draw_build_config();
        draw_tool_buttons();

        drawSceneSelector();
        draw_asset_database();

        /*
        //EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();*/


    }


}