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
        transform.Find("Edge").gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isClear)
        {
            // if (isLock)
            //     EventHandler.CallLockCameraPosition(transform.position);
            GetComponent<Collider2D>().enabled = false;
            transform.Find("Edge").gameObject.SetActive(true);
            EventHandler.CallZoneActiveEvent(transform.name, rounds);
        }
    }
}
