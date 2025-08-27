using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BookItemHolderUI : MonoBehaviour
{
    //�����µ�������ʾ���
    public Image icon = null;

    public Text description = null;

    //�ⲿ������鼮������Я������Ϣ��
    private Book_SO bookInfo;

    //����İ�ť����
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

    //�򿪶�Ӧ���鼮����ϸ��Ϣ���
    public void OpenReelCanvas()
    {
        //��ʧ��󼤻�Ա�ÿ�ε�����ܴ�����������
        ReelPanelMgr.GetInstance().reelPanel.SetActive(false);
        ReelPanelMgr.GetInstance().reelPanel.SetActive(true);
        ReelPanelMgr.GetInstance().UpdateReelContent(bookInfo.BookDescription);
        //����ĸ�ѡ��ͰѴ�ѡ���Ӧ���鼮���Ƽ�¼����
        SubmitCanvasUI.GetInstance().submitBookName = bookInfo.BookName;
    }
}
