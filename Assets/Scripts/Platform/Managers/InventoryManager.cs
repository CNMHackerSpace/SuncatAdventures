using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;



public class InventoryManager : MonoBehaviour, IGameManager
{

    private Dictionary<string, int> items;
    public ManagerStatus status { get; private set; }

    public string equippedItem { get; private set; }

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        items = new Dictionary<string, int>();
        status = ManagerStatus.Started;
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in items)
        {
            itemDisplay += $"{item.Key} ({item.Value})";
        }
        Debug.Log(itemDisplay);
    }
    public void AddItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {
            items[name] = 1;
        }

        DisplayItems();
    }
    public List<string> GetItemList()
    {
        List<string> list = new List<string>(items.Keys);
        return list;
    }

    public int GetItemCount(string name)
    {
        int number = 0;
        if (items.ContainsKey(name))
        {
            number = items[name];
        }
        return number;
    }

    public bool EquipItem(string name)
    {
        if (items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            Debug.Log($"Equipped {name}");
            return true;
        }

        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }
}
