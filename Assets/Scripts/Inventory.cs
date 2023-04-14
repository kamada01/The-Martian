using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int Slots = 4;
    private List<InventoryItem> InvItems = new List<InventoryItem>();
    public event EventHandler<InventoryEventArgs> AddedItem;

    public void AddItem(InventoryItem item)
    {
        if (InvItems.Count < Slots)
        {
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider.enabled)
            {
                collider.enabled = false;
                InvItems.Add(item);
                item.OnPickup();

                if (AddedItem != null)
                {
                    AddedItem(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}