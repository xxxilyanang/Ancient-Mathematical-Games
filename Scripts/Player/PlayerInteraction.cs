using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactionUI; // 用于显示交互UI的对象
    private bool isNearObject ; // 标记是否靠近物体

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            isNearObject = true;
            ShowInteractionUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            isNearObject = false;
            HideInteractionUI();
        }
    }

    private void Update()
    {
        if (isNearObject && Input.GetKeyDown(KeyCode.F))
        {
            ExecuteFunction();
        }
    }

    void ShowInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
    }

    void HideInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    void ExecuteFunction()
    {
        Debug.Log("Function executed!");
    }
}