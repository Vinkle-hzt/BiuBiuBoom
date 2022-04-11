using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO-DO：完成入侵和处决 (入侵键位 Fire1, 处决键位 Fire2)
public class StateShadow : State
{
    private Rigidbody2D rb;
    private Vector3 position;
    public StateShadow(Transform body, PlayerInfo pInfo) : base(body, pInfo)
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
        transform.position += position * pInfo.speed * Time.deltaTime * 1.5f;
    }

    void MoveCheck()
    {
        position.x = Input.GetAxis("Horizontal");
        position.y = Input.GetAxis("Vertical");
    }

    void LossEnergy()
    {
        pInfo.energy = Mathf.Max(pInfo.energy - pInfo.energyLoss * Time.fixedDeltaTime, 0);
    }

    public override void Reset()
    {
        // 重力和速度均设为0
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        pInfo.canShoot = false;
    }
}
