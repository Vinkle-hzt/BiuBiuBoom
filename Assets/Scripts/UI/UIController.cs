using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject zoneOver;
    public GameObject zoneStart;

    private void Start()
    {
        gameOver.SetActive(false);
        zoneOver.SetActive(false);
        zoneStart.SetActive(true);
    }

    private void OnEnable()
    {
        EventHandler.GameOverEvent += OnGameOverEvent;
        EventHandler.ZoneClearEvent += OnZoneClearEvent;
        EventHandler.ZoneActiveEvent += OnZoneStartEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameOverEvent -= OnGameOverEvent;
        EventHandler.ZoneClearEvent -= OnZoneClearEvent;
        EventHandler.ZoneActiveEvent -= OnZoneStartEvent;
    }

    private void OnGameOverEvent()
    {
        StartCoroutine(GameOverCouroutine());
    }

    private IEnumerator GameOverCouroutine()
    {
        yield return new WaitForSeconds(3);

        gameOver.SetActive(true);
        gameOver.GetComponent<Animator>().Play("finish");

        yield return new WaitForSeconds(3);

        SceneManage.instance.GoBackToMain();
    }

    private void OnZoneClearEvent(string zoneName)
    {
        StartCoroutine(ZoneClearCouroutine());
    }

    private IEnumerator ZoneClearCouroutine()
    {
        zoneOver.SetActive(true);
        zoneOver.GetComponent<Animator>().Play("finish");

        yield return new WaitForSeconds(2);

        zoneOver.SetActive(false);
    }

    private void OnZoneStartEvent(string zoneName, int rounds)
    {
        StartCoroutine(ZoneStartCouroutine(rounds));
    }

    private IEnumerator ZoneStartCouroutine(int rounds)
    {
        zoneStart.SetActive(true);
        zoneStart.transform.Find("Text").Find("Text_2").GetComponent<Text>().text = rounds.ToString() + " Rounds";
        zoneStart.GetComponent<Animator>().Play("finish");

        yield return new WaitForSeconds(2);

        zoneStart.SetActive(false);
    }
}
