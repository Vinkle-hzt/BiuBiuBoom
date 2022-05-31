using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    [Header("跳跃")]
    public KeyCode jump;
    [Header("切换形态")]
    public KeyCode changState;
    [Header("开枪")]
    public KeyCode fire;
    [Header("斩杀")]
    public KeyCode kill;
    [Header("骇入")]
    public KeyCode hack;
    [Header("使用权限")]
    public KeyCode skill;
    [Header("esc面板")]
    public KeyCode esc;

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
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
