using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Vector2 position = new Vector2(0,0);
    // Start is called before the first frame update
    void Start()
    {
            PhotonNetwork.Instantiate("Player", position, Quaternion.identity);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
