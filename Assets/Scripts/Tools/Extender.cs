using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Extender
{
    static public Vector3 SetX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    static public Vector3 SetY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    static public Vector3 SetZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    static public Color SetAlpha(this Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }

    public static Color DarkColor(this Color c, float amount)
    {
        return new Color(c.r - amount, c.g - amount, c.b - amount);
    }

    public static Color LightColor(this Color c, float amount)
    {
        return new Color(c.r + amount, c.g + amount, c.b + amount);
    }
}
