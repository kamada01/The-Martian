using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    void Start()
    {
        Inventory.AddedItem += InventoryScript_AddedItem;
        Inventory.RemovedItem += Inventory_ItemRemoved;
    }

    private void InventoryScript_AddedItem(object sender, InventoryEventArgs e)
    {
        Transform inventory = transform.Find("Inventory");
        foreach (Transform slot in inventory)
        {
            // get inventory slot
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDrag itemDragHandler = imageTransform.GetComponent<ItemDrag>();

            // empty slot
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventory = transform.Find("Inventory");
        foreach(Transform slot in inventory)
        {
            // get inventory slots
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDrag itemDragHandler = imageTransform.GetComponent<ItemDrag>();

            // find item in UI
            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;
                break;
            }
        }
    }

}