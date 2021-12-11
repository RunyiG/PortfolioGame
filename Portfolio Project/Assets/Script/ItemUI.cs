using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Image image;
    private Items item;

    private TextMeshProUGUI amountText;
    private TextMeshProUGUI priceText;

    [SerializeField]
    private KeyCode interactKey = KeyCode.E;
    [SerializeField]
    private GameObject UIGameObject_1 = null;
    [SerializeField]
    private GameObject UIGameObject_2 = null;
    [SerializeField]
    private GameObject UIGameObject_3 = null;
    public bool Interactable { get; set; }
    private bool showUI = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        image = transform.Find("image").GetComponent<Image>();
        amountText = transform.Find("amountText").GetComponent<TextMeshProUGUI>();
        priceText = transform.Find("priceText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            showUI = !showUI;
            UIGameObject_1.SetActive(showUI);
            UIGameObject_2.SetActive(showUI);
            UIGameObject_3.SetActive(showUI);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        ItemDragUI.Instance.Show(item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        ItemDragUI.Instance.Hide();
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetAmountText(int amount)
    {
        if (amount <= 1)
        {
            amountText.text = " ";
        }
        else
        {
            amountText.text = amount.ToString();
        }
    }

    public void SetPriceText()
    {
        int price;
        price = Items.GetSellPrice(item.itemTypes) * item.amount;
        priceText.text = price.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetItem(Items item)
    {
        this.item = item;
        SetSprite(Items.GetSprite(item.itemTypes));
        SetAmountText(item.amount);
        SetPriceText();
    }



}
