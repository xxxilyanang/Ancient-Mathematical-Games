using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BookItemHolderUI : MonoBehaviour
{
    //容器下的两个显示组件
    public Image icon = null;

    public Text description = null;

    //外部传入的书籍【此项携带的信息】
    private Book_SO bookInfo;

    //自身的按钮引用
    //private Button myButton;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenReelCanvas);
    }

    public void InitBookItem(Book_SO book_SO)
    {
        bookInfo = book_SO;
        SetupBookItemUI(bookInfo);
    }

    private void SetupBookItemUI(Book_SO item)
    {
        if (item != null)
        {
            icon.sprite = item.BookImage;
            description.text = item.BookBrief;
        }
    }

    //打开对应该书籍的详细信息面板
    public void OpenReelCanvas()
    {
        //先失活后激活，以便每次点击都能触发开启动画
        ReelPanelMgr.GetInstance().reelPanel.SetActive(false);
        ReelPanelMgr.GetInstance().reelPanel.SetActive(true);
        ReelPanelMgr.GetInstance().UpdateReelContent(bookInfo.BookDescription);
        //点击哪个选项，就把此选项对应的书籍名称记录下来
        SubmitCanvasUI.GetInstance().submitBookName = bookInfo.BookName;
    }
}
