using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

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

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        InventoryUpdate();
    }

    private void Inventory_OnItemListChanged(object snder,System.EventArgs e)
    {
        InventoryUpdate();
    }

    private void InventoryUpdate()
    {
        foreach (Transform child in itemSlotList)
        {
            if (child==itemSlot)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        int x = 0;
        int y = 0;
        float itemSlotSize = 240.0f;
        foreach (Items item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotList).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x* itemSlotSize, y* itemSlotSize);

            Image image = itemSlotRectTransform.Find("itemImage").GetComponent<Image>();
            image.sprite = item.GetSprite();

            //num of item in inventory
            x++;
            if (x > 4) 
            {
                x = 0;
                y++;
            }
        }
    }






}
