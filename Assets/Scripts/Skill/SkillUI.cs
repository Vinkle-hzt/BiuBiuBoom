using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    Transform[] skills;
    string[] skillNames;
    public GameObject player;
    public Text cd;
    public Text times;

    // Start is called before the first frame update
    void Start()
    {
        skills = GetComponentsInChildren<Transform>();
        skillNames = new string[skills.Length];

        for (int i = 0; i < skills.Length; i++)
        {
            skillNames[i] = skills[i].name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //CD

        // if (player.GetComponent<PlayerController>().state.GetCurSkillTime() > 0)
        // {
        //     cd.gameObject.SetActive(true);
        //     cd.text = ((int)player.GetComponent<PlayerController>().state.GetCurSkillTime() + 1).ToString();

        //     for (int i = 1; i < skillNames.Length; i++)
        //     {
        //         if (skillNames[i] == player.GetComponent<PlayerController>().pInfo.skill_name)
        //         {
        //             skills[i].GetComponent<Image>().color = new Color(
        //                 skills[i].GetComponent<Image>().color.r,
        //                 skills[i].GetComponent<Image>().color.g,
        //                 skills[i].GetComponent<Image>().color.b,
        //                 0.5f
        //             );
        //         }
        //     }
        // }
        // else
        // {
        //     cd.gameObject.SetActive(false);

        //     for (int i = 1; i < skillNames.Length; i++)
        //     {
        //         if (skillNames[i] == player.GetComponent<PlayerController>().pInfo.skill_name)
        //         {
        //             skills[i].GetComponent<Image>().color = new Color(
        //                 skills[i].GetComponent<Image>().color.r,
        //                 skills[i].GetComponent<Image>().color.g,
        //                 skills[i].GetComponent<Image>().color.b,
        //                 1f
        //             );
        //         }
        //     }
        // }

        // TODO: 重新更改 skill 位置
        // // 没有获取技能
        // if (player.GetComponent<PlayerController>().pInfo.skill == null)
        // {
        //     for (int i = 1; i < skills.Length; i++)
        //     {
        //         skills[i].gameObject.SetActive(false);
        //     }

        //     times.gameObject.SetActive(false);
        //     cd.gameObject.SetActive(false);
        // }
        // //有技能
        // else
        // {
        //     //修改次数UI
        //     //times.text = "x" + player.GetComponent<PlayerController>().pInfo.skill.times.ToString();
        //     times.gameObject.SetActive(true);

        //     for (int i = 1; i < skillNames.Length; i++)
        //     {
        //         skills[i].gameObject.SetActive(false);
        //         if (skillNames[i] == player.GetComponent<PlayerController>().pInfo.skill_name)
        //         {
        //             skills[i].gameObject.SetActive(true);
        //         }
        //     }
        // }
    }
}
