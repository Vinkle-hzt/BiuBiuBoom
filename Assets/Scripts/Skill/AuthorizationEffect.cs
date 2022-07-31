using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorizationEffect : MonoBehaviour
{
    [SerializeField]
    private float forceTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            float ratio = other.GetComponent<Enemy>().isElite ? 0.6f : 1.0f;
            Stagger stagger = new Stagger(forceTime * ratio);
            other.GetComponent<InfoController>().AddBuff(stagger);
        }
    }

    public void Finish()
    {
        Destroy(this.gameObject);
    }
}
