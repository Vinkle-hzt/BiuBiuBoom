using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移速")]
    public float speed;
    [Header("跳跃力度")]
    public float jumpForce;

    [Header("地面检测")]
    public GameObject groundCheck1;
    public GameObject groundCheck2;
    public float checkDistance;
    public LayerMask layer;

    private Rigidbody2D rb;
    private float horizontal;
    private float xVelocity;

    //地面检测
    private RaycastHit2D ray1;
    private RaycastHit2D ray2;

    [SerializeField]
    private bool isJump;
    [SerializeField]
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCheck();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Jump();
        Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector2(xVelocity * speed * Time.fixedDeltaTime, rb.velocity.y);

        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
        }
    }

    void MoveCheck()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        xVelocity = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGround)
        {
            isJump = true;
        }
        /*else if(Input.GetButtonUp("Jump") || !isGround)
        {
            isJump = false;
        }*/
    }

    void Jump()
    {
        if (isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            isJump = false;
        }
    }

    void GroundCheck()
    {
        ray1 = Physics2D.Raycast(groundCheck1.transform.position, Vector2.down, checkDistance, layer);
        ray2 = Physics2D.Raycast(groundCheck2.transform.position, Vector2.down, checkDistance, layer);
        
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
