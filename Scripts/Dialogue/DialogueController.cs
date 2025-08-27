using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    //外部传入人物对应的当前对话
    public DialogueData_SO currentData;

    //外部传入的Canvas控件，用于人物接近NPC时，显示该Canvas
    public GameObject canvas_ShowInputTip;

    //用于切换观察NPC的摄像机
    public GameObject camera_SeeNPC;

    //当前是否处于可对话状态
    bool canTalk = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentData != null)
        {
            canvas_ShowInputTip.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canTalk && Input.GetKeyDown(KeyCode.F))
        {
            //确认后关闭提示面板
            canvas_ShowInputTip.SetActive(false);
            //打开对话面板
            OpenDialogue();
            
            //禁止人物移动  和 摄像机跟随
            PlayerCameraMgr.GetInstance().DisablePlayerMove();
            PlayerCameraMgr.GetInstance().DisableMainCamera();
            if(camera_SeeNPC != null)
            {
                //启动对应NPC上的摄像机，覆盖原本的摄像机
                camera_SeeNPC.SetActive(true);
            }

            //解除光标锁定，并是其可见
            OperationStateMgr.GetInstance().SwitchCursorState(true);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
            //人物离开范围也将提示面板消除
            canvas_ShowInputTip.SetActive(false);
        }
    }

    void OpenDialogue()
    {
        //传输对话信息
        //打开对话面板
        OperationStateMgr.GetInstance().currentCamera = camera_SeeNPC;
        //DialogueUI.GetInstance().SetCurrentNPCCamera(camera_SeeNPC);//设置NPC摄像机
        DialogueUI.GetInstance().UpdateDialogueData(currentData);//初始化数据
        DialogueUI.GetInstance().UpdateMainDialogue(currentData.dialoguePieces[0]);
    }
}
