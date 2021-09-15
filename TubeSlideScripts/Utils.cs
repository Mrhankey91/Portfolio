using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static string FloatToTimeString(this float f)
    {
        return "" + f.ToString("0.00");
    }

}
