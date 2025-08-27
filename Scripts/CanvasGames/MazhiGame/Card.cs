using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateEnum
{
    δ���,
    �����,
}



public class Card : MonoBehaviour
{

    bool _rotating;//�Ƿ���ת

  public StateEnum CurrentState { get; private set; }
    Material _selfMat;

    public string FruitName { get; private set; }
    
    public void Initial(string fruitName)
    {
        FruitName = fruitName;
        _selfMat = GetComponent<MeshRenderer>().material;
        Material mat = transform.Find("Quad").GetComponent<MeshRenderer>().material;//�ҵ�Quad�����ȡ�����Material
        Texture2D texture2D = Resources.Load<Texture2D>("Images/" + fruitName);//��ȡͼƬ��Դ
        int index = Shader.PropertyToID("_MainTex");
        mat.SetTexture(index, texture2D);//��������ͼ����ָ��id��
    }




    public void SwitchState(StateEnum targetState)//״̬�ı�
    {
        CurrentState = targetState;
    }




    public void Rotate(bool toFront = true)//tofrontΪtrue��Ϊ��Ҫ��ת������
    {
        if (_rotating || CurrentState == StateEnum.�����)//�����Ƭ������ת���ߵ�ǰ״̬Ϊ�������ֱ�ӷ��ز���ѡ�����
        {
            return;
        }


        _rotating = true;//��ʾ��Ƭ������ת��

        GameManager.Instance.AddCard(this);

        StartCoroutine(RotateCor(toFront));//����һ��Э�̣����� RotateCor ������ִ�п�Ƭ����ת

    }


    IEnumerator RotateCor(bool toFront)
    {
        float workTime = 0;
        GameObject tmp = new GameObject();
        tmp.transform.rotation = transform.rotation;
        tmp.transform.RotateAround(tmp.transform.position, Vector3.up, 180);
        Quaternion originRot = transform.rotation;//��ʼ�Ƕ�
        Quaternion desRot = tmp.transform.rotation;//Ŀ��Ƕ�
        Destroy(tmp);


        while (true)
        {
            workTime += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(originRot, desRot, workTime);//ƽ���Ľ�����ת

            if (workTime >= 1)
            {
                break;
            }


            yield return null;
        }


        _rotating = false;//��������ת����Ϊfalse

        if (toFront)//��Ҫ��ת�����ݵ�һ��
        {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.CompareCards(this);
        }
        else//��Ҫ��ת��ͼ���һ��
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.Clear();//�Թ����������ݽ���ɾ��
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
