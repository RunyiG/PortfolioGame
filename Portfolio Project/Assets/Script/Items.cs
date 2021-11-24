using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Items 
{
    public enum ItemTypes
    {
        Honey,
        Apple,
        Orange,
        Ice,
        HoneyRoastedApples,
    }

    public ItemTypes itemTypes;

    [Range(1,99)]
    public int amount;

    public Sprite GetSprite()
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
        }
    }

    public bool IsStackable()
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
                return false;               
        }
    }
}
