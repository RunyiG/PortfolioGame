using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem 
{
    public const int SLOT_SIZE = 2;
    private Items[] itemArray;

    public CraftingSystem()
    {
        itemArray = new Items[SLOT_SIZE];
    }

    private bool IsEmpty(int slot)
    {
        return itemArray[slot] == null;
    }

    private Items GetItems(int slot)
    {
        return itemArray[slot];
    }

    private void SetItem(Items item, int slot)
    {
        itemArray[slot] = item;
    }

    private void IncreaseItemAmount(int slot)
    {
        GetItems(slot).amount++;
    }

    private void DecreaseItemAmount(int slot)
    {
        GetItems(slot).amount--;
    }

    private void RemoveItem(int slot)
    {
        SetItem(null, slot);
    }

    private bool AbletoAddItem(Items item, int slot)
    {
        if (IsEmpty(slot))
        {
            SetItem(item, slot);
            return true;
        }
        else
        {
            if (item.itemTypes==GetItems(slot).itemTypes)
            {
                IncreaseItemAmount(slot);
                return true;
            }
            else
            {
                return false;
            }
        }
    }




}

