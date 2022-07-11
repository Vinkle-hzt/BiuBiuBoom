using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class BulletDrone : MonoBehaviour
{
    private Transform shootTarget;
    private float shootSpeed;
    public InfoController pInfo;
    private float trackTime;
    private float curTime;
    private Vector3 shootDir;

    [SerializeField]
    [Header("爆炸效果")]
    private Transform pfShot;
    public void Setup(Transform shootTarget, InfoController pInfo, float trackTime)
    {
        this.shootTarget = shootTarget;
        this.pInfo = pInfo;
        this.shootSpeed = pInfo.ShootSpeed;
        this.trackTime = trackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackTime > curTime)
        {
            shootDir = (shootTarget.position - transform.position).normalized;
            transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
            curTime += Time.deltaTime;
        }

        transform.position += shootDir * shootSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collTag = pInfo.CharacterTag == "Player" ? "Enemy" : "Player";

        if (collision.CompareTag(collTag))
        {
            pInfo.TakeDamage(pInfo, collision.GetComponent<InfoController>());
            Instantiate(pfShot, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        else if (collision.CompareTag("Ground"))
        {
            Instantiate(pfShot, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }
}
