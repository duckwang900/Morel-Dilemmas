using UnityEngine;
using System.Collections.Generic;

public class Order
{
    public int tableNum;
    public Recipe recipe;

    public Order(int tableNum, Recipe recipe)
    {
        this.tableNum = tableNum;
        this.recipe = recipe;
    }
}

public class OrderManager : MonoBehaviour
{
    public Dictionary<int, Recipe> currentOrders = new Dictionary<int, Recipe>();

    public void AddOrder(int tableNum, Recipe recipe)
    {
        if (currentOrders.ContainsKey(tableNum) == false)
        {
            currentOrders.Add(tableNum, recipe);
        }
        else
        {
            currentOrders[tableNum] = recipe;
        }

        Debug.Log("Added to orders");
    }

    public void RemoveOrder(int tableNum)
    {
        if (currentOrders.ContainsKey(tableNum) == false)
        {
            Debug.Log("Issue! This table number does not exist yet");
        }
        else
        {
            currentOrders[tableNum] = null;
        }
    }

}
