using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelData
{
    [Serializable]
    public class CoinsData
    {
        public float x;
        public float y;
        public float z;

        public CoinsData()
        {

        }

        public CoinsData(float x = 0f, float y = 0f, float z = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 GetWorldPosition()
        {
            return new Vector3(x, y, z);
        }
    }

    [Serializable]
    public class ObstacleData
    {
        public int position;
        public string type;
        public int z;
    }

    public int level;
    public float[] targetTimes;
    public CoinsData[] coins;
    public ObstacleData[] obstacles;

    public LevelData()
    {
        level = 0;
        targetTimes = new float[] { 10f, 15f, 20f };
        coins = new CoinsData[] { new CoinsData(), new CoinsData(), new CoinsData()};
        obstacles = new ObstacleData[0];
    }
}
