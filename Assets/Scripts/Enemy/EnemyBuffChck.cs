using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffChck : MonoBehaviour
{
    private bool isBuff;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RCE" && !isBuff)
        {
            isBuff = true;
            //ApplyBuff(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "RCE" && !isBuff)
        {
            isBuff = true;
            //ApplyBuff(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "RCE" && isBuff)
        {
            isBuff = false;
            //ClearBuff(other);
        }
    }
}
