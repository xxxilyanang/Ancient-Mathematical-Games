using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeDoorManDialogueController : MonoBehaviour
{
    //外部传入人物对应的当前对话
    public DialogueData_SO currentData;

    //用于切换观察NPC的摄像机
    public GameObject camera_SeeNPC;

    public  bool isTalk = true; 
    
    //当前是否处于可对话状态
    bool canTalk = false;
    void OnTriggerEnter(Collider other)
    {
        if (!isTalk||!other.CompareTag("Player") || currentData == null) return;
        //打开对话面板
        OpenDialogue();
            
        //禁止人物移动  和 摄像机跟随
        PlayerCameraMgr.GetInstance().DisablePlayerMove();
        PlayerCameraMgr.GetInstance().DisableMainCamera();

        //启动对应NPC上的摄像机，覆盖原本的摄像机
        camera_SeeNPC.SetActive(true);

        //解除光标锁定，并是其可见
        OperationStateMgr.GetInstance().SwitchCursorState(true);
        
        PlayerCameraMgr.GetInstance().PlayerBack(2,0,0);
    }
    
    void OpenDialogue()
    {
        //传输对话信息
        //打开对话面板
        OperationStateMgr.GetInstance().currentCamera = camera_SeeNPC;
        //DialogueUI.GetInstance().SetCurrentNPCCamera(camera_SeeNPC);//设置NPC摄像机
        DialogueUI.GetInstance().UpdateDialogueData(currentData);//初始化数据
        DialogueUI.GetInstance().UpdateMainDialogue(currentData.dialoguePieces[0]);
        // 获取所有挂载了特定脚本的GameObject
        // StartCoroutine(ExpendDialogueController.Enumerator(this,gameObject));
    }
}

