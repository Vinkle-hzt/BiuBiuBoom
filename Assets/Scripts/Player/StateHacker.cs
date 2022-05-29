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
    private float checkDistance;
    private LayerMask layer;

    // 动画相关
    private Animator anim;
    int groundedID;
    int runningID;
    int verticalVelID;

    private float horizontalMove;
    private float faceDirection;

    private float curSkillCD;

    public StateHacker(Transform transform, InfoController pInfo) : base(transform, pInfo)
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
        SkillActive();
    }

    void Movement()
    {
        if (horizontalMove != 0)
            rb.velocity = new Vector2(horizontalMove * pInfo.characterData.speed, rb.velocity.y);

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
            rb.velocity = new Vector2(rb.velocity.x, pInfo.characterData.jumpforce);
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

    void RecoveryEnergy()
    {
        pInfo.GetEnergy(Time.fixedDeltaTime);
    }

    public override void Reset()
    {
        rb.gravityScale = 1;
        pInfo.characterData.canShoot = true;
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
        anim.SetFloat(verticalVelID, rb.velocity.y);
    }

    void SkillActive()
    {
        if (pInfo.skill == null)
            return;

        if (pInfo.skill.isFirst)
        {
            curSkillCD = pInfo.skill.initialCoolDown;
            pInfo.skill.isFirst = false;
        }

        if (curSkillCD <= 0)
        {
            if (pInfo.skill != null)
            {
                if (Input.GetKeyDown(InputController.instance.skill))
                {
                    //还有数用次数
                    if (pInfo.skill.times > 0)
                    {
                        //触发技能
                        pInfo.skill.SkillActive();
                        curSkillCD = pInfo.skill.coolDown;
                    }

                    if (pInfo.skill.times <= 0)
                    {
                        pInfo.skill = null;
                    }
                }
            }
        }
        else
        {
            curSkillCD -= Time.deltaTime;
        }
    }
}
