using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public bool isClear;
    public int rounds;
    public bool isLock;

    private void OnEnable()
    {
        EventHandler.ZoneClearEvent += OnZoneClearEvent;
    }

    private void OnDisable()
    {
        EventHandler.ZoneClearEvent -= OnZoneClearEvent;
    }

    private void OnZoneClearEvent()
    {
        isClear = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isClear)
        {
            if (isLock)
                EventHandler.CallLockCameraPosition(transform.position);
            EventHandler.CallZoneActiveEvent(transform.name, rounds);
        }
    }
}
