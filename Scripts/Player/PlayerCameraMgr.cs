using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMgr : SingletonMono<PlayerCameraMgr>
{
    //���Ƶĵ�ǰ��ɫ
    public GameObject Player;

    //��ǰ�������
    public GameObject MainCamera;

    public void DisablePlayerMove()
    {
        //���ò��ɶ�
        Player.GetComponent<PlayerController>().enabled = false;
    }

    public void EnablePlayerMove()
    {
        //���ò��ɶ�
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
