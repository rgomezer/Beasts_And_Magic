using UnityEngine;
using System.Collections;

public class Utility
{
    bool isEqual(float a, float b)
    {
        if (a >= b - Mathf.Epsilon && a <= b + Mathf.Epsilon)
            return true;
        else
            return false;
    }
}
