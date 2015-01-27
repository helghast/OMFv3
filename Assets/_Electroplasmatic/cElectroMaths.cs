using UnityEngine;
using System.Collections;

public static class ElectroMaths
{
    public static float CustomUnitRepeat(float value, float multiplier)
    {
        value += Time.deltaTime * multiplier;
        if (value > 1)
        {
            value = value - 1;
        }
        return value;
    }
}
