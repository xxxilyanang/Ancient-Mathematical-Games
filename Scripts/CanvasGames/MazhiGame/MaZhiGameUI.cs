using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaZhiGameUI : MonoBehaviour
{
    public Button btnClose;
    // Start is called before the first frame update
    void Start()
    {
        btnClose.onClick.AddListener(switchGameState);
    }
/*    void OnClick()
    {
        //this.gameObject.SetActive(false);
        DestroyUIGame();
        Cursor.visible = false; // ���������
        camera_SeePlayer.enabled = true;
        camera_SeePlayer.RestoreRecordedPositionAndRotation();
    }*/

    private void switchGameState()
    {
        //CanvasGameMgr.GetInstance().DisableNowGameCanvas();
        this.gameObject.SetActive(false);//����ֱ�ӣ�������Ӱ�ĺ���������������
        PlayerCameraMgr.GetInstance().EnableMainCamera();
        PlayerCameraMgr.GetInstance().EnablePlayerMove();
        OperationStateMgr.GetInstance().DisableNowDialogueCamera();
        OperationStateMgr.GetInstance().SwitchCursorState(false);
    }
/*    void DisableUIGame()
    {
        Destroy(gameObject);
        GameObject objectToDestroy = GameObject.Find("CardManager");

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }*/
}
