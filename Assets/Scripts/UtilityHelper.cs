using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class UtilityHelper
{
    public static Vector3 SinWaveMovement(float period, Vector3 startPos, Vector3 direction, bool elipse = false)
    {
        if(period < Mathf.Epsilon)
        {
            return Vector3.zero;
        }
        float cicle = Time.time / period;
        const float tau = Mathf.PI * 2;

        float rawSinWave = Mathf.Sin(cicle * tau);
        float rawCosWave = Mathf.Cos(cicle * tau);

        float sinWave = (rawSinWave + 1) / 2;
        float cosWave = (rawCosWave + 1) / 2;

        if (!elipse)
        {
            return startPos + direction * sinWave;
        }
        else
        {
            return startPos + new Vector3(direction.x * sinWave, direction.y * cosWave);

        }
    }
}
