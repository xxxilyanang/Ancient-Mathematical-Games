using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelPanelMgr : SingletonMono<ReelPanelMgr>
{
    //���ڻ�����ʾ��ѧ֪ʶ�鼮���ݵľ���Panel
    public GameObject reelPanel = null;

    public void UpdateReelContent(string txt) 
    {
        ReelPanelUI.GetInstance().SetReelContent(txt);
    }
}
