using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class TrafficHijackingBullet : MonoBehaviour
{
    private Vector3 shootDir;

    [SerializeField]
    [Header("移动速度")]
    private float shootSpeed;

    [SerializeField]
    [Header("变大倍率")]
    private float scaleRate;

    [SerializeField]
    [Header("硬直时间")]
    private float stillTime;

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir.normalized * shootSpeed * Time.deltaTime;
        transform.localScale += new Vector3(scaleRate, scaleRate, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "EnemyBullet":
                Destroy(collision.gameObject);
                break;
            case "Enemy":
                collision.GetComponent<InfoController>().AddBuff(new Stagger(stillTime));
                break;
        }
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }
}
