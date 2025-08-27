using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //人物移动速度
    public float moveSpeed = 3.0f;

    public float runSpeed = 7.0f;

    //该角色挂载的状态机
    private Animator playerAnimator;

    //角色控制器
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

        // 改变状态机的参数，播放走路动作
        playerAnimator.SetInteger(PlayerSpeed, (int)verticalInput);

        bool isRunning = Input.GetMouseButton(1);

        float speedMultiplier = isRunning ? runSpeed : moveSpeed;

        // -Vector3.right是模型的朝向，Clamp防止人物后退
        playerController.SimpleMove(
            -this.transform.forward * (speedMultiplier * Mathf.Clamp(verticalInput, 0.0f, 1.0f)));

        // 设置是否奔跑的动画状态
        playerAnimator.SetBool(IsRun, isRunning);

        if (verticalInput == 0)
        {
            // 改变状态机的参数，播放静止动作
            playerAnimator.SetInteger(PlayerSpeed, 0);
            playerAnimator.SetBool(IsRun, false);
        }
    }
}