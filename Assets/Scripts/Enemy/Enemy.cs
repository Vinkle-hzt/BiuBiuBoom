using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        normal, fallDown
    }

    private InfoController eInfo;
    private EnemyInfoController enemyInfo;
    private bool isFallDown;

    private float curTime;

    private void Awake()
    {
        eInfo = GetComponent<InfoController>();
        enemyInfo = GetComponent<EnemyInfoController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FallDown();
    }

    //瘫痪
    void FallDown()
    {
        if (eInfo.isFallDown())
        {
            if (!isFallDown)
            {
                curTime = enemyInfo.characterData.fallDownTime;
                isFallDown = true;
                //TODO: 瘫痪的表现
                //用枪消失替代美术表现
                transform.Find("Aim").gameObject.SetActive(false);
            }

            if (curTime <= 0)
            {
                isFallDown = false;
                //重启
                Resume();
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
    }

    void Resume()
    {
        //用枪消失替代美术表现
        transform.Find("Aim").gameObject.SetActive(true);
        enemyInfo.Rusume();
        eInfo.Resume(eInfo.characterData.maxHealth * enemyInfo.characterData.resumePercent);
    }
}
