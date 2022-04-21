using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    State state;
    State hacker;
    State shadow;

    // PlayerInfo pInfo;
    // PlayerInfo curpInfo;
    InfoController pInfo;

    [SerializeField]
    float curTime;

    // Start is called before the first frame update
    void Start()
    {
        // pInfo = Resources.Load<PlayerInfo>("PlayerData/BaseInfo");
        // curpInfo = Instantiate(pInfo);
        pInfo = GetComponent<InfoController>();
        hacker = new StateHacker(transform, pInfo);
        shadow = new StateShadow(transform, pInfo);
        state = hacker;
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        changeState();
        state.Update();
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
        if (Input.GetButtonDown("ChangeState"))
        {
            if (pInfo.characterData.energy >= pInfo.characterData.changeStateEnergy
                && curTime <= 0
                && state is StateHacker)
            {
                curTime = pInfo.characterData.changeStateTime; // 进入冷却
                pInfo.characterData.energy -= pInfo.characterData.changeStateEnergy; // 减少能量

                state = shadow;
                state.Reset();
            }
            else if (state is StateShadow)
            {
                state = hacker;
                state.Reset();
            }
        }

        // 能量 <= 0 强制回到骇客模式
        if (pInfo.characterData.energy <= 0 && state is StateShadow)
        {
            state = hacker;
            state.Reset();
        }
    }
}