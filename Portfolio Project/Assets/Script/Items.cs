using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items 
{
    public enum ItemTypes
    {
        Food_1,
        Food_2,
        Food_3,
        Food_4,
    }

    public ItemTypes itemTypes;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemTypes)
        {
            default:
            case ItemTypes.Food_1:
                return ItemAssets.itemAssets.HoneySprite;
            case ItemTypes.Food_2:
                return ItemAssets.itemAssets.AppleSprite;
            case ItemTypes.Food_3:
                return ItemAssets.itemAssets.OrangeSprite;
            case ItemTypes.Food_4:
                return ItemAssets.itemAssets.IceSprite;
        }
    }
}
