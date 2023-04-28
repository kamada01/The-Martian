using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, InventoryItem
{
    public GameObject Hand;
    public GameObject Astronaut;

    public virtual string Name
    {
        get { return "base_item"; }
    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get { return _Image; }
    }

    // pick up item from floor into inventory
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    // drop weapon from inventory
    public void OnDrop()
    {
        gameObject.SetActive(false);
        // gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    // select weapon from inventory into hand
    public virtual void OnUse()
    {
        transform.position = Hand.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Astronaut.transform.rotation.eulerAngles.z);
    }
}