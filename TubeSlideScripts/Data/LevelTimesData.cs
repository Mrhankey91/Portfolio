using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelTimesData 
{
    public float[] bestTimes;

    //First Time running only
    public void CreateTimesSave()
    {
        bestTimes = new float[1000];
        for (int i = 0; i < bestTimes.Length; ++i)
            bestTimes[i] = 1000f;
    }
}
