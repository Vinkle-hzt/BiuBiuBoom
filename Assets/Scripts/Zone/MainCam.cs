using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCam : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject player;

    private void OnEnable()
    {
        //EventHandler.LockCameraPosition += OnLockCameraPosition;
        EventHandler.ZoneClearEvent += OnZoneClearEvent;
    }

    private void OnDisable()
    {
        //EventHandler.LockCameraPosition -= OnLockCameraPosition;
        EventHandler.ZoneClearEvent -= OnZoneClearEvent;
    }

    // private void OnLockCameraPosition(Vector2 targetPos)
    // {
    //     cinemachineVirtualCamera.Follow = null;
    //     this.transform.position = targetPos;
    // }

    private void OnZoneClearEvent()
    {
        cinemachineVirtualCamera.Follow = player.transform;
    }
}
