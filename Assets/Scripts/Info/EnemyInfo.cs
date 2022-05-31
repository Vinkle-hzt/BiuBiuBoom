using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyInfo", menuName = "BiuBiuBoom/EnemyInfo", order = 0)]
public class EnemyInfo : ScriptableObject
{
    [Header("瘫痪时长")]
    public float fallDownTime;

    [Header("瘫痪延长时长")]
    public float plusFallDownTime;

    [Header("瘫痪最大时长")]
    public float maxFallDownTime;

    [Header("重启恢复血量百分比")]
    public float resumePercent;

    [Header("重启血量减少百分比")]
    public float plusResumePercent;

    [Header("重启最低生命百分比")]
    public float minResumePercent;

    [Header("怪物感知玩家的范围")]
    public float perceptionDis;
}
