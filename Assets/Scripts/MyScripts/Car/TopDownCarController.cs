using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = .95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20f;
    public float tireScreeningSideways = 4.0f;

    //Local variables
    public float accelerationInput = 0; // my add to make public
    float steeringInput = 0;

    float rotationAngle = 0;
    float velocityVsUp = 0;

    //Components
    [SerializeField] Rigidbody2D carRigidbody2D;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       

        ApplyEngineForce();

        ApplySteering();

        KillOrthogonalVelocity();
    }

    

    private void ApplyEngineForce()
    {
        //Caculat how muf "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limt so we cannot  go faster than the max speed in the "forward" direction
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //Limt so we cannot  go faster than the max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed * .5f && accelerationInput < 0)
        {
            return;
        }

        //Limit so we cannot go faster in any direction while acceleration
        if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //Apply drag if ther is no accelerationInput so the car stops when the player lets go of the acclerator
        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigidbody2D.drag = 0;
        }

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector,ForceMode2D.Force); // I added (ForceMode)
    }

    private void ApplySteering()
    {

        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTuringFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTuringFactor = Mathf.Clamp01(minSpeedBeforeAllowTuringFactor);


        //Udate the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTuringFactor;
        

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

    float GetLaterVelocity()
    {
        //Returs how fast the car is moving sidways
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float laterVelocity, out bool isBraking)
    {
        laterVelocity = GetLaterVelocity();
        isBraking = false;

        //Check if we are moving forwar and if the player is hitting the brakes. In that case the tires should screech.
        if(accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }
        
        // IF we have a lot of side movement then the tires should be screeching
        if(Mathf.Abs(GetLaterVelocity()) > tireScreeningSideways)
        {
            return true;
        }

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
