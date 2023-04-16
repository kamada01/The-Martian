using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClick : MonoBehaviour
{
    public Inventory inventory;
    private InventoryItem selected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selected = inventory.InvList[0];
            Debug.Log("A" + inventory.InvList[0].Name);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("B" + inventory.InvList[1].Name);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("C" + inventory.InvList[2].Name);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("D" + inventory.InvList[3].Name);
        }

        if (selected != null)
        {
            inventory.UseItem(selected);
            selected.OnUse();

        }
    }
}
