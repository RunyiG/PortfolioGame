using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShop
{
    void BoughtItem(Items.ItemTypes itemTypes);
    void SellItem(Items.ItemTypes itemTypes);
    bool TryBuy(int coinAmount);
}
