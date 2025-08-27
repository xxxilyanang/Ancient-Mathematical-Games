using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTransfer : SingletonMono<CameraTransfer>
{
    public Animator animator;
    public Canvas yingCanvas;
    public float delayTime = 2f; // 2秒延迟

    void Start()
    {
        animator.enabled = false;
        // 在游戏开始时控制Canvas不显示，其没有SetActive属性
        yingCanvas.enabled = false;
    }
    //关闭当前脚本
    public void DisableCameraTransfer()
    {
        // 使用enabled属性激活Canvas
        yingCanvas.enabled = false;
        animator.enabled = false;
        animator.SetTrigger("camera");
        this.enabled = false;//设置当前脚本取消功能
        this.gameObject.GetComponent<Camera_SeePlayer>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;//使得人物获得移动权力
        OperationStateMgr.GetInstance().SwitchCursorState(false);//光标消失
    }
    public void EnableCameraTransfer()
    {
        this.enabled = true;//激活当前脚本
        // 使用enabled属性激活Canvas
        Invoke("ActivateYingCanvas", delayTime);//延时激活Canvas
        animator.enabled = true;
        animator.SetTrigger("camera");
    }
    void ActivateYingCanvas()
    {
        yingCanvas.enabled = true;
    }
}
