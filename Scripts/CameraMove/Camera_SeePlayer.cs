using UnityEngine;
using UnityEngine.EventSystems;

public class Camera_SeePlayer : SingletonMono<Camera_SeePlayer>
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;


    //�ⲿ�������ǵ�λ��
    public Transform playerPos;

    // �����������ĳ�ʼ����
    public float distance = 3.5f;

    // �����������ٶ�
    public float zoomSpeed = 2.0f;

    //�����ڲ�ͬ���������Ŵ�Сϵ����Ӱ��������Ĺ۲�
    public float playerScaleValue = 1.0f;

    //���Ժõ����������ƫ��ϵ����
    private float verOffset = 1.201108f;

    // �������������ת�ٶ�
    public float rotationSpeed = 2.0f;

    //��¼��껬���ĳ��ȣ����ｫ����ֱ�ӵ����Ƕ���ת��
    private float currentX;
    private float currentY = 5.0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // �������������Ļ����
        Cursor.visible = false; // ���������
    }

    void Update()
    {
        // ����������»���������������������ת
        if (!IsPointerOverUI()) // �������Ƿ��� UI Ԫ����
        {
            // ����������»���������������������ת
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            // �����������ת�����½Ƕ�
            currentY = Mathf.Clamp(currentY, -80f, 80f);

            // ʹ�ù��ֿ��������������ľ���
            distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * playerScaleValue;
            // ������������������С��������
            distance = Mathf.Clamp(distance, 2.5f * playerScaleValue, 4.5f * playerScaleValue);

            // ���ݵ�ǰ�ǶȺ;�����������λ��
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0); //����Ƕ�

            var position = playerPos.position;
            transform.position = position + rotation * direction;
            // �����ʼ�ճ������壨����Ĭ���������泯����Z��������
            transform.LookAt(position + new Vector3(0, verOffset * playerScaleValue, 0));

            //����ɫ��ת��ء�
            // ���ù۲������ת��ʹ�����Z��������ת
            playerPos.rotation = Quaternion.Euler(0, currentX, 0);

            //����ģ��Ĭ�ϳ���z�Ḻ�������Խ�ɫ�ƶ������Ҫ�Ƚ�ģ����Y�������ƶ�180�ȣ�
            //�Ա���Z���������Ч����ת�������ֶ���ó���
            playerPos.Rotate(new Vector3(0, 180, 0));
        }
    }
    public void RecordCurrentPositionAndRotation(Vector3 GDposition, Quaternion GDRotation)//��¼��ʼλ�úͷ���
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        transform.position = GDposition;
        transform.rotation = GDRotation;

    }
    public void RestoreRecordedPositionAndRotation()
    {
        // �ָ��������λ�úͷ���Ϊ��¼��ֵ
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    // �������Ƿ��� UI Ԫ����
    private bool IsPointerOverUI()
    {
        // ����Ƿ��д�������
        if (Input.touchCount > 0)
        {
            // ���ÿ��������λ���Ƿ��� UI Ԫ����
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
            // ������λ���Ƿ��� UI Ԫ����
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
        }

        return false;
    }
}