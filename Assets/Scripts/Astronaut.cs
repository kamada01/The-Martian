using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    public Camera cam;
    Vector2 mousePosition;

    public Inventory inventory;

    public GameObject Hand;

    public int MaxHealth = 100;
    public static int CurHealth = 100;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        inventory.UsedItem += Inventory_UsedItem;
        inventory.RemovedItem += Inventory_RemovedItem;

        CurHealth = MaxHealth;
    }

    private void Inventory_RemovedItem(object sender, InventoryEventArgs e)
    {
        InventoryItem item = e.Item;

        // put item into hand
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = null;
    }

    private void Inventory_UsedItem(object sender, InventoryEventArgs e)
    {
        InventoryItem item = e.Item;

        // put item into hand
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = Hand.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // get mouse position for aiming
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        healthBar.SetHealth(CurHealth);

    }

    private void FixedUpdate()
    {
        // update player position
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        // rotate player according to mouse position
        Vector2 direction = mousePosition - rb.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        InventoryItem item = collision.GetComponent<InventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }        
    }
}