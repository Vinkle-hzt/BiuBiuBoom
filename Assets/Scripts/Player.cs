using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移速")]
    public float speed;
    [Header("跳跃力度")]
    public float jumpForce;

    private Rigidbody2D rb;
    private float horizontal;
    private float xVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        xVelocity = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(xVelocity * speed * Time.deltaTime, rb.velocity.y);

        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //TODO:地面监测，只能跳一次
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
        }
    }
}
