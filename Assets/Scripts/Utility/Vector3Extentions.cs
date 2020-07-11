using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 SetX(this Vector3 v3, float x)
    {
        v3.x = x;
        return v3;
    }

    public static Vector3 SetY(this Vector3 v3, float y)
    {
        v3.y = y;
        return v3;
    }

    public static Vector3 SetZ(this Vector3 v3, float z)
    {
        v3.z = z;
        return v3;
    }

    /// <summary>
    /// Creates a Vector2 from the X and Z axies.
    /// </summary>
    /// <returns>Vector2(Vector3.X, Vector3.Z)</returns>
    public static Vector2 CreateVector2(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }
}
