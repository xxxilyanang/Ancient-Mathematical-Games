using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DebateNPCController : MonoBehaviour
{
    //�ⲿ�����Canvas�ؼ�����������ӽ�NPCʱ����ʾ��Canvas
    public GameObject canvas_ShowInputTip;

    //��������Ի������
    public GameObject canvas_TaskCanvas;

    //�����л��۲����˵������
    public GameObject camera_SeeOlder;

    //------------------------------private-------------------------
    //NPC��ײ�Ľ�ɫ����
    private GameObject CollidedPlayer;

    //��¼�Ƿ��Ѿ���������ǰ�¼�:
    private bool taskState = false;

    private void OnTriggerEnter(Collider other)
    {
        //��ɫ�������ʾ��ʾ
        if (other.CompareTag("Player"))
        {
            canvas_ShowInputTip.SetActive(true);
            //�������Ľ�ɫ��ֵ
            CollidedPlayer = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //������δ��ȡ״̬ʱ����F��
        if (Input.GetKeyDown(KeyCode.F) && !taskState && CollidedPlayer != null)
        {
            //���õ�ǰ����״̬����ֹ�ظ����
            taskState = true;
            //ȷ�Ϻ�ر���ʾ
            canvas_ShowInputTip.SetActive(false);

            //�����¼��������ֹ�ƶ����������ֹ���棬������

            //�����ֹ�ƶ�
            CollidedPlayer.GetComponent<PlayerController>().enabled = false;

            //��������ƶ�ȡ��
            GameObject.Find("Main Camera").GetComponent<Camera_SeePlayer>().enabled = false;

            //�л��µ������
            camera_SeeOlder.SetActive(true);

            //���ù��״̬
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //������Ի����
            canvas_TaskCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas_ShowInputTip.SetActive(false);
    }
}
