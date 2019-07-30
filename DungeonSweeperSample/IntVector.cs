using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IntVector2
{
    public int x;
    public int y;

    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static bool operator ==(IntVector2 i1, IntVector2 i2) 
    {
        return i1.Equals(i2);
    }

    public static bool operator !=(IntVector2 i1, IntVector2 i2) 
    {
       return !i1.Equals(i2);
    }
}
