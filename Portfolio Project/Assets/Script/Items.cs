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
    }

    public ItemTypes itemTypes;
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
                return false;
        }
    }
}
