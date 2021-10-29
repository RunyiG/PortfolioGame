using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController = null;

    [SerializeField]
    private float playerSpeed = 5.0f;

    private Vector2 movement;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
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
        playerController.Rigidbody2D.MovePosition(playerController.Rigidbody2D.position + movement * playerSpeed * Time.fixedDeltaTime);
    }
}
