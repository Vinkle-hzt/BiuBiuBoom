using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;
using Cinemachine;
// TO-DO：完成入侵和处决 (入侵键位 Fire1, 处决键位 Fire2)
public class StateShadow : PlayerState
{
    private Rigidbody2D rb;
    private Vector3 position;
    private bool inHack; // 是否在hack
    private GameObject hackEnemy; // hack的对象
    private Transform pfAimer;

    public StateShadow(Transform body, InfoController pInfo, Transform pfAimer) : base(body, pInfo)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        inHack = false;
        this.pfAimer = Object.Instantiate(pfAimer, transform.position, Quaternion.identity);
        resetAimer();
    }

    public override void Update()
    {
        MoveCheck();
        Movement();
        Attack();
    }

    public override void FixedUpdate()
    {
        LossEnergy();
    }

    void Movement()
    {
        if (!inHack)
            transform.position += position * pInfo.characterData.speed * Time.deltaTime * pInfo.characterData.speedRatio;
        else
            transform.position = hackEnemy.transform.position;
    }

    void MoveCheck()
    {

        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
    }

    void LossEnergy()
    {
        pInfo.LossEnergy(Time.fixedDeltaTime);
    }

    public override void Reset()
    {
        // 重力和速度均设为0
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        pInfo.characterData.canShoot = false;
        transform.Find("Aim").gameObject.SetActive(false);
        //transform.Find("Body").GetComponent<Animator>().SetBool("Shadow", true);
    }

    #region 斩杀和入侵
    GameObject findNearestEnemy()
    {
        Collider2D[] enemies = new Collider2D[20];
        int num = Physics2D.OverlapCircleNonAlloc(transform.position, pInfo.HackDis, enemies, LayerMask.GetMask("EnemyFall"));
        if (num == 0)
        {
            //Debug.Log("Notfind");
            return null;
        }
        else
        {
            GameObject ret = enemies[0].gameObject;
            for (int i = 1; i < num; i++)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position) <
                    Vector2.Distance(ret.transform.position, transform.position))
                {
                    ret = enemies[i].gameObject;
                }
            }
            // 绘制矩形表示选中
            UtilsClass.Debug.DrawRect2D(ret.transform.position, 0.5f, Color.red);
            //Debug.Log("Find, position = " + ret.transform.position);
            return ret;
        }
    }

    void Attack()
    {
        if (inHack)
        {
            Control();
        }
        else
        {
            var enemy = findNearestEnemy();
            if (enemy != null)
            {
                setAimer(enemy.transform.position);
                if (Input.GetKeyDown(InputController.instance.kill))
                {
                    BgmManager.instance.PlayKill();
                    Kill(enemy);
                }
                else if (Input.GetKeyDown(InputController.instance.hack))
                {
                    BgmManager.instance.PlayHack();
                    Hack(enemy);
                }
            }
            else
                resetAimer();
        }
    }

    void Kill(GameObject enemy)
    {
        resetAimer();
        Debug.Log("Kill");
        enemy.GetComponent<Enemy>().Dead();
        // 应该要有个瞬移的动画，我这直接瞬移了
        Vector3 pos = transform.position;
        pos.x = enemy.transform.position.x;
        pos.y = enemy.transform.position.y;
        transform.position = pos;
        transform.GetComponent<PlayerController>().StartHitTimeScale();
    }

    void Hack(GameObject enemy)
    {
        resetAimer();
        inHack = true;
        hackEnemy = enemy;
        // 取消碰撞体积，隐藏角色
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.Find("Body").gameObject.SetActive(false);
    }

    void Control()
    {
        //Debug.Log("control");
        hackEnemy.GetComponent<Enemy>().Control(Time.deltaTime);
        // 再次按下骇入键，怪物死亡
        if (Input.GetKeyDown(InputController.instance.hack))
        {
            BgmManager.instance.PlayKill();
            transform.GetComponent<PlayerController>().StartHitTimeScale();
            LeaveHack();
        }
    }

    void LeaveHack()
    {
        //Debug.Log("leave hack");
        inHack = false;
        // 角色到怪物位置
        Vector3 pos = transform.position;
        pos.x = hackEnemy.transform.position.x;
        pos.y = hackEnemy.transform.position.y;
        transform.position = pos;
        hackEnemy.GetComponent<Enemy>().Dead();
        // 取消碰撞体积，隐藏角色
        transform.GetComponent<CapsuleCollider2D>().enabled = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
        transform.Find("Body").gameObject.SetActive(true);
        transform.Find("Body").GetComponent<Animator>().Play("PlayIdle");

        //获取权限
        transform.GetComponent<PermissionController>().GetNewSkill(hackEnemy.GetComponent<Enemy>().GetSkill());
    }

    public override void Leave()
    {
        if (inHack)
            LeaveHack();
        resetAimer();
        //transform.Find("Body").GetComponent<Animator>().SetBool("Shadow", false);
    }

    private void setAimer(Vector3 position)
    {
        pfAimer.position = position;
        pfAimer.gameObject.SetActive(true);
    }

    private void resetAimer()
    {
        pfAimer.gameObject.SetActive(false);
    }
    #endregion
}
