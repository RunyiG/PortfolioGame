using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{ 
    private Items item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro tmp;

    public static ItemWorld SpawnItems(Items items,Vector3 pos)
    {
        Transform transform = Instantiate(ItemAssets.itemAssets.ItemWorldPf, pos, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(items);
        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPos, Items item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemworld = SpawnItems(item, dropPos + randomDir * 5f);
        Vector3 dropposition  = dropPos + Vector3.right;
        itemworld.transform.position = dropposition;
        return itemworld;
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tmp = transform.Find("Text (TMP)").GetComponent<TextMeshPro>();
    }

    public void SetItem(Items item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1) 
        {
            tmp.SetText(item.amount.ToString());
        }
        else
        {
            tmp.SetText(" ");
        }
    }

    public Items GetItems()
    {
        return item;
    }

    public void Destory()
    {
        Destroy(gameObject);
    }
}
