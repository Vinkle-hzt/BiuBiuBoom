using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    State state;
    State hacker;
    State shadow;

    PlayerInfo pInfo;

    // Start is called before the first frame update
    void Start()
    {
        pInfo = GetComponent<PlayerInfo>();
        hacker = new StateHacker(transform, pInfo);
        shadow = new StateShadow(transform, pInfo);
        state = hacker;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }

    // TO-DO: 改变形态
    private void changeState()
    {
        
    }
}
