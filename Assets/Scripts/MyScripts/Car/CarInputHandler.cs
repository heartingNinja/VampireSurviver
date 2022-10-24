using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{
    
    // Componeets
    TopDownCarController topDownCarController;
    private Vector2 movementVector;
    GasButton gasButton;
    BreakButton breakButton;
    PCorMobileManger pCorMobileManger;
   // SteeringWheelTutorial steeringWheelTutorial;

    void Start()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        // steeringWheelTutorial = FindObjectOfType<SteeringWheelTutorial>();
        gasButton = FindObjectOfType<GasButton>();
        breakButton = FindObjectOfType<BreakButton>();
        pCorMobileManger = FindObjectOfType<PCorMobileManger>();
    }
  
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if(pCorMobileManger.isMobile == false)
        {
            inputVector.x = movementVector.x; //Input.GetAxis("Horizontal");
            inputVector.y = movementVector.y; //Input.GetAxis("Vertical");
        }
        else
        {
          
            if(gasButton.gasOn)
            {
                inputVector.x = SteeringWheelTutorial.steeringInput;
                inputVector.y = gasButton.gasSpeed;
            }
            else
            {
                inputVector.x = SteeringWheelTutorial.steeringInput;
                inputVector.y = breakButton.breakSpeed;
            }


        }
       

        

        topDownCarController.SetInputVector(inputVector);
    }

    private void OnMovement(InputValue value)
    {
        movementVector = value.Get<Vector2>();

    }
}
