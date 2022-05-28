using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;
// TO-DO：完成入侵和处决 (入侵键位 Fire1, 处决键位 Fire2)
public class StateShadow : PlayerState
{
    private Rigidbody2D rb;
    private Vector3 position;
    private bool inHack; // 是否在hack
    private GameObject hackEnemy; // hack的对象
    public StateShadow(Transform body, InfoController pInfo) : base(body, pInfo)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        inHack = false;
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
        transform.position += position * pInfo.characterData.speed * Time.deltaTime * pInfo.characterData.speedRatio;
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
    }

    #region 斩杀和入侵
    GameObject findNearestEnemy()
    {
        Collider2D[] enemies = new Collider2D[20];
        int num = Physics2D.OverlapCircleNonAlloc(transform.position, pInfo.HackDis, enemies, LayerMask.GetMask("EnemyFall"));
        if (num == 0)
        {
            Debug.Log("Notfind");
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
            Debug.Log("Find, position = " + ret.transform.position);
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
                if (Input.GetKeyDown(InputController.instance.kill))
                    Kill(enemy);
                else if (Input.GetKeyDown(InputController.instance.hack))
                    Hack(enemy);
            }
        }
    }

    void Kill(GameObject enemy)
    {
        Debug.Log("Kill");
        enemy.GetComponent<Enemy>().Dead();
        // 应该要有个瞬移的动画，我这直接瞬移了
        transform.position = enemy.transform.position;
    }

    void Hack(GameObject enemy)
    {
        inHack = true;
        hackEnemy = enemy;
        // 取消碰撞体积，隐藏角色
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
        transform.Find("Body").gameObject.SetActive(false);
    }

    void Control()
    {
        Debug.Log("control");
        hackEnemy.GetComponent<Enemy>().Control(Time.deltaTime);
        // 再次按下骇入键，怪物死亡
        if (Input.GetKeyDown(InputController.instance.hack))
            LeaveHack();
    }

    void LeaveHack()
    {
        Debug.Log("leave hack");
        inHack = false;
        // 角色到怪物位置
        transform.position = hackEnemy.transform.position;
        hackEnemy.GetComponent<Enemy>().Dead();
        // 取消碰撞体积，隐藏角色
        transform.GetComponent<CapsuleCollider2D>().enabled = true;
        transform.Find("Body").gameObject.SetActive(true);
    }

    public override void Leave()
    {
        if (inHack)
            LeaveHack();
    }
    #endregion
}
