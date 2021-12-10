using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Items 
{
    public enum ItemTypes
    {
        N,
        Honey,
        Apple,
        Orange,
        Ice,
        HoneyRoastedApples,
        AppleOrange,
        OrangeHoney,
    }

    public ItemTypes itemTypes;

    [Range(1,99)]
    public int amount = 1;

    private ItemContainer itemContainer;


    public void SetItemContainer(ItemContainer itemContainer)
    {
        this.itemContainer = itemContainer;
    }

    public ItemContainer GetItemContainer()
    {
        return itemContainer;
    }

    public void RemoveFromItemContainer()
    {
        if (itemContainer != null)
        {
            // Remove from current Item container
            itemContainer.RemoveItem(this);
        }
    }

    public void MoveToAnotherItemContainer(ItemContainer newItemContainer)
    {
        RemoveFromItemContainer();
        // Add to new Item container
        newItemContainer.AddItems(this);
    }


    public Sprite GetSprite()
    {
        return GetSprite(itemTypes);
    }

    public static Sprite GetSprite(ItemTypes itemTypes)
    {
        switch (itemTypes)
        {
            default:
            case ItemTypes.Honey:
                return ItemAssets.itemAssets.HoneySprite;
            case ItemTypes.Apple:
                return ItemAssets.itemAssets.AppleSprite;
            case ItemTypes.Orange:
                return ItemAssets.itemAssets.OrangeSprite;
            case ItemTypes.Ice:
                return ItemAssets.itemAssets.IceSprite;
            case ItemTypes.HoneyRoastedApples:
                return ItemAssets.itemAssets.HoneyRAppleSpr;
            case ItemTypes.AppleOrange:
                return ItemAssets.itemAssets.AppleOrange;
            case ItemTypes.OrangeHoney:
                return ItemAssets.itemAssets.OrangeHoney;
        }
    }


    public bool IsStackable(ItemTypes itemType)
    {
        switch (itemTypes)
        {
            default:
            case ItemTypes.Apple:
            case ItemTypes.Orange:
            case ItemTypes.Ice:
                return true;
            case ItemTypes.Honey:
            case ItemTypes.HoneyRoastedApples:
            case ItemTypes.AppleOrange:
            case ItemTypes.OrangeHoney:
                return false;               
        }
    }

    public bool IsStackable()
    {
        return IsStackable(itemTypes);
    }

    public override string ToString()
    {
        return itemTypes.ToString();
    }

    public static int GetPrice(ItemTypes itemTypes)
    {
        switch (itemTypes)
        {
            default:
            case ItemTypes.N:
                return 0;
            case ItemTypes.Honey:
                return 5;
            case ItemTypes.Apple:
                return 3;
            case ItemTypes.Orange:
                return 3;
            case ItemTypes.Ice:
                return 7;
            case ItemTypes.HoneyRoastedApples:
                return 10;
            case ItemTypes.AppleOrange:
                return 6;
            case ItemTypes.OrangeHoney:
                return 10;
        }
    }

    public static int GetSellPrice(ItemTypes itemTypes)
    {
        switch (itemTypes)
        {
            default:
            case ItemTypes.N:
                return 0;
            case ItemTypes.Honey:
                return 4;
            case ItemTypes.Apple:
                return 2;
            case ItemTypes.Orange:
                return 2;
            case ItemTypes.Ice:
                return 4;
            case ItemTypes.HoneyRoastedApples:
                return 10;
            case ItemTypes.AppleOrange:
                return 6;
            case ItemTypes.OrangeHoney:
                return 10;
        }
    }
}
