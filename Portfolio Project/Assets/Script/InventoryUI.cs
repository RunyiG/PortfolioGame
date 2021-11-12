using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;

    private Transform itemSlotList;
    private Transform itemSlot;

    private void Awake()
    {
        itemSlotList = transform.Find("ItemSlotList");
        itemSlot = itemSlotList.Find("ItemSlot");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        InventoryUpdate();
    }

    private void InventoryUpdate()
    {
        int x = 0;
        int y = 0;
        float itemSlotSize = 240.0f;
        foreach (Items item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotList).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x* itemSlotSize, y* itemSlotSize);
            //num of item in inventory
            x++;
            if (x > 6) 
            {
                x = 0;
                y++;
            }
        }
    }






}
