using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : SingletonMono<DialogueUI>
{
    [Header("Basic Elements")]
    //public Image icon;
    public Text mainText;
    public Button nextButton;
    public Text nameText;   //标识当前对话人的文本标签，还未添加
    public GameObject dialoguePanel;
    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;
    [Header("Data")]
    public DialogueData_SO currentData;
    int currentIndex = 0;

    //--------------------private
    private string NPCName;


    //[HideInInspector]
    //public GameObject currentCamera;//不在窗口显示

    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialogue);
        ExpendOptionUI.FindScript();
        ExpendOptionUI.FindScript2();
    }
    
    
    //继续对话
    void ContinueDialogue()
    {
        if (currentIndex < currentData.dialoguePieces.Count)
            UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);
        else
        {
            PlayerCameraMgr.GetInstance().EnableMainCamera();
            PlayerCameraMgr.GetInstance().EnablePlayerMove();
            OperationStateMgr.GetInstance().DisableNowDialogueCamera();
            //currentCamera.SetActive(false);
            OperationStateMgr.GetInstance().SwitchCursorState(false);
            //Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标光标在屏幕中央
            //Cursor.visible = false; // 隐藏鼠标光标
            dialoguePanel.SetActive(false);
        }
    }
    public void UpdateDialogueData(DialogueData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }
    public void UpdateMainDialogue(DialoguePiece piece)
    {
        dialoguePanel.SetActive(true);
        currentIndex++;
        nameText.text = piece.Name;//将对话的名字赋值
        mainText.text = "";
        mainText.DOText(piece.text, 1f);
        if (piece.options.Count == 0 && currentData.dialoguePieces.Count > 0)
        {
            nextButton.interactable = true;
            nextButton.gameObject.SetActive(true);
            nextButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            //nextButton.gameObject.SetActive(false);
            nextButton.interactable = false;//使按钮无法点击
            nextButton.transform.GetChild(0).gameObject.SetActive(false);//设置按钮下文本失活
        }
        //创建options
        CreateOptions(piece);
    }

    void CreateOptions(DialoguePiece piece)
    {
        if (optionPanel.childCount > 0)
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

        foreach (var t in piece.options)
        {
            if(t.isHide) continue;
            var option = Instantiate(optionPrefab, optionPanel);
            option.UpdateOption(piece, t , currentData.name);
        }
    }

   

    // private void Start()
    // {
    
    // }
    /*    public void SetCurrentNPCCamera(GameObject camera_SeeNPC)
    {
        currentCamera = camera_SeeNPC;
    }   */ 
}
