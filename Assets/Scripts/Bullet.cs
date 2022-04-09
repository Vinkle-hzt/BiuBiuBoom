using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private float shootSpeed;
    private PlayerInfo pInfo;
    public void Setup(Vector3 shootDir, PlayerInfo pInfo)
    {
        this.pInfo = pInfo;
        this.shootDir = shootDir;
        shootSpeed = pInfo.shootSpeed;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += shootDir * shootSpeed * Time.deltaTime;
    }

    // TO-DO: 当打击到怪物时更新pInfo中的能量
    // note: 可以调用 pInfo.AddEnergy


    // TO-DO: 碰撞检测，碰撞后销毁(应该实例化一个爆炸特效)
}
