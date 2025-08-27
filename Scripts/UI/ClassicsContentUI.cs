using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassicsContentUI : MonoBehaviour
{
    //点击取消的按钮
    public Button btn_close;

    //--------------------------------private-----------------------------
    //当前UI的动画
    private Animator canvas_animator;

    //关闭动画执行完后，该参数标志此时应该关闭该面板了
    private bool isShouldClose;

    private void Awake()
    {
        canvas_animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 处理按键事件
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("8");
            //将当前画布关闭
            canvas_animator.SetBool("isClose", true);
        };
    }

    // Update is called once per frame
    void Update()
    {
        //应该将自己失活了
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
