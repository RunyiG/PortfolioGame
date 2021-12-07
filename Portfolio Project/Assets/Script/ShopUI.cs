using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    private Transform shopSlotList;
    private Transform shopSlot;

    public GameObject inventoryObj;
    public  Inventory inventory;

    public int coins;

    private void Awake()
    {
        shopSlotList = transform.Find("ShopSlotList");
        shopSlot = shopSlotList.Find("ShopSlot");
        shopSlot.gameObject.SetActive(true);
        
    }

    private void Start()
    {
        CreateItemShopSlot(Items.ItemTypes.Honey, Items.GetSprite(Items.ItemTypes.Honey), Items.GetPrice(Items.ItemTypes.Honey), 0);
        CreateItemShopSlot(Items.ItemTypes.Ice, Items.GetSprite(Items.ItemTypes.Ice), Items.GetPrice(Items.ItemTypes.Ice), 1);
        inventory = inventoryObj.GetComponent<InventoryUI>().inventory;
    }

    private void CreateItemShopSlot(Items.ItemTypes itemTypes, Sprite itemSprite,int itemCost,int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopSlot, shopSlotList);
        RectTransform shopRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemGap = 130f;
        shopRectTransform.anchoredPosition = new Vector2(0, -shopItemGap * positionIndex);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("Image").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Items duplicateItem = new Items { itemTypes = itemTypes, amount = 1 };
            TryBuyItem(itemTypes, duplicateItem);
            inventory.AddItems(duplicateItem);
        };
    }

    private void TryBuyItem(Items.ItemTypes itemType,Items items)
    {
        if (itemType.Equals(Items.ItemTypes.Honey))
        {
            inventory.coin = inventory.coin - Items.GetPrice(Items.ItemTypes.Honey) * items.amount;
        }
        else if (itemType.Equals(Items.ItemTypes.Ice))
        {
            inventory.coin = inventory.coin - Items.GetPrice(Items.ItemTypes.Ice) * items.amount;
        }
        inventoryObj.GetComponent<InventoryUI>().CoinText.text = inventory.coin.ToString();
        Debug.Log(itemType);
    }

}
