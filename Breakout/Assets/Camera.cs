using System.Collections;
using System.Collections.Generic;

public static class Camera
{
    private static UnityEngine.Camera main = UnityEngine.Camera.main;
    public static float Height => 2f * main.orthographicSize;
    public static float Width => Height * main.aspect;
}
