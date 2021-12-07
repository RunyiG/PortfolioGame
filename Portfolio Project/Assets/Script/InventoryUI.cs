using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    private PlayerMovement player;
    public CraftingUI craftUI;

    private Transform itemSlotList;
    private Transform itemSlot;

    [SerializeField]
    private Transform ItemPf;

    // private Transform CoinUI;
    [SerializeField]
    public TMP_Text CoinText;

    private void Awake()
    {
        itemSlotList = transform.Find("ItemSlotList");
        itemSlot = itemSlotList.Find("ItemSlot");
        itemSlot.gameObject.SetActive(false);
        itemSlot.GetComponent<ItemSlotUI>().OnItemDropped += Inventory_OnItemListChanged;
        // CoinUI = transform.Find("CoinUI");
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

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
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
        float itemSlotgapSize = 240.0f;
        foreach (Inventory.InventorySlot inventorySlot in inventory.GetInventorySlotArray())
        {
            Items item = inventorySlot.GetItem();
            //int i = 0;
            Transform slot = Instantiate(itemSlot, itemSlotList);
            RectTransform itemSlotRectTransform = slot.GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            //itemSlotRectTransform.transform.parent = itemSlot.transform;

            //Sell item
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                Items duplicateItem = new Items { itemTypes = item.itemTypes, amount = item.amount };
                TrySellItem(item.itemTypes, duplicateItem);
                inventory.RemoveItem(item);
            };

            //Drop item
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Items duplicateItem = new Items { itemTypes = item.itemTypes, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.transform.position, duplicateItem);
            };


            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotgapSize, -y * itemSlotgapSize);

            if (!inventorySlot.IsEmpty())
            {
                // Have Items
                Transform uiItemTransform = Instantiate(ItemPf, itemSlotList);
                uiItemTransform.GetComponent<RectTransform>().anchoredPosition = itemSlotRectTransform.anchoredPosition;
                ItemUI itemUI = uiItemTransform.GetComponent<ItemUI>();
                itemUI.SetItem(item);
            }

            Inventory.InventorySlot tmpInventorySlot = inventorySlot;

            ItemSlotUI itemSlotUI = itemSlotRectTransform.GetComponent<ItemSlotUI>();
            itemSlotUI.SetOnDropAction(() =>
            {
                // Dropped on this UI Item Slot
                Items draggedItem = ItemDragUI.Instance.GetItem();
                draggedItem.RemoveFromItemContainer();
                inventory.AddItem(draggedItem, tmpInventorySlot);
            });

            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }

    private void TrySellItem(Items.ItemTypes itemType, Items item)
    {
        if (itemType.Equals(Items.ItemTypes.Apple))
        {
            inventory.coin = inventory.coin + Items.GetSellPrice(Items.ItemTypes.Apple) * item.amount;
        }
        else if (itemType.Equals(Items.ItemTypes.Orange))
        {
            inventory.coin = inventory.coin + Items.GetSellPrice(Items.ItemTypes.Orange) * item.amount;
        }
        if (itemType.Equals(Items.ItemTypes.Honey))
        {
            inventory.coin = inventory.coin + Items.GetSellPrice(Items.ItemTypes.Honey) * item.amount;
        }
        else if (itemType.Equals(Items.ItemTypes.Ice))
        {
            inventory.coin = inventory.coin + Items.GetSellPrice(Items.ItemTypes.Ice) * item.amount;
        }
        else if (itemType.Equals(Items.ItemTypes.HoneyRoastedApples))
        {
            inventory.coin = inventory.coin + Items.GetSellPrice(Items.ItemTypes.HoneyRoastedApples) * item.amount;
        }
        CoinText.text = inventory.coin.ToString();
    }
}
