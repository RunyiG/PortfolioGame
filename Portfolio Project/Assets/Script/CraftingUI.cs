using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    [SerializeField]
    private Transform ItemPf;
        
    private Transform[] slotTransformArray;
    private Transform outputslotTransform;
    private Transform itemContainer;
    private CraftingSystem craftingSystem;

    private void Start()
    {
        Transform CraftSlot = transform.Find("CraftSlot");
        itemContainer = transform.Find("ItemContainer");
        slotTransformArray = new Transform[CraftingSystem.SLOT_SIZE];
        for (int i = 0; i < CraftingSystem.SLOT_SIZE; i++)
        {
            slotTransformArray[i] = CraftSlot.Find("RecipeSlot_" + i);
        }
        outputslotTransform = transform.Find("OutputSlot");

        CreatItem(new Items { itemTypes = Items.ItemTypes.Apple }, 0);
        CreatItem(new Items { itemTypes = Items.ItemTypes.Honey }, 1);
        CreatItemOutput(new Items { itemTypes = Items.ItemTypes.HoneyRoastedApples });

    }

    public void SetCraftingSystem(CraftingSystem craftingSystem)
    {
        this.craftingSystem = craftingSystem;

        craftingSystem.OnSlotsChanged += craftingSystem_OnSlotsChanged;
        UpdateCraftUIVisual();
    }

    private void craftingSystem_OnSlotsChanged(object sender,System.EventArgs eventArgs)
    {
        UpdateCraftUIVisual();
    }

    private void CreatItem(Items item, int slot)
    {
        Transform itemTransform = Instantiate(ItemPf, itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = slotTransformArray[slot].GetComponent<RectTransform>().anchoredPosition;
    }

    private void CreatItemOutput(Items item)
    {
        Transform itemTransform = Instantiate(ItemPf, itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = outputslotTransform.GetComponent<RectTransform>().anchoredPosition;
    }

    private void UpdateCraftUIVisual()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < CraftingSystem.SLOT_SIZE; i++)
        {
            if (!craftingSystem.IsEmpty(i))
            {
                CreatItem(craftingSystem.GetItems(i), i);
            }
        }

        if (craftingSystem.GetOutputItems() != null) 
        {
            CreatItemOutput(craftingSystem.GetOutputItems());
        }
    }
}
