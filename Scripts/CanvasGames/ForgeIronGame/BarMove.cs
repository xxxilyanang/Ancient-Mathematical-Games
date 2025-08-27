using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    public float speed = 180.0f;
    public float maxX = 400.0f; // ���ݺ�����ȵ���

    private bool movingRight = true;

    void Update()
    {
        if (transform.localPosition.x > maxX) movingRight = false;
        if (transform.localPosition.x < -maxX) movingRight = true;

        transform.localPosition += (movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime;
    }
}
