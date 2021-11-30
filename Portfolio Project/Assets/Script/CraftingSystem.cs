using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem: ItemContainer
{
    public event EventHandler OnSlotsChanged;

    public const int SLOT_SIZE = 2;
    
    private Items.ItemTypes[] HoneyApple;
    private Dictionary<Items.ItemTypes, Items.ItemTypes[]> recipeDictionary;

    private Items[] itemArray;
    private Items ItemOutput;


    public CraftingSystem()
    {
        itemArray = new Items[SLOT_SIZE];

        recipeDictionary = new Dictionary<Items.ItemTypes, Items.ItemTypes[]>();

        Items.ItemTypes[] recipe = new Items.ItemTypes[SLOT_SIZE];
        HoneyApple[0] = Items.ItemTypes.Apple;
        HoneyApple[1] = Items.ItemTypes.Honey;
        recipeDictionary[Items.ItemTypes.HoneyRoastedApples] = recipe;

        //HoneyApple = new Items.ItemTypes[SLOT_SIZE];
        //HoneyApple[0] = Items.ItemTypes.Honey;      HoneyApple[1] = Items.ItemTypes.Apple;
        //HoneyApple[0] = Items.ItemTypes.Apple;      HoneyApple[1] = Items.ItemTypes.Honey;

    }

    public bool IsEmpty(int slot)
    {
        return itemArray[slot] == null;
    }

    public Items GetItems(int slot)
    {
        return itemArray[slot];
    }

    public void SetItem(Items item, int slot)
    {
        itemArray[slot] = item;
        CreateOutput();
        OnSlotsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseItemAmount(int slot)
    {
        GetItems(slot).amount++;
        OnSlotsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseItemAmount(int slot)
    {
        GetItems(slot).amount--;
        OnSlotsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(int slot)
    {
        SetItem(null, slot);
    }

    public bool AbletoAddItem(Items item, int slot)
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

    private Items.ItemTypes GetRecipeOutput()
    {
        for (int i = 0; i < SLOT_SIZE; i++)
        {
            if (HoneyApple[i]!=Items.ItemTypes.N)
            {
                if (IsEmpty(i) || GetItems(i).itemTypes != HoneyApple[i]) 
                {
                    return Items.ItemTypes.N;
                }
            }
        }
        return Items.ItemTypes.HoneyRoastedApples;
    }

    private void CreateOutput()
    {
        Items.ItemTypes recipeOutput = GetRecipeOutput();
        if (recipeOutput==Items.ItemTypes.N)
        {
            ItemOutput = null;
        }
        else
        {
            ItemOutput = new Items { itemTypes = recipeOutput };
        }
    }

    public Items GetOutputItems()
    {
        return ItemOutput;
    }

    public void UseRecipeItems()
    {
        for (int i = 0; i < SLOT_SIZE; i++)
        {
            DecreaseItemAmount(i);
        }
    }

    public void RemoveItem(Items items)
    {
        if (items == ItemOutput) 
        {
            UseRecipeItems();
            CreateOutput();
        }
        else
        {
            for (int i = 0; i < SLOT_SIZE; i++)
            {
                if (GetItems(i) == items) 
                {
                    RemoveItem(i);
                }
            }
        }
    }

    public void AddItem(Items items)
    {
        throw new NotImplementedException();
    }

    public bool CanAdd()
    {
        throw new NotImplementedException();
    }
}

