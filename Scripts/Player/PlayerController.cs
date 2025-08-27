using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�����ƶ��ٶ�
    public float moveSpeed = 3.0f;

    public float runSpeed = 7.0f;

    //�ý�ɫ���ص�״̬��
    private Animator playerAnimator;

    //��ɫ������
    private CharacterController playerController;
    private static readonly int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    private static readonly int IsRun = Animator.StringToHash("isRun");

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        // �ı�״̬���Ĳ�����������·����
        playerAnimator.SetInteger(PlayerSpeed, (int)verticalInput);

        bool isRunning = Input.GetMouseButton(1);

        float speedMultiplier = isRunning ? runSpeed : moveSpeed;

        // -Vector3.right��ģ�͵ĳ���Clamp��ֹ�������
        playerController.SimpleMove(
            -this.transform.forward * (speedMultiplier * Mathf.Clamp(verticalInput, 0.0f, 1.0f)));

        // �����Ƿ��ܵĶ���״̬
        playerAnimator.SetBool(IsRun, isRunning);

        if (verticalInput == 0)
        {
            // �ı�״̬���Ĳ��������ž�ֹ����
            playerAnimator.SetInteger(PlayerSpeed, 0);
            playerAnimator.SetBool(IsRun, false);
        }
    }
}