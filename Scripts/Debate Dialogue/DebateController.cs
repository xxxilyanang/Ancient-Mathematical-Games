using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DebateController : MonoBehaviour
{
    //�ⲿ�����Canvas�ؼ�����������ӽ�NPCʱ����ʾ��Canvas
    public GameObject canvas_ShowInputTip;

    //��������Ի������
    //public GameObject canvas_TaskCanvas;

    //�����л��۲�NPC�������
    public GameObject camera_SeeNPC;

    //�ⲿ���������Ӧ�ĵ�ǰ�Ի�
    public DebateData_SO currentData;

    //��ǰ�Ƿ��ڿɶԻ�״̬
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
        /*        //������δ��ȡ״̬ʱ����F��
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
                }*/
        if (canTalk && Input.GetKeyDown(KeyCode.F))
        {
            //ȷ�Ϻ�ر���ʾ���
            canvas_ShowInputTip.SetActive(false);
            //�򿪶Ի����
            OpenDialogue();

            //��ֹ�����ƶ�  �� ���������
            PlayerCameraMgr.GetInstance().DisablePlayerMove();
            PlayerCameraMgr.GetInstance().DisableMainCamera();

            //������ӦNPC�ϵ������������ԭ���������
            camera_SeeNPC.SetActive(true);

            //������������������ɼ�
            OperationStateMgr.GetInstance().SwitchCursorState(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
            //�����뿪��ΧҲ����ʾ�������
            canvas_ShowInputTip.SetActive(false);
        }
    }
    void OpenDialogue()
    {
        OperationStateMgr.GetInstance().currentCamera = camera_SeeNPC;
        DebateDialogueUI.GetInstance().UpdateDebateData(currentData);//��ʼ������
        DebateDialogueUI.GetInstance().UpdateMainDebate(currentData.debatePieces[0]);
    }
}
