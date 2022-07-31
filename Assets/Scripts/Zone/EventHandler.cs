using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public static event Action<Vector2> LockCameraPosition;
    public static void CallLockCameraPosition(Vector2 targetPos)
    {
        LockCameraPosition?.Invoke(targetPos);
    }

    public static event Action<string> ZoneClearEvent;
    public static void CallZoneClearEvent(string zoneName)
    {
        ZoneClearEvent?.Invoke(zoneName);
    }

    public static event Action<string, int> ZoneActiveEvent;
    public static void CallZoneActiveEvent(string zoneName, int rounds)
    {
        ZoneActiveEvent?.Invoke(zoneName, rounds);
    }

    public static event Action EnemyDeathEvent;
    public static void CallEnemyDeathEvent()
    {
        EnemyDeathEvent?.Invoke();
    }

    public static event Action<bool> AnimationReversePlay;
    public static void CallAnimationReversePlay(bool isReverse)
    {
        AnimationReversePlay?.Invoke(isReverse);
    }

    public static event Action GameOverEvent;
    public static void CallGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }
}
