using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [Header("子弹")] [SerializeField] 
    private Transform pfBullet;
    private PlayerAimWeapon playerAimWeapon;
    private void Start()
    {
        playerAimWeapon = GetComponent<PlayerAimWeapon>();
        playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
    }

    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (e.shootPosition - bulletTransform.position).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir, e.playerInfo);
    }

}
