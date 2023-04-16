using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, InventoryItem
{
    public virtual string Name
    {
        get { return "base_item"; }
    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get { return _Image; }
    }
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
    public virtual void OnUse()
    {

    }
}