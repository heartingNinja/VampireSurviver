using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{
    // Componeets
    TopDownCarController topDownCarController;
    private Vector2 movementVector;

    void Start()
    {
        topDownCarController = GetComponent<TopDownCarController>();
    }
  
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = movementVector.x; //Input.GetAxis("Horizontal");
        inputVector.y = movementVector.y; //Input.GetAxis("Vertical");

        topDownCarController.SetInputVector(inputVector);
    }

    private void OnMovement(InputValue value)
    {
        movementVector = value.Get<Vector2>();

    }
}
