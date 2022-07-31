using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private float shootSpeed;
    private InfoController pInfo;

    [SerializeField]
    [Header("爆炸效果")]
    private Transform pfHit;

    public void Setup(Vector3 shootDir, InfoController pInfo)
    {
        this.pInfo = pInfo;
        this.shootDir = shootDir;
        shootSpeed = pInfo.ShootSpeed;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += shootDir * shootSpeed * Time.deltaTime;
    }

    public void boom()
    {
        Instantiate(pfHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // TO-DO: 当打击到怪物时更新pInfo中的能量
    // note: 可以调用 pInfo.AddEnergy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collTag = pInfo.CharacterTag == "Player" ? "Enemy" : "Player";
        if (collision.CompareTag(collTag))
        {
            pInfo.TakeDamage(pInfo, collision.GetComponent<InfoController>());
            boom();
            return;
        }
        else if (collision.CompareTag("Ground"))
        {
            boom();
            return;
        }
    }
}
