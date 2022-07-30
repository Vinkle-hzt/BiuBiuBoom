using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCEBullet : MonoBehaviour
{
    private enum State
    {
        bullet, buff
    }

    [SerializeField]
    private float speed;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float deleteDefence;
    [SerializeField]
    private float deleteSpeed;
    [SerializeField]
    private float consistantTime;
    [SerializeField]
    private LayerMask layer;

    private Vector3 shootDir;
    private State state;
    [SerializeField]
    private float shootSpeed;

    [SerializeField]
    [Header("爆炸效果")]
    private Transform pfHit;

    public GameObject circle;

    private bool isMove;
    private Transform hit;
    public void Initial(Vector3 shootDir)
    {
        state = State.bullet;
        this.shootDir = shootDir;
        isMove = true;
        circle.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += shootDir.normalized * shootSpeed * Time.deltaTime;
        }
        if (state == State.buff)
        {
            //新的加buff方法
            SubDefence subDefence = new SubDefence(deleteDefence, Time.deltaTime);   //每一帧施加buff，每个buff持续一帧
            SubSpeed subSpeed = new SubSpeed(deleteSpeed, Time.deltaTime);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var coll in colliders)
            {
                if (coll.gameObject.tag == "Enemy")
                {
                    coll.GetComponent<InfoController>().AddBuff(subDefence);
                    coll.GetComponent<InfoController>().AddBuff(subSpeed);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.bullet)
        {
            if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
            {
                hit = Instantiate(pfHit, transform.position, Quaternion.identity);
                isMove = false;
                //生成圆形范围，存在一定时间
                StartCoroutine(StartBuffs());
                //Destroy(gameObject);
                return;
            }
        }
    }

    IEnumerator StartBuffs()
    {
        state = State.buff;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
        circle.SetActive(true);
        GetComponent<CircleCollider2D>().radius = radius;
        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject, consistantTime);

        yield return new WaitForSeconds(consistantTime);

        GetComponent<CircleCollider2D>().enabled = false;
    }

    // public void ApplyBuff(InfoController info)
    // {
    //     //施加buff
    //     info.DefenceDebuff(deleteDefence);
    //     info.SpeedDebuff(deleteSpeed);
    // }

    // public void ClearBuff(InfoController info)
    // {
    //     //消去buff
    //     info.DefenceResume();
    //     info.SpeedResume();
    //     Destroy(hit.gameObject);
    //     Destroy(transform.gameObject);
    // }
}
