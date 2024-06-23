using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Vector2 position = new Vector2(0,0);
    // Start is called before the first frame update

    private PhotonView photonView;

    private void Start(){
        photonView = GetComponent<PhotonView>();
    }

    public void SetPosition(){
        photonView.RPC("SetPositionRPC",RpcTarget.All);
    }

    [PunRPC]
    private void SetPositionRPC(){
        Debug.Log("실행");
        transform.position = new Vector3(0, 0, 0);
    }
}
