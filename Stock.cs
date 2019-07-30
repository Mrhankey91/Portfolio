using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stock
{
    public Resource resource;
    public int amount = 0;

    public Stock(Resource resource, int amount)
    {
        this.resource = resource;
        this.amount = amount;
    }

    public string GetStockInfo()
    {
        return resource.name + " " + amount;
    }
}

[System.Serializable]
public class Stocks
{
    public List<Stock> stocks = new List<Stock>();

    void Start()
    {
    }

    /// <summary>
    /// Check if resource is in stock if so return list index. If not return -1
    /// </summary>
    /// <param name="resource"></param>
    /// <returns>-1 = not having it in stock</returns>
    public int HasResource(Resource resource)
    {
        for(int i = 0; i < stocks.Count; ++i)
        {
            if(stocks[i].resource == resource)
            {
                return i;
            }
        }

        return -1;
    }

    public void AddResourceToStock(Resource resource, int amount)
    {
        int i = HasResource(resource);
        if(i == -1)
        {
            stocks.Add(new Stock(resource, amount));
        }
        else
        {
            stocks[i].amount += amount;
        }
    }
}
