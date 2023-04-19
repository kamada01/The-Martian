using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    public GameObject inventory;
    public Inventory inv;
    public GameObject Hand;
    private int selected;

    private void Update()
    {
        // detect player input from choosing item from inventory
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Hand.transform.GetChild(0).gameObject.SetActive(false);
            Hand.transform.DetachChildren();
            selected = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Hand.transform.GetChild(0).gameObject.SetActive(false);
            Hand.transform.DetachChildren();
            selected = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Hand.transform.GetChild(0).gameObject.SetActive(false);
            Hand.transform.DetachChildren();
            selected = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Hand.transform.GetChild(0).gameObject.SetActive(false);
            Hand.transform.DetachChildren();
            selected = 3;
        }

        try
        {
            ItemDrag dragHandler = inventory.transform.GetChild(selected).GetChild(0).GetComponent<ItemDrag>();
            InventoryItem item = dragHandler.Item;
            inv.UseItem(item);
            item.OnUse();
        } catch { }




    }
}
