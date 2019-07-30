using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject tilePrefab;

    public Tile[,] grid;

    private int worldWidth;
    private int worldHeight;

    public void GenerateWorld(int width, int height)
    {
        worldWidth = width;
        worldHeight = height;
        grid = new Tile[width, height];
        Tile home = null;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                GameObject obj = Instantiate(tilePrefab, new Vector3(x * 1.8f - (width/2*1.8f), y * 1.8f - (height / 2 * 1.8f), 0), Quaternion.identity) as GameObject;
                Tile tile = obj.GetComponent<Tile>();
                tile.position = new Vector2Int(x, y);

                if (x == width/2 && y == height/2)
                {
                    tile.eastNeighbour = true;
                    tile.westNeighbour = true;
                    tile.northNeighbour = true;
                    tile.southNeighbour = true;
                    tile.type = TileData.Type.Home;
                    tile.building = BuildingData.instance.GetBuilding(BuildingData.Type.Home);

                    grid[x - 1, y].eastNeighbour = true;
                    grid[x - 1, y].Init();

                    grid[x, y - 1].northNeighbour = true;
                    grid[x, y - 1].Init();
                    home = tile;
                    Player.instance.homeTile = tile;
                }
                else
                {
                    float rand = Random.Range(0f, 100f);
                    bool added = false;
                    if (x < width-1 && rand < 50) { tile.eastNeighbour = added = true; }
              

                    rand = Random.Range(0f, 100f);
                    if ((added && y < height-1 && rand < 30) || (y < height - 1 )) { tile.northNeighbour = true; }

                    if (x > 0)
                    {
                        rand = Random.Range(0f, 100f);
                        if (grid[x - 1, y].eastNeighbour)
                        {
                            tile.westNeighbour = true;
                        }
                        else if (rand < 10)
                        {
                            tile.westNeighbour = true;
                            grid[x - 1, y].eastNeighbour = true;
                            grid[x - 1, y].Init();
                        }
                    }

                    if (y > 0)
                    {
                        rand = Random.Range(0f, 100f);
                        if (grid[x, y - 1].northNeighbour)
                        {
                            tile.southNeighbour = true;
                        }
                        else if (rand < 10)
                        {
                            tile.southNeighbour = true;
                            grid[x, y - 1].northNeighbour = true;
                            grid[x, y - 1].Init();
                        }
                    }

                    tile.type = GetTileType(x, y);
                }

                float seed = Random.Range(0f, 10000f);
                float biome = Mathf.PerlinNoise(x/10f + seed, y/10f + seed);
                if (biome < 0.25f)
                {
                    tile.biome = TileData.Biome.Desert;
                }else if(biome < 0.35f)
                {
                    tile.biome = TileData.Biome.Grass;
                }
                else if (biome < 0.5f)
                {
                    tile.biome = TileData.Biome.Forest;
                }
                else if (biome < 0.6f)
                {
                    tile.biome = TileData.Biome.Grass;
                }
                else if (biome < 0.8f)
                {
                    tile.biome = TileData.Biome.Snow;
                }

                if(tile.type == TileData.Type.Resources)
                {
                    tile.resources = GetTileResources(tile.biome);
                }
                tile.Init();
                grid[x, y] = tile;
                tile.transform.parent = transform;
                tile.name = "Tile_" + x + "_" + y;
            }
        }
        GameObject.Find("Controller").GetComponent<TileController>().SelectTile(home);
    }

    private TileData.Type GetTileType(int x, int y)
    {
        float rand = Random.Range(0f, 100f);
        if(rand < 5)//TOWN
        {
            if((x > 0 && grid[x - 1, y].type != TileData.Type.Town) && 
                //(x < worldWidth - 1 && grid[x + 1, y].type != TileData.Type.Town) &&
                (y > 0 && grid[x, y - 1].type != TileData.Type.Town))// &&
                //(y < worldHeight + 1 && grid[x, y + 1].type != TileData.Type.Town))
            {
                return TileData.Type.Town;
            }
            else
            {
                return TileData.Type.Resources;
            }
            
        }
        else if(rand < 75)
        {
            return TileData.Type.Resources;
        }
        return TileData.Type.None;
    }

    private List<Resource> GetTileResources(TileData.Biome biome)
    {
        List<Resource> resources = new List<Resource>();
        List<Resource> biomeResources = ResourceData.instance.GetBiomeResources(biome);

        foreach(Resource r in biomeResources)
        {
            float rand = Random.Range(0f, 100f);
            if (rand < 40)
            {
                resources.Add(r);
            }
        }

        return resources;
    }
}
