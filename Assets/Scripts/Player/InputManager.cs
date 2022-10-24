using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    PlayerControls.GameplayActions gameplay;

    [HideInInspector] public Vector2 movement;

    private void Awake()
    {
        playerControls = new PlayerControls();
        gameplay = playerControls.Gameplay;
    }

    private void Update()
    {
        MovementInput();
        Debug.Log(movement);
    }

    #region Enable and Disable Player Controls
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion

    public void MovementInput()
    {
        movement = gameplay.Move.ReadValue<Vector2>();
    }
}
