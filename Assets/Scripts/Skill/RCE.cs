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
        base.GetCoolDown();
        bullet = Resources.Load<Transform>("/Prefabs/crossed");
        gunEndPoint = transform.Find("GunEndPointPosition");
    }

    public override void SkillActive()
    {
        base.times--;

        Vector3 shootDir = (Input.mousePosition - gunEndPoint.position).normalized;
        Vector3 shootRotation = new Vector3(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * 180 / Mathf.PI);
        Transform RCEbullet = transform.GetComponent<InstantiateCS>().InstantiateBullet(bullet, gunEndPoint.position, Quaternion.Euler(shootRotation));

        RCEbullet.GetComponent<RCEBullet>().Initial(shootDir);
    }
}
