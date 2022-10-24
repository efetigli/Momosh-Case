using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Input Manager")]
    [SerializeField] InputManager InputManager;

    [Header("Player Components")]
    [SerializeField] CharacterController CC;

    [Header("Player Attributes")]
    [SerializeField] float MoveSpeed;
    [SerializeField] float RotateSpeed;

    [Header("Body Components")]
    [SerializeField] Transform BodyTransform;

    Vector3 move;

    private void Update()
    {
        Movement();
        FaceBodyMovingDirection();
    }

    public void Movement()
    {
        move = new Vector3(InputManager.movement.x, 0f, InputManager.movement.y);
        CC.Move(move * MoveSpeed * Time.deltaTime);
    }

    public void FaceBodyMovingDirection()
    {
        if (move == Vector3.zero)
            return;

        BodyTransform.localRotation = Quaternion.Lerp(BodyTransform.rotation, 
            Quaternion.LookRotation(move), RotateSpeed * Time.deltaTime);
    }
}
