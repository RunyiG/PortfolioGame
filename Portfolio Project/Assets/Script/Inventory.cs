using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Items> itemList;
    public event EventHandler OnItemListChanged;

    public Inventory()
    {
        itemList = new List<Items>();

        AddItem(new Items { itemTypes = Items.ItemTypes.Food_1, amount = 1 });
        AddItem(new Items { itemTypes = Items.ItemTypes.Food_2, amount = 1 });
        AddItem(new Items { itemTypes = Items.ItemTypes.Food_3, amount = 1 });
        AddItem(new Items { itemTypes = Items.ItemTypes.Food_4, amount = 1 });
    }

    public void AddItem(Items items)
    {
        itemList.Add(items);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Items> GetItemList()
    {
        return itemList;
    }
}
