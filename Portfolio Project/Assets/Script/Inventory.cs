using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: ItemContainer, IShop
{
    private List<Items> itemList;
    public Action<Items> useItemAction;
    public InventorySlot[] inventorySlotArray;

    public event EventHandler OnItemListChanged;

    public int MaxSlot { get { return 6; } }

    public Inventory(Action<Items> useItemAction, int slotCount)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Items>();

        inventorySlotArray = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            inventorySlotArray[i] = new InventorySlot(i);
        }

        AddItem(new Items { itemTypes = Items.ItemTypes.Apple, amount = 2 });
    }


    public void AddItems(Items item)
    {
        itemList.Add(item);
        item.SetItemContainer(this);
        GetEmptyInventorySlot().SetItem(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    // Drag Item to specific Inventory slot
    public void AddItem(Items item, InventorySlot inventorySlot)
    {
        itemList.Add(item);
        item.SetItemContainer(this);
        inventorySlot.SetItem(item);

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool AddItem(Items items)
    {
        if (MaxSlot <= itemList.Count)
        {
            return false;
        }

        if (items.IsStackable())
        {
            bool ItemInInventory = false;
            foreach (Items inventoryItem in itemList)
            {
                if (inventoryItem.itemTypes == items.itemTypes) 
                {
                    inventoryItem.amount += items.amount;
                    ItemInInventory = true;
                }
            }
            if (!ItemInInventory) 
            {
                itemList.Add(items);
                items.SetItemContainer(this);
                GetEmptyInventorySlot().SetItem(items);
            }
        }
        else
        {
            itemList.Add(items);
            items.SetItemContainer(this);
            GetEmptyInventorySlot().SetItem(items);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void RemoveItem(Items item)
    {
        GetInventorySlotWithItem(item).RemoveItem();
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItemAmount(Items.ItemTypes itemType, int amount)
    {
        RemoveItemAmount(new Items { itemTypes = itemType, amount = amount });
    }

    public void RemoveItemAmount(Items items)
    {
        if (items.IsStackable())
        {
            Items ItemInInventory = null;
            foreach (Items inventoryItem in itemList)
            {
                if (inventoryItem.itemTypes == items.itemTypes)
                {
                    inventoryItem.amount -= items.amount;
                    ItemInInventory = inventoryItem;
                }
            }
            if (ItemInInventory != null && ItemInInventory.amount <= 0)  
            {
                GetInventorySlotWithItem(ItemInInventory).RemoveItem();
                itemList.Remove(ItemInInventory);
            }
        }
        else
        {
            GetInventorySlotWithItem(items).RemoveItem();
            itemList.Remove(items);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Items item)
    {
        useItemAction(item);
    }

    public List<Items> GetItemList()
    {
        return itemList;
    }

    public bool CanAdd()
    {
        return GetEmptyInventorySlot() != null;
    }

    public class InventorySlot
    {

        private int index;
        private Items item;

        public InventorySlot(int index)
        {
            this.index = index;
        }

        public Items GetItem()
        {
            return item;
        }

        public void SetItem(Items item)
        {
            this.item = item;
        }

        public void RemoveItem()
        {
            item = null;
        }

        public bool IsEmpty()
        {
            return item == null;
        }

    }

    public InventorySlot GetEmptyInventorySlot()
    {
        foreach (InventorySlot inventorySlot in inventorySlotArray)
        {
            if (inventorySlot.IsEmpty())
            {
                return inventorySlot;
            }
        }
        Debug.LogError("Cannot find an empty InventorySlot!");
        return null;
    }

    public InventorySlot GetInventorySlotWithItem(Items item)
    {
        foreach (InventorySlot inventorySlot in inventorySlotArray)
        {
            if (inventorySlot.GetItem() == item)
            {
                return inventorySlot;
            }
        }
        Debug.LogError("Cannot find Item " + item + " in a InventorySlot!");
        return null;
    }

    public InventorySlot[] GetInventorySlotArray()
    {
        return inventorySlotArray;
    }

    public void BoughtItem(Items.ItemTypes itemTypes)
    {
        Debug.Log("Buy" + itemTypes);
    }

    public void SellItem(Items.ItemTypes itemTypes)
    {
        throw new NotImplementedException();
    }

    public bool TryBuy(int coinAmount)
    {
        throw new NotImplementedException();
    }
}
