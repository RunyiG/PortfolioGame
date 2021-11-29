using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
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
        ItemWorld itemworld = SpawnItems(item, dropPos + randomDir * 5.0f);
        itemworld.GetComponent<Rigidbody2D>().AddForce(randomDir * 5.0f, ForceMode2D.Impulse);
        return itemworld;
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
