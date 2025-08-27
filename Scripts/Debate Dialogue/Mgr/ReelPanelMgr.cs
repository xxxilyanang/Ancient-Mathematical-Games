using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelPanelMgr : SingletonMono<ReelPanelMgr>
{
    //用于唤起显示数学知识书籍内容的卷轴Panel
    public GameObject reelPanel = null;

    public void UpdateReelContent(string txt) 
    {
        ReelPanelUI.GetInstance().SetReelContent(txt);
    }
}
