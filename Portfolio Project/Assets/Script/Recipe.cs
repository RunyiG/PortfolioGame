using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Items items;
}

[CreateAssetMenu (fileName = "Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;  

    public bool Craftable(ItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if (itemContainer.ItemCount(itemAmount.items) < itemAmount.items.amount) 
            {
                return false;
            }
        }
        return true;
    }

    public void Craft(ItemContainer itemContainer)
    {
        if (Craftable(itemContainer))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.items.amount; i++)
                {
                    itemContainer.RemoveItem(itemAmount.items);
                }
            }

            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.items.amount; i++)
                {
                    itemContainer.AddItem(itemAmount.items);
                }
            }
        }
    }
}
