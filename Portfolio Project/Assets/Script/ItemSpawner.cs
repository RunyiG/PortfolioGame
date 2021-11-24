using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Items item;

    private void Start()
    {
        ItemWorld.SpawnItems(item, transform.position);
        Destroy(gameObject);
    }
}
