using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;

    public AudioSource[] turret;
    public AudioSource[] drone;
    public AudioSource[] playerShoot;
    public AudioSource flash;
    public AudioSource[] enemyFallDown;
    public AudioSource[] hack;
    public AudioSource[] kill;

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
                Destroy(transform.gameObject);
            }
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayTurretShoot()
    {
        int random = UnityEngine.Random.Range(0, turret.Length);
        turret[random].Play();
    }

    public void PlayDroneShoot()
    {
        int random = UnityEngine.Random.Range(0, drone.Length);
        drone[random].Play();
    }

    public void PlayPlayerShoot()
    {
        int random = UnityEngine.Random.Range(0, playerShoot.Length);
        playerShoot[random].Play();
    }

    public void PlayPlayerFlash()
    {
        flash.Play();
    }

    public void PlayEnemyFallDown()
    {
        int random = UnityEngine.Random.Range(0, enemyFallDown.Length);
        enemyFallDown[random].Play();
    }

    public void PlayHack()
    {
        int random = UnityEngine.Random.Range(0, hack.Length);
        hack[random].Play();
    }

    public void PlayKill()
    {
        int random = UnityEngine.Random.Range(0, kill.Length);
        kill[random].Play();
    }
}
