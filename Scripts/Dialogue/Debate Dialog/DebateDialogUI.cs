using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebateDialogUI : MonoBehaviour
{
    //点击继续（下一句）的按钮
    public Button btn_next;

    //点击提供论据的按钮：
    public Button btn_give;

    //点击追问按钮：
    public Button btn_ask;

    //用于选择论据的画布：
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
