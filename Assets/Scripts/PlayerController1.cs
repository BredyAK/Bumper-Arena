using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public Rigidbody rb;

    //定义初始参数
    public float rollForce = 68f;
    public float spinForce = 230f;
    public float accelerationMultiper = 2.5f;
    public float maxRollSpeed = 150f;
    public float speedDecayMultiper = 0.6f;

    //速度衰减周期
    private float speedDecayTime = 0.2f;

    //受撞击后暂停
    private float lastHitTime = 3f; //上次撞击时间，可作为游戏开始时所有玩家暂停数秒
    private float stayTime = 0.2f; //撞击后需等待时间

    //计时器
    private float speedTimer = 0;

    void FixedUpdate()
    {
        //Player1通过W推进，Player2通过Space推进，Player3通过ArrowUp推进，Player4通过Num0推进
        if (Input.GetKey(KeyCode.Q) && lastHitTime > stayTime)
        {
            if (rb.velocity.sqrMagnitude < maxRollSpeed)
            {
                rb.AddForce(transform.forward * rollForce * accelerationMultiper);
            }
        }
        else if (lastHitTime > stayTime)
        {
            transform.Rotate(0, spinForce * Time.deltaTime, 0);
        }
        
        if (speedTimer >= speedDecayTime)
        {
            rb.velocity *= speedDecayMultiper;
            speedTimer = 0;
        }

        //计时器
        lastHitTime += Time.deltaTime; //撞击后刷新
        speedTimer += Time.deltaTime;
    }

    //碰撞事件
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            lastHitTime = 0;
        }
    }
}
