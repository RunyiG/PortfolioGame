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
    [SerializeField]
    private CraftingUI craftingUI;

    private Vector2 movement;
    private Inventory inventory;

    private Player player;

    //private CraftingSystem craftingSystem;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        CraftingSystem craftingSystem = new CraftingSystem();
        craftingUI.SetCraftingSystem(craftingSystem);
    }

    private void Awake()
    {
        inventory = new Inventory(UseItem,6);
        inventoryUI.SetPlayer(this);
        //inventory Object to UI invnetory
        inventoryUI.SetInventory(inventory);

        //CraftingSystem craftingSystem = new CraftingSystem();
        //craftingUI.SetCraftingSystem(craftingSystem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld!= null) 
        {
            if (inventory.AddItem(itemWorld.GetItems()))
                itemWorld.Destory();
        }
    }

    private void UseItem(Items item)
    {
        switch (item.itemTypes)
        {
            case Items.ItemTypes.Honey:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.Honey, amount = 1 });
                break;
            case Items.ItemTypes.Apple:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.Apple, amount = 1 });
                break;
            case Items.ItemTypes.Orange:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.Orange, amount = 1 });
                break;
            case Items.ItemTypes.Ice:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.Ice, amount = 1 });
                break;
            case Items.ItemTypes.HoneyRoastedApples:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.HoneyRoastedApples, amount = 1 });
                break;
            case Items.ItemTypes.AppleOrange:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.AppleOrange, amount = 1 });
                break;
            case Items.ItemTypes.OrangeHoney:
                inventory.RemoveItem(new Items { itemTypes = Items.ItemTypes.OrangeHoney, amount = 1 });
                break;
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
        playerController.Rigidbody2D.velocity = movement.normalized * playerSpeed * Time.fixedDeltaTime;
    }

}
