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
    public GameObject Finish;

    private List<string> stageNames = new List<string>
    { "Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage 5", "Stage 6", "Stage 7", "Stage 8", "Stage 9", "Stage 10"};
    private static Stage instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextStage()
    {
        totalPoint += stagePoint;
        stagePoint = 0;
        if (stageIndex < stageNames.Count - 1)
        {
            stageIndex++;
            SceneManager.LoadScene(stageNames[stageIndex]);
        }
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
