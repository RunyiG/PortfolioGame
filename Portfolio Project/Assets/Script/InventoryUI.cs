using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private PlayerMovement player;

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

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    private void Inventory_OnItemListChanged(object snder, System.EventArgs e)
    {
        InventoryUpdate();
    }

    private void InventoryUpdate()
    {
        foreach (Transform child in itemSlotList)
        {
            if (child == itemSlot)
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
            //Use item
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                inventory.UseItem(item);
            };
            //Drop item
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                Items duplicateItem = new Items { itemTypes = item.itemTypes, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.transform.position,duplicateItem);//
            };


            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSize, y * itemSlotSize);
            Image image = itemSlotRectTransform.Find("itemImage").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI itemText = itemSlotRectTransform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                itemText.SetText(item.amount.ToString());
            }
            else
            {
                itemText.SetText(" ");
            }

            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }






}
