using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameMgr : SingletonMono<CanvasGameMgr>
{
    //一些canvas小游戏
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
            isEnableForgeIronGame = false;//启动一次过后就设置为false
            ForgeIronGame.SetActive(true);
            //启动游戏后，需要重置游戏状态!!
            ForgeIronGameUI.GetInstance().ResetGameState();
        }else if (isEnableMaZhiGame)
        {
            isEnableMaZhiGame = false;
            //其他设置
            MaZhiGame.SetActive(true);//设置画板为可见
        }else if (isEnableYingBuZuGame)
        {
            isEnableYingBuZuGame = false;
            //就调用
            CameraTransfer.GetInstance().EnableCameraTransfer();
        }
    }

    //这个不严谨，需要动态变化!
 /*   public void DisableNowGameCanvas()
    {
        isEnableForgeIronGame = false;
        forgeIronGame.SetActive(isEnableForgeIronGame);
    }*/
}
