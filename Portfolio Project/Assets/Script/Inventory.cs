using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour/*,ItemContainer*/
{
    private List<Items> itemList;
    public event EventHandler OnItemListChanged;

    public int MaxSlot { get { return 6; } }

    public Inventory()
    {
        itemList = new List<Items>();

        AddItem(new Items { itemTypes = Items.ItemTypes.Honey, amount = 1 });
    }

    public bool AddItem(Items items)
    {
        if (MaxSlot <= itemList.Count)
            return false;

        if (items.IsStackable())
        {
            bool ItemInInventory = false;
            foreach (Items inventoryItem in itemList)
            {
                if (inventoryItem.itemTypes == items.itemTypes) 
                {
                    inventoryItem.amount += items.amount;
                    ItemInInventory = true;
                }
            }
            if (!ItemInInventory) 
            {
                itemList.Add(items);
            }
        }
        else
        {
            itemList.Add(items);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public List<Items> GetItemList()
    {
        return itemList;
    }

    public int ItemCount(Items item)
    {
        int number = 0;
        foreach (Items inventoryItem in itemList)
        {
            if (inventoryItem.itemTypes == item.itemTypes)
            {
                number++;
            }
        } 
        return number;
    }

    public bool ComtainItem(Items items)
    {
        foreach (Items inventoryItem in itemList)
        {
            if (inventoryItem.itemTypes == items.itemTypes)
            {              
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Items items)
    {
        foreach (Items inventoryItem in itemList)
        {
            if (inventoryItem.itemTypes == items.itemTypes)
            {
                items.amount = 0;
                return true;
            }
        }
        return false;
    }
}
