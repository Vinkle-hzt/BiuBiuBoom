using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject door;

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
        foreach (var zone in FindObjectsOfType<Zone>())
        {
            if (!zone.isClear)
                return;
        }

        EventHandler.CallGameOverEvent();
    }
}
