using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float moveSpeed;

    Vector2 moveDirection;
    Vector2 faceDirection;
    Rigidbody2D rb;
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        PlayerInput();
        PlayerMove();
        PlayerFacing();
        PlayerDash();
    }
    private void PlayerInput()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        faceDirection = worldPos - (Vector2)transform.position;
    }
    private void PlayerMove()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
    private void PlayerFacing()
    {
        float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90f + angle);
    }
    private void PlayerDash()
    {
        
    }

    
}
