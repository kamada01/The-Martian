using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public InventoryItem Item {get; set; }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(0, 0, 10);
    }
}