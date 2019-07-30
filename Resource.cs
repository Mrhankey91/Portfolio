using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Resource
{
    public int id = 0;
    public string name = "";
    [SerializeField]
    public Sprite sprite;
    [SerializeField]
    public ResourceData.Type type;
    [SerializeField]
    public List<TileData.Biome> biomes;

    public List<int> buildingIds = new List<int>();

    public void GetBuildingId()
    {
        foreach(Building building in BuildingData.instance.buildings)
        {
            if(building.buildable && building.resource == this.type)
            {
                buildingIds.Add(building.id);
            }
        }
    }
}
