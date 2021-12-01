using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemContainer
{
    void RemoveItem(Items items);
    void AddItems(Items items);
    bool CanAdd();
}
