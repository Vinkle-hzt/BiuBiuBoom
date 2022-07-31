using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float radius;

    private Vector3 shootDir;
    [SerializeField]
    private float shootSpeed;

    [SerializeField]
    [Header("爆炸效果")]
    private Transform pfHit;

    private bool isMove;
    private Transform hit;
    private Transform player;
    public void Initial(Vector2 shootDir, Transform transform)
    {
        this.shootDir = shootDir;
        isMove = true;
        player = transform;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += shootDir.normalized * shootSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            hit = Instantiate(pfHit, transform.position, Quaternion.identity);
            isMove = false;
            GetComponent<Collider2D>().enabled = false;

            //爆炸
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            Stagger stagger = new Stagger(3 * Time.deltaTime);   //掉线3帧
            foreach (var coll in colliders)
            {
                if (coll.gameObject.tag == "Enemy")
                {
                    //造成伤害
                    coll.GetComponent<InfoController>().TakeDamageBySkill(player.GetComponent<InfoController>(), coll.GetComponent<InfoController>());
                    coll.GetComponent<InfoController>().AddBuff(stagger);
                }
            }
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
