using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    //玩家相撞的碰撞系数，受体型影响
    public float bounceForce = 1;

    //点
    public Transform pointGrow;

    //定义玩家生命
    public int playerHP1 = 3, playerHP2 = 3, playerHP3 = 3, playerHP4 = 3;
    public Text HPText1, HPText2, HPText3, HPText4;

    //碰撞事件
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall_Disappear")
        {
            GetComponent<Rigidbody>().AddForce(bounceForce * (GetComponent<Transform>().position - collision.gameObject.GetComponent<Transform>().position));
            Debug.Log("撞到墙体! 反弹方向：" + (GetComponent<Transform>().position - collision.gameObject.GetComponent<Transform>().position) + "，反弹力：" + bounceForce * (GetComponent<Transform>().position - collision.gameObject.GetComponent<Transform>().position));
            Destroy(collision.gameObject);
            //Debug.Log("摧毁墙体!");
        }
    }

    //触发器
    private void OnTriggerEnter(Collider trigger)
    {
        switch (trigger.gameObject.tag)
        {
            //玩家死亡
            case "Death_Plat":
                //重置玩家属性
                transform.position = new Vector3(0, 0, 0.5f);
                transform.localScale = new Vector3(1f, 0.5f, 1f);
                GetComponent<Rigidbody>().mass = 1;
                switch (name)
                {
                    case "Player 1":
                        GetComponent<PlayerController1>().speedDecayMultiper = 0.6f;
                        break;
                    case "Player 2":
                        GetComponent<PlayerController2>().speedDecayMultiper = 0.6f;
                        break;
                    case "Player 3":
                        GetComponent<PlayerController3>().speedDecayMultiper = 0.6f;
                        break;
                    case "Player 4":
                        GetComponent<PlayerController4>().speedDecayMultiper = 0.6f;
                        break;
                    default:
                        break;
                }
                Debug.Log("[" + name + "]挂掉了!");

                //扣除生命
                switch (name)
                {
                    case "Player 1":
                        playerHP1--;
                        if (playerHP1 <= 0)
                        {
                            HPText1.text = "已死亡!";
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            HPText1.text = "生命：" + playerHP1;
                        }
                        break;
                    case "Player 2":
                        playerHP2--;
                        if (playerHP2 <= 0)
                        {
                            HPText2.text = "已死亡!";
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            HPText2.text = "生命：" + playerHP2;
                        }
                        break;
                    case "Player 3":
                        playerHP3--;
                        if (playerHP3 <= 0)
                        {
                            HPText3.text = "已死亡!";
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            HPText3.text = "生命：" + playerHP3;
                        }
                        break;
                    case "Player 4":
                        playerHP4--;
                        if (playerHP4 <= 0)
                        {
                            HPText4.text = "已死亡!";
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            HPText4.text = "生命：" + playerHP4;
                        }
                        break;
                    default:
                        break;
                }
                break;
            //生成成长点
            case "Point":
                Debug.Log("吃点成功!");
                pointGrow.position = new Vector3(10, (float)0.25, -15);
                if (GetComponent<Rigidbody>().mass < 3)
                {
                    GetComponent<Rigidbody>().mass += 0.3f;
                }
                if (transform.localScale.x < 2.5)
                {
                    transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y, transform.localScale.z * 1.2f);
                }

                //重设速度衰减
                if (GetComponent<PlayerController1>().speedDecayMultiper * (float)1.08 < 1)
                {
                    GetComponent<PlayerController1>().speedDecayMultiper *= (float)1.08;
                }
                Debug.Log("NEW: " + GetComponent<PlayerController1>().speedDecayMultiper);

                break;
            default:
                break;
        }
    }
}
