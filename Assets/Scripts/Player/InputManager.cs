using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls PlayerControls;
    PlayerControls.GameplayActions Gameplay;

    public Vector2 movement { get; private set; }

    private void Awake()
    {
        PlayerControls = new PlayerControls();
        Gameplay = PlayerControls.Gameplay;
    }

    private void Update()
    {
        MovementInput();
    }

    #region Enable and Disable Player Controls
    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }
    #endregion

    public void MovementInput()
    {
        movement = Gameplay.Move.ReadValue<Vector2>();
    }
}
