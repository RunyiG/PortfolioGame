using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOperator : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro text;

    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        text.enabled = false;
    }

    public void Active(string operatorKey)
    {
        spriteRenderer.enabled = true;
        text.enabled = true;
        text.text = operatorKey;
    }

    public void Deactive()
    {
        spriteRenderer.enabled = false;
        text.text = string.Empty;
        text.enabled = false;
    }
}
