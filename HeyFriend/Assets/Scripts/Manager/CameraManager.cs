using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    private float totalposition_x;
    private float positionAverage;

    private float cusPosition_y ;
    void Start(){
        cusPosition_y = transform.position.y;
    }
    
    void Update()
    {
        if(players.Count<=0)return;
        foreach(KeyValuePair<string,GameObject> player in players){
            totalposition_x += player.Value.transform.position.x;
        }
        positionAverage = totalposition_x/players.Count;
        transform.position = new Vector3(positionAverage, cusPosition_y,-10);
        totalposition_x = 0;
    }
}
