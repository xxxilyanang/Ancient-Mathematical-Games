using UnityEngine;
using UnityEngine.EventSystems;

public class Camera_SeePlayer : SingletonMono<Camera_SeePlayer>
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;


    //外部传入主角的位置
    public Transform playerPos;

    // 摄像机与物体的初始距离
    public float distance = 3.5f;

    // 鼠标滚轮缩放速度
    public float zoomSpeed = 2.0f;

    //人物在不同场景的缩放大小系数：影响摄像机的观察
    public float playerScaleValue = 1.0f;

    //调试好的摄像机上下偏移系数：
    private float verOffset = 1.201108f;

    // 鼠标上下左右旋转速度
    public float rotationSpeed = 2.0f;

    //记录鼠标滑动的长度（这里将长度直接当作角度旋转）
    private float currentX;
    private float currentY = 5.0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标光标在屏幕中央
        Cursor.visible = false; // 隐藏鼠标光标
    }

    void Update()
    {
        // 鼠标左右上下滑动控制摄像机和物体的旋转
        if (!IsPointerOverUI()) // 检查鼠标是否在 UI 元素上
        {
            // 鼠标左右上下滑动控制摄像机和物体的旋转
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            // 限制摄像机旋转的上下角度
            currentY = Mathf.Clamp(currentY, -80f, 80f);

            // 使用滚轮控制摄像机与物体的距离
            distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * playerScaleValue;
            // 限制摄像机与物体的最小和最大距离
            distance = Mathf.Clamp(distance, 2.5f * playerScaleValue, 4.5f * playerScaleValue);

            // 根据当前角度和距离计算摄像机位置
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0); //传入角度

            var position = playerPos.position;
            transform.position = position + rotation * direction;
            // 摄像机始终朝向物体（这里默认物体正面朝向是Z轴正方向）
            transform.LookAt(position + new Vector3(0, verOffset * playerScaleValue, 0));

            //【角色旋转相关】
            // 设置观察体的旋转，使物体的Z正方向旋转
            playerPos.rotation = Quaternion.Euler(0, currentX, 0);

            //由于模型默认朝向z轴负方向，所以角色移动完后：需要先将模型绕Y轴正向移动180度，
            //以便以Z轴正方向的效果旋转（由左手定则得出）
            playerPos.Rotate(new Vector3(0, 180, 0));
        }
    }
    public void RecordCurrentPositionAndRotation(Vector3 GDposition, Quaternion GDRotation)//记录初始位置和方向
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        transform.position = GDposition;
        transform.rotation = GDRotation;

    }
    public void RestoreRecordedPositionAndRotation()
    {
        // 恢复摄像机的位置和方向为记录的值
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    // 检查鼠标是否在 UI 元素上
    private bool IsPointerOverUI()
    {
        // 检查是否有触摸输入
        if (Input.touchCount > 0)
        {
            // 检查每个触摸的位置是否在 UI 元素上
            foreach (Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }
        else
        {
            // 检查鼠标位置是否在 UI 元素上
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
        }

        return false;
    }
}