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

    private void OnZoneClearEvent()
    {
        foreach (var zone in FindObjectsOfType<Zone>())
        {
            if (!zone.isClear)
                return;
        }

        //TODO: 通关的门可以交互
        //door.GetComponent<Collider2D>().enabled = true;
    }
}
