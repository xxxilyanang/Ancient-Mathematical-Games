using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DebateNPCController : MonoBehaviour
{
    //外部传入的Canvas控件，用于人物接近NPC时，显示该Canvas
    public GameObject canvas_ShowInputTip;

    //用于人物对话的面板
    public GameObject canvas_TaskCanvas;

    //用于切换观察老人的摄像机
    public GameObject camera_SeeOlder;

    //------------------------------private-------------------------
    //NPC碰撞的角色对象
    private GameObject CollidedPlayer;

    //记录是否已经触发过当前事件:
    private bool taskState = false;

    private void OnTriggerEnter(Collider other)
    {
        //角色进入后，显示提示
        if (other.CompareTag("Player"))
        {
            canvas_ShowInputTip.SetActive(true);
            //将碰到的角色赋值
            CollidedPlayer = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //任务处于未接取状态时按下F键
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas_ShowInputTip.SetActive(false);
    }
}
