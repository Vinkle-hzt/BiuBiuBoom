using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    public Sprite rightGear;

    private Transform gear;

    private void OnEnable()
    {
        EventHandler.ZoneClearEvent += OnZoneClearEvent;
    }

    private void OnDisable()
    {
        EventHandler.ZoneClearEvent -= OnZoneClearEvent;
    }

    private void OnZoneClearEvent(string zoneName)
    {
        gear = transform.Find(zoneName + "Gear");
        if (gear != null)
            gear.GetComponent<SpriteRenderer>().sprite = rightGear;
    }
}
