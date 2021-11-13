using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItems(Items items,Vector3 pos)
    {
        Transform transform = Instantiate(ItemAssets.itemAssets.ItemWorldPf, pos, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(items);

        return itemWorld;
    }

    private Items item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Items item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
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
