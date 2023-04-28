using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int Slots = 4;
    private List<InventoryItem> InvItems = new List<InventoryItem>();
    public event EventHandler<InventoryEventArgs> AddedItem;
    public event EventHandler<InventoryEventArgs> RemovedItem;
    public event EventHandler<InventoryEventArgs> UsedItem;

    public InventoryItem getItem(int i)
    {
        return InvItems[i];
    }

    public List<InventoryItem> InvList
    {
        set { InvItems = value;  }
        get { return InvItems;  }
    } 

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

    public void RemoveItem(InventoryItem item)
    {
        if (InvItems.Contains(item))
        {
            InvItems.Remove(item);
            item.OnDrop();

            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            if (RemovedItem != null)
            {
                RemovedItem(this, new InventoryEventArgs(item));
            }
        }
    }
    internal void UseItem(InventoryItem item)
    {
        if (UsedItem != null)
        {
            UsedItem(this, new InventoryEventArgs(item));
        }
    }
}