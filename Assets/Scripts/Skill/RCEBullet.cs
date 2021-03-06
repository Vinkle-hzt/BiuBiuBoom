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
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += shootDir * shootSpeed * Time.deltaTime;
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

        yield return new WaitForSeconds(consistantTime);

        GetComponent<CircleCollider2D>().enabled = false;
    }

    public void ApplyBuff(InfoController info)
    {
        //施加buff
        info.DefenceDebuff(deleteDefence);
        info.SpeedDebuff(deleteSpeed);
    }

    public void ClearBuff(InfoController info)
    {
        //消去buff
        info.DefenceResume();
        info.SpeedResume();
        Destroy(hit.gameObject);
        Destroy(transform.gameObject);
    }
}
