using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBagUI : MonoBehaviour
{
    //���ȡ���İ�ť
    public Button btn_close;
    
    //--------------------------------private-----------------------------
    //��ǰUI�Ķ���
    private Animator canvas_animator;

    //�رն���ִ����󣬸ò�����־��ʱӦ�ùرո������
    private bool isShouldClose;

    private void Awake()
    {
        canvas_animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        btn_close.onClick.AddListener(() =>
        {
            //����ǰ�����ر�
            canvas_animator.SetBool("isClose", true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //Ӧ�ý��Լ�ʧ����
        if (isShouldClose)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ChangeIsCloseCanvas()
    {
        isShouldClose = true;
    }
}
