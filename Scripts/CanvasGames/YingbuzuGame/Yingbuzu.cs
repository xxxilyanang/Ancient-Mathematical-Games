using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Yingbuzu : MonoBehaviour
{
    public Text paperText;//十九份麻纸
    public Text bambooText;//零份桂竹
    public Button buChangepaper;//兑换麻纸
    public Button buChangebamboo;//兑换桂竹
    public Button buSubmit;//提交
    public Button buCanel;//关闭提示面板
    public Text textTips;          // 提示文本
    private bool isOver;       // 游戏是否完成
    public GameObject tipspanel;//提示面板
    public GameObject curCanvas;    // 当前画布

    private int paperCount = 19;
    private int bambooCount = 0;
    // 定义一个字典，用于存储阿拉伯数字和对应的中文汉字
    private static Dictionary<int, string> numberMap = new Dictionary<int, string>()
    {
        { 0, "零" },{ 1, "一" },{ 2, "二" },{ 3, "三" },{ 4, "四" },{ 5, "五" },{ 6, "六" },{ 7, "七" },
        { 8, "八" },{ 9, "九" },{ 10, "十" },{ 11, "十一" },{ 12, "十二" },{ 13, "十三" },
        { 14, "十四" },{ 15, "十五" },{ 16, "十六" },{ 17, "十七" },{ 18, "十八" },{ 19, "十九" },
    };
    void Start()
    {
        // 初始化文本显示
        UpdateText();
        //兑换麻纸和桂竹
        buChangepaper.onClick.AddListener(BuChangepaperOnclick);
        buChangebamboo.onClick.AddListener(BuChangebambooOnclick);
        //提交按钮
        buSubmit.onClick.AddListener(BuSubmitOnclick);
        //关闭按钮
        buCanel.onClick.AddListener(BuCancelOnclick);
    }

    void UpdateText()
    {
        paperText.text = ConvertNumber(paperCount) + "份麻纸";
        bambooText.text = ConvertNumber(bambooCount) + "份桂竹";
    }

    void ExchangePaper()
    {
        if (paperCount >= 2)
        {
            paperCount -= 2;
            bambooCount += 3;
            UpdateText();
        }
    }
    void ExchangeBamboo()
    {
        if (bambooCount >= 3)
        {
            bambooCount -= 3;
            paperCount += 2;
            UpdateText();
        }
    }
    void Submit()
    {
        if (paperCount == 9 && bambooCount == 15)
        {
            Debug.Log("提交成功!");

        }
        else
        {
            Debug.Log("提交失败，请重新提交");
            paperCount = 19;
            bambooCount = 0;
            UpdateText();
        }
    }
    // 将阿拉伯数字转换为中文汉字
    string ConvertNumber(int number)
    {
        if (numberMap.ContainsKey(number))
        {
            return numberMap[number];
        }
        else
        {
            return "未知";
        }
    }
    void BuChangepaperOnclick()
    {
        ExchangePaper();
    }
    void BuChangebambooOnclick()
    {
        ExchangeBamboo();
    }
    void BuSubmitOnclick()
    {
        if (paperCount == 9 && bambooCount == 15)
        {
            textTips.text = "恭喜你，运用了盈不足的方法成功解开了刘徽的麻纸桂竹灯笼谜题题，你成功了！";
            tipspanel.SetActive(true);
            isOver = true;
        }
        else
        {
            textTips.text = "答案不对哦，再想想吧，可以运用盈不足的方法！";
            tipspanel.SetActive(true);
        }
    }
    void BuCancelOnclick()
    {
        tipspanel.SetActive(false);
        if (isOver)
        {
            //curCanvas.SetActive(false);
            // 获取 摄像头移动脚本 组件，关闭
            CameraTransfer.GetInstance().DisableCameraTransfer();
        }
    }

}

