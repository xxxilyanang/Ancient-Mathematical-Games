using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookOnclickEvent : MonoBehaviour
{
    //打开典籍按钮
    public Button btn_book1;
    public Button btn_book2;
    public Button btn_book3;
    //用于展示典籍内容的画布：
    public GameObject canvas_Content;
    void Start()
    {
        // 添加点击事件监听器
        btn_book1.onClick.AddListener(OnClick);
        btn_book2.onClick.AddListener(OnClick);
        btn_book3.onClick.AddListener(OnClick);
    }
    // 点击事件处理方法
    void OnClick()
    {
        canvas_Content.SetActive(true);
    }

}
