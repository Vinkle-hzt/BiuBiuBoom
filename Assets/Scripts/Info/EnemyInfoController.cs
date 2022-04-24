using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoController : MonoBehaviour
{
    public EnemyInfo templateData;
    public EnemyInfo characterData;

    private void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }

    void addFallDownTime()
    {
        characterData.fallDownTime = Mathf.Min(
            characterData.fallDownTime += characterData.plusFallDownTime,
            characterData.maxFallDownTime
        );
    }

    void subResumePercent()
    {
        characterData.resumePercent = Mathf.Max(
            characterData.resumePercent - characterData.plusResumePercent,
            characterData.minResumePercent
            );
    }

    public void Rusume()
    {
        addFallDownTime();
        subResumePercent();
    }
}
