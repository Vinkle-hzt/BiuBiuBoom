using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHacker : State
{
    private Rigidbody2D rb;

    //地面检测
    private RaycastHit2D ray1;
    private RaycastHit2D ray2;

    private bool isJump;
    private bool isGround;

    private Transform groundCheck1;
    private Transform groundCheck2;
    private float checkDistance;
    private LayerMask layer;

    private float horizontalMove;

    public StateHacker(Transform transform, PlayerInfo pinfo) : base(transform, pinfo)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        groundCheck1 = transform.Find("Body").Find("GroundCheck1");
        groundCheck2 = transform.Find("Body").Find("GroundCheck2");
        layer = 1 << LayerMask.NameToLayer("Ground");
        isJump = false;
        isGround = false;
    }

    public override void FixedUpdate()
    {
        Movement();
        GroundCheck();
    }

    public override void Update()
    {
        MoveCheck();
    }

    void Movement()
    {
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * pInfo.speed * Time.deltaTime, rb.velocity.y);
        }

        Jump();
    }

    void MoveCheck()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        // check jump
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isJump = true;
        }
    }

    void Jump()
    {
        if (isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, pInfo.jumpforce * Time.deltaTime);
            isJump = false;
        }
    }

    void GroundCheck()
    {
        ray1 = Physics2D.Raycast(groundCheck1.position, Vector2.down, checkDistance, layer);
        ray2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, checkDistance, layer);

        if (ray1 || ray2)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
        //或者用Physics2D.OverlapBox();
    }
}
