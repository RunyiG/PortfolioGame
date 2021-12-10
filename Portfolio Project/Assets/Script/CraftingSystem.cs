using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : ItemContainer
{
    public event EventHandler OnSlotsChanged;

    public const int SLOT_SIZE = 2;

    //private Items.ItemTypes[] HoneyApple;
    private Dictionary<Items.ItemTypes, Items.ItemTypes[]> recipeDictionary;

    private Items[] itemArray;
    private Items ItemOutput;


    public CraftingSystem()
    {
        itemArray = new Items[SLOT_SIZE];

        recipeDictionary = new Dictionary<Items.ItemTypes, Items.ItemTypes[]>();

        Items.ItemTypes[] recipe = new Items.ItemTypes[SLOT_SIZE];
        recipe[0] = Items.ItemTypes.Apple;
        recipe[1] = Items.ItemTypes.Honey;
        recipeDictionary[Items.ItemTypes.HoneyRoastedApples] = recipe;

        recipe = new Items.ItemTypes[SLOT_SIZE];
        recipe[0] = Items.ItemTypes.Apple;
        recipe[1] = Items.ItemTypes.Orange;
        recipeDictionary[Items.ItemTypes.AppleOrange] = recipe;

        recipe = new Items.ItemTypes[SLOT_SIZE];
        recipe[0] = Items.ItemTypes.Honey;
        recipe[1] = Items.ItemTypes.Orange;
        recipeDictionary[Items.ItemTypes.OrangeHoney] = recipe;

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
        if (item != null) 
        {
            item.RemoveFromItemContainer();
            item.SetItemContainer(this);
        }
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
        if (GetItems(slot) != null) 
        {
            GetItems(slot).amount--;
            if (GetItems(slot).amount == 0) 
            {
                RemoveItem(slot);
            }

            OnSlotsChanged?.Invoke(this, EventArgs.Empty);
        }
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
            if (item.itemTypes == GetItems(slot).itemTypes)
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
        foreach (Items.ItemTypes recipes in recipeDictionary.Keys)
        {
            Items.ItemTypes[] recipe = recipeDictionary[recipes];
            bool correctRecipe = true;
            for (int i = 0; i < SLOT_SIZE; i++)
            {
                if (recipe[i] != Items.ItemTypes.N)
                {
                    if (IsEmpty(i) || GetItems(i).itemTypes != recipe[i])
                    {
                        correctRecipe = false;
                    }
                }
            }
            if (correctRecipe) 
            {
                
                return recipes;
            }
        }

        return Items.ItemTypes.N;
    }

    private void CreateOutput()
    {
        Items.ItemTypes recipeOutput = GetRecipeOutput();
        if (recipeOutput == Items.ItemTypes.N)
        {
            ItemOutput = null;
        }
        else
        {
            ItemOutput = new Items { itemTypes = recipeOutput };
            ItemOutput.SetItemContainer(this);
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
            OnSlotsChanged?.Invoke(this, EventArgs.Empty);
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

    public void AddItems(Items items)
    {
    }

    public bool CanAdd()
    {
        return false;
    }
}

