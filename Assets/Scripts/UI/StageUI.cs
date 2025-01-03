using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public StageTimer stageTimer;
    public TextMeshProUGUI EnemyText;

    public GameObject[] clearObjects;
    public GameObject[] stars;
    public TextMeshProUGUI[] timeTexts;
    public TextMeshProUGUI puzzleText;

    public GameObject[] GameoverObjects;
    public TextMeshProUGUI GameoverTimeText;

    public Button[] finishButtons;

    public string TextSound = "coin_7";
    public string StarSound = "coin_11";
    public string ClearSound = "Jingle_Win_00";

    private void Start()
    {
        for (int i = 0; i < clearObjects.Length; i++)
        {
            if (i < stars.Length)
                stars[i].SetActive(false);
            clearObjects[i].SetActive(false);
        }

        for (int i = 0; i < GameoverObjects.Length; i++)
        {
            GameoverObjects[i].SetActive(false);
        }

    }

    public IEnumerator StageCorutine(bool[] starInfo)
    {
        var wait = new WaitForSeconds(1f);
        int i;

        AudioManager.PlaySound(ClearSound);

        for (i = 0; i < 4; i++)
        {
            AudioManager.PlaySound(TextSound);
            clearObjects[i].SetActive(true);
            yield return wait;
        }

        for (int j = 0; j < 3; j++)
        {
            if (starInfo[j])
            {
                AudioManager.PlaySound(StarSound);
                stars[j].SetActive(true);
                yield return wait;
            }
        }

        AudioManager.PlaySound(TextSound);
        clearObjects[i].SetActive(true);
    }

    public IEnumerator GameoverCorutine()
    {
        var wait = new WaitForSeconds(1f);

        for (int i = 0; i < GameoverObjects.Length; i++)
        {
            GameoverObjects[i].SetActive(true);
            yield return wait;
        }
    }
}
