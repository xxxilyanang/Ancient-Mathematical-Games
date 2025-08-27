using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MZPanel : MonoBehaviour
{
    public Button btnClose;
    //public GameObject maincamera;
    Camera_SeePlayer camera_SeePlayer;


    private void Start()
    {
        GameObject maincamera = GameObject.Find("Main Camera");
        camera_SeePlayer= maincamera.GetComponent<Camera_SeePlayer>();
        //this.gameObject.SetActive(true);
       // camera_SeePlayer = maincamera.GetComponent<Camera_SeePlayer>();
        btnClose.onClick.AddListener(OnClick);
        

    }
   
    void OnClick()
    {
        //this.gameObject.SetActive(false);
        DestroyUIGame();
        Cursor.visible = false; // Òþ²ØÊó±ê¹â±ê
        camera_SeePlayer.enabled = true;
        camera_SeePlayer.RestoreRecordedPositionAndRotation();
        

    }
    void DestroyUIGame()
    {
        Destroy(gameObject);
        GameObject objectToDestroy = GameObject.Find("CardManager");

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }

  

}
