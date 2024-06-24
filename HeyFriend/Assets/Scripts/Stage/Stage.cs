using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex = 0;

    public TMP_Text UIStage;
    public TMP_Text UITotalPoint;
    public TMP_Text UIStagePoint;
    public GameObject Finish;

    public List<string> players = new List<string>();

    private List<string> stageNames = new List<string>
    { "STAGE 1", "STAGE 2", "STAGE 3", "STAGE 4", "STAGE 5","Game Clear"};
    private static Stage instance;

    private PhotonView pv;
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

        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        UpdateUI();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player.name);
        }
    }

    public void NextStage()
    {
        totalPoint += stagePoint; 
        stagePoint = 0;           
        if (stageIndex < stageNames.Count - 1)
        {
            stageIndex++;
            PhotonNetwork.LoadLevel(stageNames[stageIndex]);
        }
        else
        {
            Finish.SetActive(true);
        }
    }

    public void PlayerFinish(GameObject player)
    {
        pv.RPC("PlayerFinishRPC",RpcTarget.All,player.name);

        Debug.Log(players.Count+ "가 다음 스테이지로 이동했습니다");
    }

    [PunRPC]
    private void PlayerFinishRPC(string name){
        players.Remove(name);
        Destroy(GameObject.Find(name));
    }
    void Update()
    {
        UpdateUI();
        if (stagePoint >= totalPoint)
        {
            Finish.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        UIStage.text = stageNames[stageIndex];
        UITotalPoint.text = totalPoint.ToString() + " /";
        UIStagePoint.text = stagePoint.ToString();
    }
}
