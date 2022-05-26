using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    private InfoController info;
    void Awake()
    {
        info = GameObject.FindGameObjectWithTag("Player").GetComponent<InfoController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = info.characterData.health / info.characterData.maxHealth;
    }
}
