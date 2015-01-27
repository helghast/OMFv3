using UnityEngine;
using System.Collections;

public class Comp_ScrollBG : MonoBehaviour
{
    public enum ScrollType
    {
        SKY = 0,
        CLOUDS,
        FAR_LAYER,
        MID_LAYER,
        NEAR_LAYER,

        H_WATER,
        V_WATER,

        FL_WATER,
        ML_WATER,
        BL_WATER,

        FOREGROUND
    }
    public ScrollType scrollType;
    public float SMOOTH_TIME_SCROLL_SPEED;
    public Vector2 scroll_direction;

    private float current_scroll_speed;
    private float target_scroll_speed;
    private Vector2 uv_offset;

    // necesario para el smoothDamp
    private float scroll_speed_velocity;

    private float custom_repeat_value;

    // Use this for initialization
    void Start()
    {
        // We create the vector once instead of creating inside update (Frees GC)
        uv_offset = new Vector2(0, 0);
        scroll_speed_velocity = 0;

        custom_repeat_value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        setScrollSpeed(0);
        float local_target_scroll_speed = target_scroll_speed * GameLogicManager.Instance.GameSpeed;
        current_scroll_speed = Mathf.SmoothDamp(current_scroll_speed, local_target_scroll_speed, ref scroll_speed_velocity, SMOOTH_TIME_SCROLL_SPEED);
        custom_repeat_value = ElectroMaths.CustomUnitRepeat(custom_repeat_value, current_scroll_speed);

        uv_offset.x = custom_repeat_value * scroll_direction.x;
        uv_offset.y = custom_repeat_value * scroll_direction.y;
        renderer.sharedMaterial.SetTextureOffset("_MainTex", uv_offset);
    }
  
    public void setScrollSpeed(float new_scroll_speed)
    {
        switch (scrollType)
        {
            case ScrollType.CLOUDS:
                target_scroll_speed = EnvironmentManager.Instance.Clouds_Speed;
                break;
            case ScrollType.SKY:
                target_scroll_speed = EnvironmentManager.Instance.Sky_Speed;
                break;
            case ScrollType.FAR_LAYER:
                target_scroll_speed = EnvironmentManager.Instance.SpeedLayer2;
                break;
            case ScrollType.MID_LAYER:
                target_scroll_speed = EnvironmentManager.Instance.SpeedLayer3;
                break;
            case ScrollType.NEAR_LAYER:
                target_scroll_speed = EnvironmentManager.Instance.SpeedLayer4;
                break;
            case ScrollType.V_WATER:
                target_scroll_speed = EnvironmentManager.Instance.Water_FLayer_Speed;
                break;
            case ScrollType.H_WATER:
                target_scroll_speed = EnvironmentManager.Instance.Water_MLayer_Speed;
                break;
            case ScrollType.FL_WATER:
                target_scroll_speed = EnvironmentManager.Instance.Water_FLayer_Speed;
                break;
            case ScrollType.ML_WATER:
                target_scroll_speed = EnvironmentManager.Instance.Water_MLayer_Speed;
                break;
            case ScrollType.BL_WATER:
                target_scroll_speed = EnvironmentManager.Instance.Water_BLayer_Speed;
                break;
        }
        
        // if (dash)
        // mult target * dash_multiplier
        //target_scroll_speed = new_scroll_speed;
    }

    public float CustomUnitRepeat(float value, float multiplier)
    {
        value += Time.deltaTime * multiplier;
        if (value > 1)
        {
            value = value - 1;
        }
        return value;
    }
   
}