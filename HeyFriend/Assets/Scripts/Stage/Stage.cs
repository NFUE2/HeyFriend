using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;

    public TMP_Text UIStage;
    public TMP_Text UITotalPoint;
    public TMP_Text UIStagePoint;
    public GameObject RestartBtn;
    public GameObject Finish;

    public void NextStage()
    {
        if (stageIndex == 0)
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;

        SceneManager.LoadScene("Stage 2");
        UIStage.text= "STAGE " + (stageIndex + 1);
        Finish.SetActive(false);
    }

    void Update()
    {
        UITotalPoint.text = (totalPoint + " /").ToString();
        UIStagePoint.text = (stagePoint).ToString();

        if (stagePoint >= totalPoint)
        {
            Finish.SetActive(true);
        }
    }
}
