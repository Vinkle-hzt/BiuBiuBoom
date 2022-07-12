using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillIcons", menuName = "Skill/SkillIcons", order = 0)]
public class SkillIcons : ScriptableObject
{
    [Header("技能名称和图标")]
    public List<SkillDetails> skills;

    public SkillDetails GetSkillDetails(string skillName)
    {
        return skills.Find(skill => skill.skillName == skillName);
    }
}

[System.Serializable]
public class SkillDetails
{
    public string skillName;
    public Sprite skillIcon;
}
