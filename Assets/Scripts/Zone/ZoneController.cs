using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public Transform currentZone;
    public int currentRounds;
    private Transform[] enemyRounds;
    private bool isFight;
    private int fightRound;
    private int enemyNumbers;
    [Header("波次倒计时")]
    public float loadNextRoundTime;
    private float currentLoadNextRoundTime;

    private void Start()
    {
        //GameObject.Find()是全局搜索
        //Transform.Find()是搜索子物体
        currentZone = GameObject.Find("Zone1").transform;
        GetCurrentZoneChildren();

        //初始化
        isFight = false;
        fightRound = 1;
        enemyNumbers = enemyRounds[fightRound - 1].childCount;
        currentLoadNextRoundTime = loadNextRoundTime;
    }

    private void Update()
    {
        if (isFight)
        {
            CheckRoundEnd();
        }
    }

    private void OnEnable()
    {
        EventHandler.ZoneActiveEvent += OnZoneActiveEvent;
        EventHandler.EnemyDeathEvent += OnEnemyDeathEvent;
    }

    private void OnDisable()
    {
        EventHandler.ZoneActiveEvent -= OnZoneActiveEvent;
        EventHandler.EnemyDeathEvent -= OnEnemyDeathEvent;
    }

    private void OnEnemyDeathEvent()
    {
        enemyNumbers--;
    }

    private void OnZoneActiveEvent(string zoneName, int rounds)
    {
        //获取当前区域的波次
        currentZone = transform.Find(zoneName).transform;
        currentRounds = rounds;
        GetCurrentZoneChildren();
        currentLoadNextRoundTime = loadNextRoundTime;
        fightRound = 1;
        enemyNumbers = enemyRounds[fightRound - 1].childCount;
    }

    private void GetCurrentZoneChildren()
    {
        enemyRounds = new Transform[currentRounds];

        for (int i = 0; i < currentRounds; i++)
        {
            enemyRounds[i] = currentZone.GetChild(i);
            //隐藏后面的波次
            if (i != 0) enemyRounds[i].gameObject.SetActive(false);
        }
    }

    private void CheckRoundEnd()
    {
        if (currentLoadNextRoundTime <= 0f || enemyNumbers <= 0)
        {
            //还有剩余波次
            if (fightRound < currentRounds)
            {
                fightRound++;
                enemyNumbers += enemyRounds[fightRound - 1].childCount;
                enemyRounds[fightRound - 1].gameObject.SetActive(true);
                currentLoadNextRoundTime = loadNextRoundTime;
            }
            //最后一波打完了
            else
            {
                isFight = false;
                currentZone = null;
                EventHandler.CallZoneClearEvent();
            }
        }
        //如果为最后一个波次则不再倒计时
        else if (currentLoadNextRoundTime > 0f && fightRound < currentRounds)
            currentLoadNextRoundTime -= Time.deltaTime;
    }
}