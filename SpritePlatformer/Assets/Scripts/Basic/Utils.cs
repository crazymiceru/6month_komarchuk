using UnityEngine;

public static class Utils
{
    public static float ClampAround(this float value, float min, float max)
    {
        if (value < min || value > max)
        {
            var l1 = Mathf.Abs(Mathf.Min(min - value, 360 - (min - value)));
            var l2 = Mathf.Abs(Mathf.Min(max - value, 360 - (max - value)));
            if (l1 < l2) value = min; else value = max;
        }
        return value;
    }
}
