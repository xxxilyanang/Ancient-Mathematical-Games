using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class SmashInteraction : MonoBehaviour
{
    public Image image0;             // 粉碎机0图片
    public Animator smashAnimator0;   // 粉碎机0的Animator
    public Image image1;             // 粉碎机1图片
    public Animator smashAnimator1;   // 粉碎机1的Animator
    public Image image2;             // 粉碎机2图片
    public Animator smashAnimator2;   // 粉碎机2的Animator

    private GameObject draggedObject; // 当前拖动的对象
    private Vector3 initialPosition;  // 图片1的初始位置
    private RectTransform canvasRectTransform; // 画布的RectTransform组件
    private bool isOver;       // 游戏是否完成
    public GameObject tipspanel;//提示面板
    public Button Bucancel;//关闭提示面板按钮
    public Button Busubmit;//提交按钮
    //文本更新相关
    public Text textinput;           // 输入文本
    public Text textTips;          // 提示文本
    public Text textsurplus0;          // 剩余材料文本
    public Text textsurplus1;
    public Text textsurplus2;
    public GameObject curCanvas;    // 当前画布
    private bool enterimage0 = true;       // 是否进入图片1
    private bool enterimage1 = true;       // 是否进入图片0
    private bool enterimage2 = true;       // 是否进入图片2


    void Start()
    {
        // 获取画布的RectTransform组件
        canvasRectTransform = GetComponent<RectTransform>();
        //关闭按钮
        Bucancel.onClick.AddListener(BuCancelOnclick);
        //提交按钮
        Busubmit.onClick.AddListener(BuSubmitOnclick);
    }

    void Update()
    {
        // 如果鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标下的对象
            draggedObject = GetObjectUnderMouse();

            if ((draggedObject != null) && draggedObject.CompareTag("canmove"))
            {
                // 如果鼠标下的对象是Image类型
                Image image = draggedObject.GetComponent<Image>();
                if (image != null)
                {
                    // 记录图片1的初始位置
                    initialPosition = draggedObject.transform.position;
                }
            }
        }

        // 如果鼠标左键松开
        if (Input.GetMouseButtonUp(0))
        {
            // 如果鼠标下的对象是Image类型
            Image image = draggedObject.GetComponent<Image>();
            if ((image != null) && draggedObject.CompareTag("canmove"))
            {
                // 检查鼠标下的对象是否在图片2的范围内
                if (IsOverlapping(image.rectTransform, image1.rectTransform) && enterimage1)
                {
                    // 隐藏拖动的对象
                    draggedObject.SetActive(false);
                    // 播放粉碎机动画
                    smashAnimator1.SetTrigger("isPlaying");
                    Invoke("GrayOut1", 2.2f);
                    enterimage1 = false;
                }
                else if (IsOverlapping(image.rectTransform, image0.rectTransform) && enterimage0)
                {
                    // 隐藏拖动的对象
                    draggedObject.SetActive(false);
                    // 播放粉碎机动画
                    smashAnimator0.SetTrigger("isPlaying");
                    Invoke("GrayOut0", 2.2f);
                    enterimage0 = false;
                }
                else if (IsOverlapping(image.rectTransform, image2.rectTransform) && enterimage2)
                {
                    // 隐藏拖动的对象
                    draggedObject.SetActive(false);
                    // 播放粉碎机动画
                    smashAnimator2.SetTrigger("isPlaying");
                    Invoke("GrayOut2", 2.2f);
                    enterimage2 = false;
                }
                else
                {
                    // 将图片1的位置重置为初始位置
                    draggedObject.transform.position = initialPosition;
                }
                // 重置当前拖动的对象
                draggedObject = null;
            }
        }

        // 如果鼠标左键按下且有对象被拖动
        if (Input.GetMouseButton(0) && draggedObject != null && draggedObject.CompareTag("canmove"))
        {
            // 将鼠标位置转换为相对于画布的本地坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out Vector2 localPoint);

            // 更新拖动对象的位置为本地坐标
            draggedObject.transform.localPosition = localPoint;
        }
    }
    //将图片颜色置灰
    public void GrayOut0()
    {
        // 将图片2的颜色设置为灰色
        image0.color = Color.gray;
        textsurplus0.text = "剩余米数为二";
        tipspanel.SetActive(true);
        textTips.text = "经过每次消耗三米材料的简单纺织后，剩余的材料米数为二";
    }
    public void GrayOut1()
    {
        // 将图片2的颜色设置为灰色
        image1.color = Color.gray;
        textsurplus1.text = "剩余米数为三";
        tipspanel.SetActive(true);
        textTips.text = "经过每次消耗五米材料的简单纺织后，剩余的材料米数为三";
    }
    public void GrayOut2()
    {
        // 将图片2的颜色设置为灰色
        image2.color = Color.gray;
        textsurplus2.text = "剩余米数为三";
        tipspanel.SetActive(true);
        textTips.text = "经过每次消耗七米材料的精致纺织后，剩余的材料米数为三";
    }

    // 检查两个 RectTransform 是否重叠
    private bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        Vector3[] corners = new Vector3[4];
        rect1.GetWorldCorners(corners);
        Bounds bounds1 = new Bounds(corners[0], Vector3.zero);
        for (int i = 1; i < 4; i++)
        {
            bounds1.Encapsulate(corners[i]);
        }

        rect2.GetWorldCorners(corners);
        Bounds bounds2 = new Bounds(corners[0], Vector3.zero);
        for (int i = 1; i < 4; i++)
        {
            bounds2.Encapsulate(corners[i]);
        }

        return bounds1.Intersects(bounds2);
    }

    // 获取鼠标下的对象
    private GameObject GetObjectUnderMouse()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;

        // 将点击事件发送到UI系统中，返回点击位置下的所有射线碰撞对象
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // 返回点击位置下的第一个对象
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Image>() != null)
            {
                return result.gameObject;
            }
        }

        return null;
    }
    private void BuCancelOnclick()
    {
        tipspanel.SetActive(false);
        if (isOver)
        {
            curCanvas.SetActive(false);
        }
    }
    private void BuSubmitOnclick()
    {
        if ("二十三" == textinput.text)
        {
            textTips.text = "恭喜你，运用了大衍求一术的方法解开了秦九韶先生的织布谜题，你成功了！";
            tipspanel.SetActive(true);
            isOver = true;
        }
        else
        {
            textTips.text = "答案不对哦，再想想吧，可以运用大衍求一术的方法！";
            tipspanel.SetActive(true);
        }
    }
}
