using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraManager
{
    //-----------------------------------------------
    //  STATIC ATRIBUTES
    //-----------------------------------------------
    private static CameraManager instance;
    public static CameraManager Instance
    {
        get
        {
            return instance;
        }
    }

    //-----------------------------------------------
    //  ATRIBUTES
    //-----------------------------------------------
    private Comp_Camera comp_camera;
    // Array de planos que conforman el VF de la camara actual
    private Plane[] current_vf_planes;


    //-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
    private CameraManager()
    {

    }

    public static void CreateManager()
    {
        if (instance == null)
        {
            instance = new CameraManager();
            Instance.Initialize();
        }
    }

    public void Initialize()
    {
      
    }


    //-----------------------------------------------
    //  METHODS
    //-----------------------------------------------
    public void SetCompCamera(Comp_Camera _comp_camera)
    {
        comp_camera = _comp_camera;
    }

    // Recalcula los planos del VF y los guarda en una variable de clase. 
    // Esta función solo debería ser llamada una única vez por frame. El volumen de visión que devuelve será constante en todo el Frame.
    public void RecalculateFrustumPlanes(Camera camera)
    {
        current_vf_planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public Plane[] GetViewFrustumPlanes()
    {
        return current_vf_planes;
    }

    //-----------------------------------------------
    //  PROPERTIES
    //-----------------------------------------------
    public float X_Pos_Camera_Offset
    {
        get
        {
            return comp_camera.PLAYER_SCREEN_POSITION;
        }
    }
}
