using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    [Header("移动幅度[0-1]")]
    [Header("越接近1,移动越不明显")]
    public float moveRate;

    private float startPoint;

    private void Start()
    {
        startPoint = transform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector2(startPoint + cam.position.x * moveRate, transform.position.y);
    }
}
