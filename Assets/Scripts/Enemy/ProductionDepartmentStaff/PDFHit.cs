using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDFHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            transform.parent.GetComponent<ProductionDepartmentStaff>().Hit(other.GetComponent<InfoController>());
        }
       
    }
}