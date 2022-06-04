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
    public AudioSource changeState;

    private bool isTurret;
    private bool isDrone;
    private bool isPlayerShoot;
    private bool isEnemyFallDown;
    private bool isHack;
    private bool isKill;

    private int random_turret;
    private int random_drone;
    private int random_playerShoot;
    private int random_enemyFallDown;
    private int random_hack;
    private int random_kill;

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

        isTurret = false;
        isDrone = false;
        isPlayerShoot = false;
        isEnemyFallDown = false;
        isHack = false;
        isKill = false;

        random_turret = -1;
        random_drone = -1;
        random_playerShoot = -1;
        random_enemyFallDown = -1;
        random_hack = -1;
        random_kill = -1;
    }

    public void PlayTurretShoot()
    {
        if(isTurret)
        {
            turret[random_turret].Stop();
        }
        random_turret = UnityEngine.Random.Range(0, turret.Length);
        turret[random_turret].Play();
        isTurret = true;
    }

    public void PlayDroneShoot()
    {
        if(isDrone)
        {
            drone[random_drone].Stop();
        }
        random_drone = UnityEngine.Random.Range(0, drone.Length);
        drone[random_drone].Play();
        isDrone = true;
    }

    public void PlayPlayerShoot()
    {
        if(isPlayerShoot)
        {
            playerShoot[random_playerShoot].Stop();
        }
        random_playerShoot = UnityEngine.Random.Range(0, playerShoot.Length);
        playerShoot[random_playerShoot].Play();
        isPlayerShoot = true;
    }

    public void PlayPlayerFlash()
    {
        flash.Play();
    }

    public void PlayEnemyFallDown()
    {
        if(isEnemyFallDown)
        {
            enemyFallDown[random_enemyFallDown].Stop();
        }
        random_enemyFallDown = UnityEngine.Random.Range(0, enemyFallDown.Length);
        enemyFallDown[random_enemyFallDown].Play();
        isEnemyFallDown = true;
    }

    public void PlayHack()
    {
        if(isHack)
        {
            hack[random_hack].Stop();
        }
        random_hack = UnityEngine.Random.Range(0, hack.Length);
        hack[random_hack].Play();
        isHack = true;
    }

    public void PlayKill()
    {
        if(isKill)
        {
            kill[random_kill].Stop();
        }
        random_kill = UnityEngine.Random.Range(0, kill.Length);
        kill[random_kill].Play();
        isKill = true;
    }

    public void PlayChangeState()
    {
        changeState.Play();
    }
}
