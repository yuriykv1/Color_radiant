using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; 
    private PlayerInput.OnFootActions onFoot;
    private Movement move;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;  
        move = GetComponent<Movement>();
    }

    void Update()
    {
        Vector2 move = onFoot.Movement.ReadValue<Vector2>();
        Debug.Log("Move input: " + move);
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
