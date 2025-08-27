using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMgr : SingletonMono<PlayerCameraMgr>
{
    //控制的当前角色
    public GameObject Player;

    //当前主摄像机
    public GameObject MainCamera;

    public void DisablePlayerMove()
    {
        //设置不可动
        Player.GetComponent<PlayerController>().enabled = false;
    }

    public void EnablePlayerMove()
    {
        //设置不可动
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void DisableMainCamera()
    {
        MainCamera.GetComponent<Camera_SeePlayer>().enabled = false;
    }

    public void EnableMainCamera()
    {
        MainCamera.GetComponent<Camera_SeePlayer>().enabled = true;
    }

    public void PlayerBack(int x, int y, int z)
    {
        var position = Player.transform.position;
        position = new Vector3(
            position.x + x,
            position.y + y,
            position.z + z
        );
        Player.transform.position = position;
    }
}
