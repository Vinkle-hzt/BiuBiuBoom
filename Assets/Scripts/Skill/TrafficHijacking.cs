using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BiuBiuBoom.Utils;

public class TrafficHijacking : Skill
{
    public Transform pfBullet;
    public TrafficHijacking(Transform transform) : base(transform)
    {
        base.level = 1;
        base.InitialCoolDownRatio();
        base.InitialCache();
        pfBullet = Resources.Load<Transform>("Prefabs/Skill/TrafficHijackingBullet");
    }

    public override void SkillActive()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mouseWorldPosition - transform.position).normalized;

        Transform bulletTransform = Object.Instantiate(pfBullet, transform.position, Quaternion.identity);

        bulletTransform.GetComponent<TrafficHijackingBullet>().Setup(aimDirection);
    }
}
