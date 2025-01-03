using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBase : MonoBehaviour
{
    [Header("Stage Base")]
    public int stageNumber;

    public ClearPoint clearPoint;

    public float clearTime = 60f;
    public bool puzzleClear = false;
    public bool isBoss = false;

    public StageUI stageUI;

    public StageTimer StageTimer { get { return stageUI.stageTimer; } }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        GameManager.Instance.CurrentStage = this;

        foreach (var button in stageUI.finishButtons)
            button.onClick.AddListener(() => StartCoroutine(OnFinishButton()));

        FadeManager.Instance.FadeOut();

        SetEnemyCount();
    }

    public void CheckPlayerInClearPoint()
    {
        if (clearPoint.IsPlayerInside)
        {
            StageClear();
        }
    }
    
    public void SetEnemyCount()
    {
        stageUI.EnemyText.text = "Enemies: " + GameManager.Instance.EnemieCount.ToString();
    }

    public void EnableClearPoint()
    {
        clearPoint.gameObject.SetActive(true);
    }

    public void StageClear()
    {
        PlayerController.Instance.SetInputSystem(false);
        StageTimer.timerOn = false;

        if (StageTimer.time < clearTime)
            GameManager.Instance.CollectStar();

        // �ð� �Է�
        stageUI.timeTexts[0].text = string.Format("{0:00}:{1:00}", (int)(StageTimer.time / 60), (int)(StageTimer.time % 60));
        stageUI.timeTexts[1].text = string.Format("{0:00}:{1:00}", (int)(clearTime / 60), (int)(clearTime % 60));

        bool[] startinfo;
        // ���� ���̸� ���� Ŭ���� �ƴϸ� ���� ���� ���� ���
        if (isBoss)
        {
            GameManager.Instance.CollectStar();
            stageUI.puzzleText.text = "Boss Clear";
            startinfo = new bool[] { true, true, StageTimer.time < clearTime, };
        }
        else
        { 
            stageUI.puzzleText.text = "Puzzle " + (puzzleClear ? "Clear" : "Fail");
            startinfo = new bool[3] {
                GameManager.Instance.EnemieCount == 0,
                puzzleClear,
                StageTimer.time < clearTime,
                };
        }
        // �ű�� �����ϱ�
        #region Save Data
        var saveData = DataManager.Instance.SaveData;

        if (stageNumber == 3 && !saveData.UnlockSquare){
            saveData.UnlockSquare = true;
            Debug.Log("Square ability unlocked");
        }

        if (stageNumber == 6 && !saveData.UnlockTrueCircle)
        {
            saveData.UnlockTrueCircle = true;
            Debug.Log("True Circle ability unlocked");
        }

        if (saveData.Stages[stageNumber].isClear == true)
            saveData.Stages[stageNumber].bestTime = Mathf.Min(saveData.Stages[stageNumber].bestTime, StageTimer.time);
        else
            saveData.Stages[stageNumber].bestTime = StageTimer.time;
        if (saveData.Stages[stageNumber].isPuzzleClear == false && (puzzleClear || isBoss))
        {
            saveData.Stages[stageNumber].isPuzzleClear = true;
            saveData.leftStatPoint++;
        }

        if (saveData.Stages[stageNumber].isTimeClear == false && StageTimer.time < clearTime)
        {
            saveData.Stages[stageNumber].isTimeClear = true;
            saveData.leftStatPoint++;
        }

        if (saveData.Stages[stageNumber].isAllKill == false && (GameManager.Instance.IsAllKill || isBoss))
        {
            saveData.Stages[stageNumber].isAllKill = true;
            saveData.leftStatPoint++;
        }

        saveData.Stages[stageNumber].isClear = true;
        DataManager.Instance.SaveGameData();

        #endregion

        StartCoroutine(stageUI.StageCorutine(startinfo));
    }

    public void Gameover()
    {
        PlayerController.Instance.SetInputSystem(false);
        StageTimer.timerOn = false;

        stageUI.GameoverTimeText.text = string.Format("{0:00}:{1:00}", (int)StageTimer.time, (int)(StageTimer.time * 100) % 100);

        StartCoroutine(stageUI.GameoverCorutine());
    }

    protected IEnumerator OnFinishButton()
    {
        var scene = SceneManager.LoadSceneAsync("StageSelect");
        scene.allowSceneActivation = false;
        yield return FadeManager.Instance.FadeIn();
        GameManager.Instance.audioManager.PlayMusic(-1);

        scene.allowSceneActivation = true;
    }
}
