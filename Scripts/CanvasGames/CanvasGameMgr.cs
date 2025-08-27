using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameMgr : SingletonMono<CanvasGameMgr>
{
    //һЩcanvasС��Ϸ
    public GameObject ForgeIronGame;

    public GameObject MaZhiGame;

    [HideInInspector]
    public bool isEnableForgeIronGame;
    [HideInInspector]
    public bool isEnableMaZhiGame;
    [HideInInspector]
    public bool isEnableYingBuZuGame;

    // Update is called once per frame
    void Update()
    {
        if (isEnableForgeIronGame)
        {
            isEnableForgeIronGame = false;//����һ�ι��������Ϊfalse
            ForgeIronGame.SetActive(true);
            //������Ϸ����Ҫ������Ϸ״̬!!
            ForgeIronGameUI.GetInstance().ResetGameState();
        }else if (isEnableMaZhiGame)
        {
            isEnableMaZhiGame = false;
            //��������
            MaZhiGame.SetActive(true);//���û���Ϊ�ɼ�
        }else if (isEnableYingBuZuGame)
        {
            isEnableYingBuZuGame = false;
            //�͵���
            CameraTransfer.GetInstance().EnableCameraTransfer();
        }
    }

    //������Ͻ�����Ҫ��̬�仯!
 /*   public void DisableNowGameCanvas()
    {
        isEnableForgeIronGame = false;
        forgeIronGame.SetActive(isEnableForgeIronGame);
    }*/
}
