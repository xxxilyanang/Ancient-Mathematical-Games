using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaZhiPos : MonoBehaviour
{
    public MZPanel mZPanel;
    public GameObject maincamera;
    Camera_SeePlayer camera_SeePlayer;
    private void Start()
    {
       camera_SeePlayer=maincamera.GetComponent<Camera_SeePlayer>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mZPanel.enabled = true;
            Cursor.visible = true; // 强制显示鼠标光标
            Cursor.lockState = CursorLockMode.None; // 解锁鼠标
            Debug.Log("触碰到了触发点");
            mZPanel.gameObject.SetActive(true);
            camera_SeePlayer.enabled = false;
        }
    }
}
