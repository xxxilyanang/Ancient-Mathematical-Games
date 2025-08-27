using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateEnum
{
    未配对,
    已配对,
}



public class Card : MonoBehaviour
{

    bool _rotating;//是否旋转

  public StateEnum CurrentState { get; private set; }
    Material _selfMat;

    public string FruitName { get; private set; }
    
    public void Initial(string fruitName)
    {
        FruitName = fruitName;
        _selfMat = GetComponent<MeshRenderer>().material;
        Material mat = transform.Find("Quad").GetComponent<MeshRenderer>().material;//找到Quad物件获取上面的Material
        Texture2D texture2D = Resources.Load<Texture2D>("Images/" + fruitName);//获取图片资源
        int index = Shader.PropertyToID("_MainTex");
        mat.SetTexture(index, texture2D);//给材质贴图贴到指定id上
    }




    public void SwitchState(StateEnum targetState)//状态改变
    {
        CurrentState = targetState;
    }




    public void Rotate(bool toFront = true)//tofront为true则为需要旋转到正面
    {
        if (_rotating || CurrentState == StateEnum.已配对)//如果卡片正在旋转或者当前状态为已配对则直接返回不做选择操作
        {
            return;
        }


        _rotating = true;//表示卡片正在旋转中

        GameManager.Instance.AddCard(this);

        StartCoroutine(RotateCor(toFront));//启动一个协程，调用 RotateCor 方法来执行卡片的旋转

    }


    IEnumerator RotateCor(bool toFront)
    {
        float workTime = 0;
        GameObject tmp = new GameObject();
        tmp.transform.rotation = transform.rotation;
        tmp.transform.RotateAround(tmp.transform.position, Vector3.up, 180);
        Quaternion originRot = transform.rotation;//起始角度
        Quaternion desRot = tmp.transform.rotation;//目标角度
        Destroy(tmp);


        while (true)
        {
            workTime += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(originRot, desRot, workTime);//平滑的进行旋转

            if (workTime >= 1)
            {
                break;
            }


            yield return null;
        }


        _rotating = false;//将正在旋转设置为false

        if (toFront)//需要旋转到内容的一面
        {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.CompareCards(this);
        }
        else//需要旋转到图标的一面
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.Clear();//对管理器中内容进行删除
        }

    }


    public void Highlight()
    {
        _selfMat.color = Color.white;
    }

    public void Normal()
    {
        _selfMat.color = Color.white;

    }
}
