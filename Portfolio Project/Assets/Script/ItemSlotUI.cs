using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IDropHandler
{
    private Action onDropAction;

    public void SetOnDropAction(Action onDropAction)
    {
        this.onDropAction = onDropAction;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropItemSlot");
        ItemDragUI.Instance.Hide();
        onDropAction();
    }
}
