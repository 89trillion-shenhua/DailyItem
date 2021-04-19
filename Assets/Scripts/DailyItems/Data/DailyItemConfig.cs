using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public struct DailyProduct
{
    public int productId;
    public int type;
    public int subType;
    public int num;
    public int costGold;
    public int costGem;
    public int isPurchased;
}

public class DailyItemConfig
{
    public List<DailyProduct> DailyProducts;
    public int dailyProductCountDown;

    public DailyItemConfig ()
    {
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        var jsonfile = Resources.Load<TextAsset>("Json/data");
        JSONNode jsondata = JSONNode.Parse(jsonfile.text);

        this.DailyProducts = new List<DailyProduct>();
        JSONArray dataArray = jsondata["dailyProduct"].AsArray;
        foreach (JSONNode node in dataArray)
        {
            DailyProduct dailyProduct = new DailyProduct();
            dailyProduct.productId = node["productId"].AsInt;
            dailyProduct.type = node["type"].AsInt;
            dailyProduct.subType = node["subType"].AsInt;
            dailyProduct.num = node["num"].AsInt;
            dailyProduct.costGold = node["costGold"].AsInt;
            dailyProduct.costGem = node["costGem"].AsInt;
            dailyProduct.isPurchased = node["isPurchased"].AsInt;
            this.DailyProducts.Add(dailyProduct);
        }

        this.dailyProductCountDown = jsondata["dailyProductCountDown"];
    }
}