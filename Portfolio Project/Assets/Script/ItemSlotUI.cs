using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IDropHandler
{
    private Action onDropAction;
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs : EventArgs
    {
        public Items item;
        public int slot;
    }
    private int slot;
    public void SetOnDropAction(Action onDropAction)
    {
        this.onDropAction = onDropAction;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropItemSlot");
        ItemDragUI.Instance.Hide();
        onDropAction?.Invoke();
        Items items = ItemDragUI.Instance.GetItem();
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { item = items, slot = slot });
    }
}
