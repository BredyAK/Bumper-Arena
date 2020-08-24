using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class PointGenerator : MonoBehaviour
{
    //计时器相关
    public Text timerText;
    private float totalCount = 0;
    private float playCount = 0;
    private float secondCount;
    private int minuteCount;
    private int hourCount;

    //阶段相关
    public Text stageText;
    public int stageOne = 7; //阶段一开始时间
    public int stageTwo = 20; //阶段二开始时间
    public int delayPointGrow = 7; //成长点刷新延迟
    private float tempTimeOne = 0;
    private int numPointGrow = 0;

    //刷新点位置
    public Transform pointGrow;

    void Start()
    {
        //清空调试信息
        ClearLog();

        pointGrow.position = new Vector3(-10, (float)0.25, -15);
    }

    void Update()
    {
        //计时器
        secondCount += Time.deltaTime; //计时器秒数
        totalCount += Time.deltaTime; //真实秒数
        playCount = totalCount - stageOne; //真实秒数去掉热身时间剩余秒数
        timerText.text = hourCount + "h:" + minuteCount + "m:" + (int)secondCount + "s";
        if (secondCount >= 60)
        {
            minuteCount++;
            secondCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }

        //第一阶段：第7秒开始，变大点出现
        if (playCount >= 0)
        {
            tempTimeOne += Time.deltaTime;
            stageText.text = "阶段：1";
            if (tempTimeOne >= 1f)
            {
                if ((int)playCount % delayPointGrow == 0)
                {
                    //Debug.Log("尝试调用函数!");
                    GeneratePoint("grow");
                }
                //Debug.Log("成长点现位于" + pointGrow.position + "的位置!");
                tempTimeOne = 0;
            }
        }
    }

    void GeneratePoint(string point)
    {
        int x = Random.Range(-6, 6);
        int y = Random.Range(-6, 6);
        //Debug.Log("生成随机数" + x + ", " + y + "!");
        switch (point)
        {
            case "grow":
                pointGrow.position = new Vector3(x * (float)1.5, 0, y * (float)1.5);
                numPointGrow++;
                Debug.Log("已生成第" + numPointGrow + "个<color=#FF0000>成长点</color>!");
                break;
        }
    }

    //清空调试信息
    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}