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

    public GameObject topRight;
    public GameObject bottomLeft;
    private Vector3 topRightL;
    private Vector3 bottomLeftL;
    // Start is called before the first frame update
    void Start()
    {
        inventory.UsedItem += Inventory_UsedItem;
        inventory.RemovedItem += Inventory_RemovedItem;

        CurHealth = MaxHealth;

        topRightL = topRight.transform.position;
        bottomLeftL = bottomLeft.transform.position;
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

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            DamagePopup.Create(worldPosition, 100);
        }

    }

    private Vector2 input;
    private void FixedUpdate()
    {
        if ((transform.position.x <= bottomLeftL.x && movement.x == -1) || (transform.position.x >= topRightL.x && movement.x == 1))
        {
            movement.x = 0;
        }
        if ( (transform.position.y >= topRightL.y && movement.y == 1))
        {
            movement.y = 0;
        }

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