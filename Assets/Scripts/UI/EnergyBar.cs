using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyBar;
    private InfoController info;
    void Awake()
    {
        info = GameObject.FindGameObjectWithTag("Player").GetComponent<InfoController>();
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.fillAmount = info.Energy / info.MaxEnergy;
    }
}
