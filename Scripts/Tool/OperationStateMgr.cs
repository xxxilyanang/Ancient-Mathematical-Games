using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationStateMgr : SingletonMono<OperationStateMgr>
{
    private GameObject nowCamera;
    public GameObject currentCamera
    {
        set
        {
            nowCamera = value;
        }

        get { return nowCamera; }
    }

    public void DisableNowDialogueCamera()
    {
        nowCamera.SetActive(false);
    }

    public void EnableNowDialogueCamera()
    {
        nowCamera.SetActive(true);
    }

    public void SwitchCursorState(bool isActive)
    {
        //如果不激活
        if (!isActive)
        {
            Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标光标在屏幕中央
            Cursor.visible = false; // 隐藏鼠标光标
        }else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
