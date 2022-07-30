using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static bool startShake = false; //camera是否开始震动
    public static float seconds = 0.05f; //震动持续时间
    public static bool started = false; //是否已经开始震动
    public static float quake = 0.2f; //震动系数

    private Vector3 camPOS; //camera的起始位置
    // Start is called before the first frame update
    void Start()
    {
        camPOS = transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (startShake)
        {
            transform.localPosition = camPOS + Random.insideUnitSphere * quake;
        }

        if (started)
        {
            StartCoroutine(WaitForSecond(seconds));
            started = false;
        }
    }

    public void ShakeFor(float a, float b)
    {
        //if(startShake)
        //return;
        seconds = a;
        started = true;
        startShake = true;
        quake = b;

    }

    IEnumerator WaitForSecond(float a)
    {
        yield return new WaitForSeconds(a);
        startShake = false;
        transform.localPosition = camPOS;
    }

}
