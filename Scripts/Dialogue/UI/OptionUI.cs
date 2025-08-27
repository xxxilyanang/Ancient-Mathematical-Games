using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionText;

    private Button thisButton;//当前选择标签的 Button 脚本
    private DialoguePiece currentPiece;
    private string nextPieceID;

    private bool takeQuest;

    private string scriptName;
    
    private GameSort gameSort;
    
    void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }
    public void UpdateOption(DialoguePiece piece, DialogueOption option , string name)
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        takeQuest = option.takeQuest;
        gameSort = option.gameSort;
        scriptName = name;
    }

    private void OnOptionClicked()
    {
         ExpendOptionUI.ChangeOptionHide(scriptName,nextPieceID);
         ExpendOptionUI.ChangeGame(scriptName,nextPieceID,currentPiece.ID);
        
        if (nextPieceID == "")
        {
            DialogueUI.GetInstance().dialoguePanel.SetActive(false);

            if (!takeQuest)//如果该选项没有接取任务的标志,就解除各种锁定
            {
                PlayerCameraMgr.GetInstance().EnableMainCamera();
                PlayerCameraMgr.GetInstance().EnablePlayerMove();
                OperationStateMgr.GetInstance().DisableNowDialogueCamera();
                //DialogueUI.GetInstance().currentCamera.SetActive(false);
                OperationStateMgr.GetInstance().SwitchCursorState(false);
                //Cursor.lockState = CursorLockMode.Locked;   // 锁定鼠标光标在屏幕中央
                //Cursor.visible = false;                     // 隐藏鼠标光标
            }else
            {
                //如果takeQuest为true,且满足:
                if (gameSort == GameSort.Game_ForgeIron)
                {
                    CanvasGameMgr.GetInstance().isEnableForgeIronGame = true;
                }else if (gameSort == GameSort.Game_MaZhi)
                {
                    CanvasGameMgr.GetInstance().isEnableMaZhiGame = true;
                }else if (gameSort == GameSort.Game_YingBuZu)
                {
                    CanvasGameMgr.GetInstance().isEnableYingBuZuGame = true;
                    OperationStateMgr.GetInstance().DisableNowDialogueCamera();
                }
            }
        }
        else
        {
            DialogueUI.GetInstance().UpdateMainDialogue(
                DialogueUI.GetInstance().currentData.dialogueIndex[nextPieceID]);
        }
    }
    
    

}

