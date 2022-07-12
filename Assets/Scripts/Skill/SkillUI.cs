using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public GameObject[] skills;
    public GameObject skillCache;
    public GameObject player;
    public GameObject cd;
    public GameObject ready;
    public SkillIcons skillIcons;
    public GameObject overload;
    public GameObject cache;
    private Transform cacheFill;
    private PermissionController permissionController;

    // Start is called before the first frame update
    void Start()
    {
        permissionController = player.GetComponent<PermissionController>();
        cacheFill = cache.transform.Find("Fill");
        cacheFill.GetComponent<Image>().color = new Color(255, 255, 255, 1);
    }

    // Update is called once per frame
    void Update()
    {
        CheckCache();
        CheckCD();
        CheckOverload();
        CheckSkillIcons();
        CheckSkillCache();
    }

    void CheckCD()
    {
        if (permissionController.curCoolDownTime <= 0)
        {
            cd.SetActive(true);
            ready.SetActive(false);
            cd.transform.Find("CDNumber").GetComponent<Text>().text = permissionController.curCoolDownTime.ToString("0.00");
            cd.transform.Find("CDSilder").GetComponent<Slider>().maxValue = permissionController.maxCoolDownTime;
            cd.transform.Find("CDSilder").GetComponent<Slider>().value = permissionController.curCoolDownTime / permissionController.maxCoolDownTime;
        }
        else
        {
            cd.SetActive(false);
            ready.SetActive(true);
        }
    }

    void CheckSkillCache()
    {
        if (permissionController.cacheSkill != null)
        {
            skillCache.SetActive(true);
            SkillDetails skillDetails = skillIcons.GetSkillDetails(permissionController.cacheSkill);
            skillCache.GetComponent<Image>().sprite = skillDetails.skillIcon;
        }
        else
        {
            skillCache.SetActive(false);
            skillCache.GetComponent<Image>().sprite = null;
        }
    }

    void CheckCache()
    {
        cache.GetComponent<Slider>().maxValue = permissionController.maxCache;
        float originValue = cache.GetComponent<Slider>().value;
        float targetValue = permissionController.cache / permissionController.maxCache > 1 ? 1 : permissionController.cache / permissionController.maxCache;
        float speed = (originValue - targetValue) / 1.0f;
        if (originValue != targetValue)
        {
            cache.GetComponent<Slider>().value = originValue + speed * Time.deltaTime;
        }

        //控制颜色变化
        if (originValue >= 0 && originValue <= 0.5)
        {
            //改变B通道
            cacheFill.GetComponent<Image>().color = new Color(
                cacheFill.GetComponent<Image>().color.r,
                cacheFill.GetComponent<Image>().color.g,
                cacheFill.GetComponent<Image>().color.b - 255 * originValue * 2,
                1
            );
        }
        else if (originValue > 0.5 && originValue <= 1)
        {
            //改变G通道
            cacheFill.GetComponent<Image>().color = new Color(
                cacheFill.GetComponent<Image>().color.r,
                cacheFill.GetComponent<Image>().color.g - 255 * (originValue - 0.5f) * 2,
                cacheFill.GetComponent<Image>().color.b,
                1
            );
        }
    }

    void CheckSkillIcons()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            SkillDetails skillDetails = skillIcons.GetSkillDetails(permissionController.skills[(permissionController.curSkillIndex + i) % permissionController.skillNums]);
            skills[i].GetComponent<Image>().sprite = skillDetails.skillIcon;
        }
    }

    void CheckOverload()
    {
        if (permissionController.isOverload)
        {
            overload.SetActive(true);
        }
        else
        {
            overload.SetActive(false);
        }
    }
}
