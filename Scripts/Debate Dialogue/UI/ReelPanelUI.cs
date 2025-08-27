using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelPanelUI : SingletonMono<ReelPanelUI>
{
    public Text bookInfo;

    public void SetReelContent(string bookInfo)
    {
        this.bookInfo.text = bookInfo;
    }

    private void OnEnable()
    {
        
    }


    //用不到的代码
    public void SetValue_isCloseSubmitPanel()
    {
        SubmitUI.GetInstance().SetSubmitPanelFalse();
    }

}
