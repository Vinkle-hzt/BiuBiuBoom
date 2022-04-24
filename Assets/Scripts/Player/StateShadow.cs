using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO-DO：完成入侵和处决 (入侵键位 Fire1, 处决键位 Fire2)
public class StateShadow : PlayerState
{
    private Rigidbody2D rb;
    private Vector3 position;
    public StateShadow(Transform body, InfoController pInfo) : base(body, pInfo)
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        MoveCheck();
        Movement();
    }

    public override void FixedUpdate()
    {
        LossEnergy();
    }

    void Movement()
    {
        transform.position += position * pInfo.characterData.speed * Time.deltaTime * pInfo.characterData.speedRatio;
    }

    void MoveCheck()
    {
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
    }

    void LossEnergy()
    {
        pInfo.LossEnergy(Time.fixedDeltaTime);
    }

    public override void Reset()
    {
        // 重力和速度均设为0
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        pInfo.characterData.canShoot = false;
        transform.Find("Aim").gameObject.SetActive(false);
    }
}
