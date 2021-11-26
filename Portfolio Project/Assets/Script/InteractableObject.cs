using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private KeyCode interactKey = KeyCode.R;
    [SerializeField]
    private GameObject UIGameObject = null;
    public bool Interactable { get; set; }
    private bool showUI = false;

    private void Start()
    {
        UIGameObject.SetActive(false);
    }

    private void Update()
    {
        if (Interactable && Input.GetKeyDown(interactKey))
        {
            showUI = !showUI;
            UIGameObject.SetActive(showUI);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Interactable = true;
            collision.gameObject.GetComponent<PlayerController>().PlayerOperator.Active(interactKey.ToString());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Interactable = false;
            UIGameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerController>().PlayerOperator.Deactive();
        }
    }
}
