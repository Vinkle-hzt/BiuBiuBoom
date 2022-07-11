using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCE : Skill
{
    private Transform bullet;
    private Transform gunEndPoint;

    public RCE(Transform transform) : base(transform)
    {
        base.level = 2;
        bullet = Resources.Load<Transform>("Prefabs/crossed");
        gunEndPoint = transform.GetChild(0).GetChild(1);
    }

    public override void SkillActive()
    {
        Vector3 shootDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunEndPoint.position).normalized;
        Vector3 shootRotation = new Vector3(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * 180 / Mathf.PI);
        Transform RCEbullet = transform.GetComponent<InstantiateCS>().InstantiateBullet(bullet, gunEndPoint.position, Quaternion.Euler(shootRotation));

        RCEbullet.GetComponent<RCEBullet>().Initial(shootDir);
        BgmManager.instance.PlayPlayerShoot();
    }
}
