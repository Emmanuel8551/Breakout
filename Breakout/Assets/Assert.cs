using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Assert
{
    public static void Equals (bool value, string message)
    {
        if (value == false) Debug.LogError(message);
    }

    public static void Equals (bool value)
    {
        Equals(value, "Error");
    }
}
