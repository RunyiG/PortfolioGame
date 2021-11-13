using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController = null;

    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private InventoryUI inventoryUI;

    private Vector2 movement;
    private Inventory inventory;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Awake()
    {
        inventory = new Inventory();
        //inventory Object to UI invnetory
        inventoryUI.SetInventory(inventory);

        ItemWorld.SpawnItems(new Items { itemTypes = Items.ItemTypes.Food_1, amount = 1 }, new Vector3(5, 5, 0));
        ItemWorld.SpawnItems(new Items { itemTypes = Items.ItemTypes.Food_2, amount = 1 }, new Vector3(-5, 5, 0));
        ItemWorld.SpawnItems(new Items { itemTypes = Items.ItemTypes.Food_3, amount = 1 }, new Vector3(5, -5, 0));
        ItemWorld.SpawnItems(new Items { itemTypes = Items.ItemTypes.Food_4, amount = 1 }, new Vector3(-5, -5, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld!= null) 
        {
            inventory.AddItem(itemWorld.GetItems());
            itemWorld.Destory();
        }
    }

    // Input
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        playerController.Animator.SetFloat("Horizontal", movement.x);
        playerController.Animator.SetFloat("Vertical", movement.y);
        playerController.Animator.SetFloat("Speed", movement.magnitude);
    }

    //Movement
    void FixedUpdate()
    {
        playerController.Rigidbody2D.MovePosition(playerController.Rigidbody2D.position 
            + movement * playerSpeed * Time.fixedDeltaTime);
    }


}
