using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCS : MonoBehaviour
{
    public Transform InstantiateBullet(Transform bullet, Vector3 gunEndPointPosition, Quaternion rotation)
    {
        Transform skillBullet = Instantiate(bullet, gunEndPointPosition, rotation);
        Debug.Log(skillBullet.position);
        return skillBullet;
    }
}
