using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAuthorization : Skill
{
    public Transform prefab;

    public ForceAuthorization(Transform transform) : base(transform)
    {
        base.level = 3;
        prefab = Resources.Load<Transform>("Prefabs/ForceAuthorization");
    }

    public override void SkillActive()
    {
        Transform informationBullet = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
