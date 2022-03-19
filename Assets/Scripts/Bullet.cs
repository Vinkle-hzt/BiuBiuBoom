using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    [Header("子弹速度")]
    public float shootSpeed = 10f;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += shootDir * shootSpeed * Time.deltaTime;
    }
}
