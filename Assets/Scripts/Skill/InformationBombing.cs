using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationBombing : Skill
{
    private Transform bullet;
    private Transform gunEndPoint;

    public InformationBombing(Transform transform) : base(transform)
    {
        base.level = 1;
        base.InitialCoolDownRatio();
        base.InitialCache();
        bullet = Resources.Load<Transform>("Prefabs/pulse");
        gunEndPoint = transform.GetChild(0).GetChild(1);
    }

    public override void SkillActive()
    {
        Vector2 shootDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunEndPoint.position).normalized;
        Vector3 shootRotation = new Vector3(0, 0, Mathf.Atan2(shootDir.y, shootDir.x) * 180 / Mathf.PI);
        Transform informationBullet = transform.GetComponent<InstantiateCS>().InstantiateBullet(bullet, gunEndPoint.position, Quaternion.Euler(shootRotation));

        informationBullet.GetComponent<InformationBullet>().Initial(shootDir, transform);
        BgmManager.instance.PlayPlayerShoot();
    }
}
