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
        //���������
        if (!isActive)
        {
            Cursor.lockState = CursorLockMode.Locked; // �������������Ļ����
            Cursor.visible = false; // ���������
        }else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
