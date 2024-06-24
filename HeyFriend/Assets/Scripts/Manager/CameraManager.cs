using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public Dictionary<string, float> players = new Dictionary<string, float>();
    private float totalposition_x;
    private float positionAverage;

    private float cusPosition_y ;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }

    void Start(){
        cusPosition_y = transform.position.y;
    }
    
    void Update()
    {
        if(players.Count<=0)return;
        int i=0;
        foreach(KeyValuePair<string,float> player in players){
            totalposition_x += player.Value;
            i++;
        }
        positionAverage = totalposition_x/players.Count;
        transform.position = new Vector3(positionAverage, cusPosition_y,-10);
        totalposition_x = 0;
    }
}
