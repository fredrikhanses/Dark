using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extentions
{
    /// <summary>
    /// Returns the angle between two vectors in radians.
    /// </summary>
    public static float V2Atan2(Vector2 v2a, Vector2 v2b)
    {
        float value = Mathf.Abs(Mathf.Atan2(v2a.y, v2a.x) * Mathf.Rad2Deg - Mathf.Atan2(v2b.y, v2b.x) * Mathf.Rad2Deg);

        if (value > 180)
        {
            return (180 - value) + 180;
        }

        return value;
    }
}
