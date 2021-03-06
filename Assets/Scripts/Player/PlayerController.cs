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
    public GameObject trail_eye;

    private float gravity;
    bool isDead = false;
    private float deadTime = 1f;

    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Cinemachine");
        pInfo = GetComponent<InfoController>();
        gravity = GetComponent<Rigidbody2D>().gravityScale;
        hacker = new StateHacker(transform, pInfo, gravity);
        shadow = new StateShadow(transform, pInfo, pfAimer);
        state = hacker;
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            HealthCheck();
            changeState();
            state.Update();

            TrailControl();
        }
        else
        {
            DeadAction();
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
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
                BgmManager.instance.PlayChangeState();

                transform.Find("Body").GetComponent<Animator>().Play("PlayShadow");

                state.Leave();
                state = shadow;
                state.Reset();
            }
            else if (state is StateShadow)
            {
                transform.Find("Body").GetComponent<Animator>().Play("PlayerIdle");

                state.Leave();
                state = hacker;
                state.Reset();
            }
        }

        // 能量 <= 0 强制回到骇客模式
        if (pInfo.characterData.energy <= 0 && state is StateShadow)
        {
            transform.Find("Body").GetComponent<Animator>().Play("PlayerIdle");

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
            trail_eye.SetActive(false);
        }
        else if (state == hacker)
        {
            trail.SetActive(false);
            trail_eye.SetActive(true);
        }
    }
    void HealthCheck()
    {
        if (pInfo.characterData.health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;
        curTime = 0;
    }

    void DeadAction()
    {
        //transform.Find("Body").GetComponent<Animator>().SetBool("Dead", true);
        curTime += Time.deltaTime;
        if (curTime >= deadTime)
            SceneManage.instance.RestartScene();
    }

    public void StartHitTimeScale()
    {
        StartCoroutine(HitTimeScale());
    }

    IEnumerator HitTimeScale()
    {
        Time.timeScale = 0.2f;
        //GetComponent<Animator>().speed = 0;
        yield return new WaitForSeconds(0.06f);
        Time.timeScale = 1;
        //GetComponent<Animator>().speed = 1;
    }
}
