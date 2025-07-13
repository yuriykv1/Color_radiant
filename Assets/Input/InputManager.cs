using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private Movement move;

    void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;
        }

        onFoot.Enable();
    }

    void Start()
    {
        move = GetComponent<Movement>();
    }

    void Update()
    {
        Vector2 moveInput = onFoot.Movement.ReadValue<Vector2>();
        Debug.Log("Move input: " + moveInput);
    }

    void OnDisable()
    {
        onFoot.Disable();
    }
}
