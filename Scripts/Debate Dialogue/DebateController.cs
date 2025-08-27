using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DebateController : MonoBehaviour
{
    //外部传入的Canvas控件，用于人物接近NPC时，显示该Canvas
    public GameObject canvas_ShowInputTip;

    //用于人物对话的面板
    //public GameObject canvas_TaskCanvas;

    //用于切换观察NPC的摄像机
    public GameObject camera_SeeNPC;

    //外部传入人物对应的当前对话
    public DebateData_SO currentData;

    //当前是否处于可对话状态
    private bool canTalk = false;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentData != null)
        {
            canvas_ShowInputTip.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*        //任务处于未接取状态时按下F键
                if (Input.GetKeyDown(KeyCode.F) && !taskState && CollidedPlayer != null)
                {
                    //设置当前任务状态，防止重复点击
                    taskState = true;
                    //确认后关闭提示
                    canvas_ShowInputTip.SetActive(false);

                    //触发事件：人物禁止移动，摄像机禁止跟随，唤起光标

                    //人物禁止移动
                    CollidedPlayer.GetComponent<PlayerController>().enabled = false;

                    //摄像机的移动取消
                    GameObject.Find("Main Camera").GetComponent<Camera_SeePlayer>().enabled = false;

                    //切换新的摄像机
                    camera_SeeOlder.SetActive(true);

                    //设置光标状态
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //打开人物对话面板
                    canvas_TaskCanvas.SetActive(true);
                }*/
        if (canTalk && Input.GetKeyDown(KeyCode.F))
        {
            //确认后关闭提示面板
            canvas_ShowInputTip.SetActive(false);
            //打开对话面板
            OpenDialogue();

            //禁止人物移动  和 摄像机跟随
            PlayerCameraMgr.GetInstance().DisablePlayerMove();
            PlayerCameraMgr.GetInstance().DisableMainCamera();

            //启动对应NPC上的摄像机，覆盖原本的摄像机
            camera_SeeNPC.SetActive(true);

            //解除光标锁定，并是其可见
            OperationStateMgr.GetInstance().SwitchCursorState(true);
        }
    }
    private void OnTriggerExit(Collider other)
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
        OperationStateMgr.GetInstance().currentCamera = camera_SeeNPC;
        DebateDialogueUI.GetInstance().UpdateDebateData(currentData);//初始化数据
        DebateDialogueUI.GetInstance().UpdateMainDebate(currentData.debatePieces[0]);
    }
}
