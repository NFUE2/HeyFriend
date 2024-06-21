using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    PhotonView pv;
    private void Awake() {
        pv = GetComponent<PhotonView>();
    }
    public void Move(InputAction.CallbackContext context) {
        if (!pv.IsMine) return;
        Vector2 direction = context.ReadValue<Vector2>().normalized;
        if (context.phase == InputActionPhase.Performed)
        {
            OnMoveEvent?.Invoke(direction);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnMoveEvent?.Invoke(Vector2.zero);
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if (!pv.IsMine) return;
        if (context.phase == InputActionPhase.Started)
            OnJumpEvent?.Invoke();
    }

    public void OnEscape()
    {
        if (StageManager.instance != null) StageManager.instance.OpenMenu();
    }
}
