using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO-DO: 动画处理
public class StateHacker : PlayerState
{
    private Rigidbody2D rb;

    //地面检测
    private RaycastHit2D ray1;
    private RaycastHit2D ray2;

    private bool isJump;
    private bool isGround;
    private bool isRunning;

    private Transform groundCheck1;
    private Transform groundCheck2;
    private LayerMask layer;

    // 动画相关
    private Animator anim;
    int groundedID;
    int runningID;
    int verticalVelID;
    float reverse;

    private float horizontalMove;
    private float faceDirection;

    private float curSkillCD;

    private float gravity;

    public StateHacker(Transform transform, InfoController pInfo, float gravity)
        : base(transform, pInfo)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        groundCheck1 = transform.Find("Body").Find("GroundCheck1");
        groundCheck2 = transform.Find("Body").Find("GroundCheck2");
        layer = 1 << LayerMask.NameToLayer("Ground");
        anim = transform.GetComponent<Animator>();
        isJump = false;
        isGround = false;

        // 初始化动画
        anim = transform.Find("Body").GetComponent<Animator>();
        groundedID = Animator.StringToHash("Grounded");
        runningID = Animator.StringToHash("Running");
        verticalVelID = Animator.StringToHash("VerticalVel");

        this.gravity = gravity;

        reverse = 1;
        EventHandler.AnimationReversePlay += OnAnimationReversePlay;
    }

    public override void FixedUpdate()
    {
        Movement();
        GroundCheck();
        RecoveryEnergy();
    }

    public override void Update()
    {
        MoveCheck();
        UpdateAnim();
    }

    private void OnAnimationReversePlay(bool isReverse)
    {
        Debug.Log("call");
        if (isReverse && isGround)
        {
            //anim.speed = -1;
            reverse = -1;
            Debug.Log("reverse");
        }
        else
        {
            //anim.speed = 1;
            reverse = 1;
        }
    }

    void Movement()
    {
        if (horizontalMove != 0)
            rb.velocity = new Vector2(horizontalMove * pInfo.Speed, rb.velocity.y);

        if (Mathf.Abs(horizontalMove) > 0.1f)
            isRunning = true;
        else
            isRunning = false;

        Jump();
    }

    void MoveCheck()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");

        // check jump
        if (Input.GetKeyDown(InputController.instance.jump) && isGround)
        {
            isJump = true;
        }
    }

    void Jump()
    {
        if (isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, pInfo.Jumpforce);
            isJump = false;
        }
    }

    void GroundCheck()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    void RecoveryEnergy()
    {
        pInfo.GetEnergy(Time.fixedDeltaTime);
    }

    public override void Reset()
    {
        rb.gravityScale = gravity;
        pInfo.CanShoot = true;
        transform.Find("Aim").gameObject.SetActive(true);
    }

    public override void Leave()
    {
        return;
    }

    void UpdateAnim()
    {
        anim.SetBool(groundedID, isGround);
        anim.SetBool(runningID, isRunning);
        anim.SetFloat("isReverse", reverse);
        anim.SetFloat(verticalVelID, rb.velocity.y);
    }
}
