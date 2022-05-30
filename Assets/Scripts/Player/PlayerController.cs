using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState state;
    private PlayerState hacker;
    private PlayerState shadow;
    public InfoController pInfo;

    [SerializeField]
    private Transform pfAimer;

    [SerializeField]
    float curTime;

    public GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        pInfo = GetComponent<InfoController>();
        hacker = new StateHacker(transform, pInfo);
        shadow = new StateShadow(transform, pInfo, pfAimer);
        state = hacker;
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        changeState();
        state.Update();

        TrailControl();
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }

    // TO-DO: 长按改变形态  添加变身相关 animator
    private void changeState()
    {
        // 只有在骇客形态才会冷却变身
        if (state is StateHacker)
        {
            curTime = Mathf.Max(0, curTime - Time.deltaTime);
        }

        // 检查按键是否按下
        if (Input.GetKeyDown(InputController.instance.changState))
        {
            if (pInfo.characterData.energy >= pInfo.characterData.changeStateEnergy
                && curTime <= 0
                && state is StateHacker)
            {
                curTime = pInfo.characterData.changeStateTime; // 进入冷却
                pInfo.characterData.energy -= pInfo.characterData.changeStateEnergy; // 减少能量

                state.Leave();
                state = shadow;
                state.Reset();
            }
            else if (state is StateShadow)
            {
                state.Leave();
                state = hacker;
                state.Reset();
            }
        }

        // 能量 <= 0 强制回到骇客模式
        if (pInfo.characterData.energy <= 0 && state is StateShadow)
        {
            state.Leave();
            state = hacker;
            state.Reset();
        }
    }

    public InfoController GetInfo()
    {
        return pInfo;
    }

    void TrailControl()
    {
        if (state == shadow)
        {
            trail.SetActive(true);
        }
        else if (state == hacker)
        {
            trail.SetActive(false);
        }
    }
}
