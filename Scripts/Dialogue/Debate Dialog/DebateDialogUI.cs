using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebateDialogUI : MonoBehaviour
{
    //�����������һ�䣩�İ�ť
    public Button btn_next;

    //����ṩ�۾ݵİ�ť��
    public Button btn_give;

    //���׷�ʰ�ť��
    public Button btn_ask;

    //����ѡ���۾ݵĻ�����
    public GameObject canvas_Select;

    // Start is called before the first frame update
    void Start()
    {
        btn_give?.onClick.AddListener(() =>
        {
            canvas_Select.SetActive(true);
        });
    }
    
}
