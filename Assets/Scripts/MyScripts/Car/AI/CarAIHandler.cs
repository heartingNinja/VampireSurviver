using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints, followMouse }

    [Header("AI Settings")]
    public AIMode aiMode;
    public float maxSpeed;
    public bool isAvoidingCars = true;

    // Local variables
    Vector3 targetPositon = Vector3.zero;
    Transform targetTransform = null;

    //Avoidance
    Vector2 avoidanceVectorLerped = Vector3.zero;

    //Waypoints
    WaypointNode currentWaypoint = null;
    WaypointNode[] allWayPoints;

    //Colliders
    BoxCollider2D boxCollider;

    //Components
    TopDownCarController topDownCarController;


    private void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WaypointNode>();
        maxSpeed = topDownCarController.maxSpeed;

        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

       switch(aiMode)
        {
            case AIMode.followPlayer:
                followPlayer();
                break;

            case AIMode.followWaypoints:
                followWaypoints();
                break;

            case AIMode.followMouse:
                followMousePositon();
                break;

        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = ApplyThrottleOrBrake(inputVector.x);

        //Send the input to the car controller
        topDownCarController.SetInputVector(inputVector);
    }

    //AI follows the mouse position
    private void followMousePositon()
    {
        // Take the mouse positon in screen space and convert it to world space
        Vector3 worldPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetPositon = worldPositon;
    }

    private float ApplyThrottleOrBrake(float inputx)
    {
        // If we are going too fast then do not accelerate further
        if (topDownCarController.GetVelocityMagnitude() > maxSpeed)
            return 0;

        //Apply throttle forwards based on how much the car wants to turn. If it's a sharp turn this will cause the car to apply less speed forward
        return 1.05f - Mathf.Abs(inputx) / 1.0f;
    }

    private void followWaypoints()
    {
        //Pick the closest waypoint if we dont have a waypoint set
        if (currentWaypoint == null)
            currentWaypoint = FindClosesWayPoint();

        //Set the target on the waypoints positon
        if(currentWaypoint != null)
        {
            targetPositon = currentWaypoint.transform.position;

            //Store how close we are to the target
            float distanceToWayPoint = (targetPositon - transform.position).magnitude;

            //Check if we are close enough to consider that we have reached the waypoint
            if(distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {

                if (currentWaypoint.maxSpeed > 0)
                    maxSpeed = currentWaypoint.maxSpeed;
                else maxSpeed = 1000;

                // If we are close enough then follow to the next waypoint, if there are multiple waypoints then pick one at random
                currentWaypoint = currentWaypoint.nextWaypointNode[UnityEngine.Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    private WaypointNode FindClosesWayPoint()
    {
        return allWayPoints
        .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
        .FirstOrDefault();

    }

    private void followPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if(targetTransform != null)
        {
            targetPositon = targetTransform.position;
        }
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPositon - transform.position;
        vectorToTarget.Normalize();

        //Apply avoidance to steering
        if (isAvoidingCars)
            AvoidCars(vectorToTarget, out vectorToTarget);
       
        //Calculate an angle towards the target
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        //We want the car to turn as much as possible if the angle is greater than 45 degress and we want it to smooth out so if the angle is small we want the AI to make smaller
        float steerAmount = angleToTarget / 45.0f;

        //Clamp steering to between -1 and 1
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    //Checks for cars ahead of the car
    bool IsCarsInFrontOfAICar(out Vector3 position, out Vector3 otherCarRightVector)
    {
        //Disable the cars own collider to avoid having the AI car detect itself
        boxCollider.enabled = false;

        // Perform the circle cast in front of the car with a slight offset forward and only in the Car layer
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * .5f, 1.1f, transform.up, 12, 1 << LayerMask.NameToLayer("Car"));

        //Enable the cars own collider to  detect itself
        boxCollider.enabled = true;

        if(raycastHit2D.collider != null)
        {
            //Draw a red line showing how the detection is, make it red since we have detected another car
            Debug.DrawRay(transform.position, transform.up * 12, Color.red);
            

            position = raycastHit2D.collider.transform.position;
            otherCarRightVector = raycastHit2D.collider.transform.right;

            return true;
        }
        else
        {
            //We didnt detect any other car so draw black line with the distance that we use to check for other cars
            Debug.DrawRay(transform.position, transform.up * 12, Color.black);
            
        }

        //No car was detected but we still need assign out values 
        position = Vector3.zero;
        otherCarRightVector = Vector3.zero;

        return false;

    }

    // add a bool to stop this script for hitting player
    void AvoidCars(Vector2 vectorToTarget, out Vector2 newVectorToTarget)
    {
        if(IsCarsInFrontOfAICar(out Vector3 otherCarsPosition, out Vector3 otherCarRightVector))
        {
            Vector2 avoidanceVector = Vector2.zero;

            //Calculate the reflectiong vector if we would hit the other car
            avoidanceVector = Vector2.Reflect((otherCarsPosition - targetPositon).normalized, otherCarRightVector);

            float distanceToTarget = (targetPositon - transform.position).magnitude;

            //We want to be able to control how much disire the AI has to drive towards the wyapoint vs avoiding the other cars.
            //As we get closer to the waypoint the desire to reach the waypoint increases
            float driveToTargetinfluence = 20.0f / distanceToTarget;

            //Ensure that we limit the value to between 30% and 100% as we always want the AI to desire to reach the waypoint
            driveToTargetinfluence = Mathf.Clamp(driveToTargetinfluence, 0.3f, 1.0f);

            //The disire to avoid the car is simply the inverse to reach the waypoint;
            float avoidanceInfluence = 1.0f - driveToTargetinfluence;

            //Reduce jittering a little bit by using a lerp
            avoidanceVectorLerped = Vector2.Lerp(avoidanceVectorLerped, avoidanceVector, Time.fixedDeltaTime * 4);

            //avoidance vector
            newVectorToTarget = vectorToTarget * driveToTargetinfluence + avoidanceVectorLerped * avoidanceInfluence;
            newVectorToTarget.Normalize();

            //Draw the vector which indicates the avoidance vector in green
            Debug.DrawRay(transform.position, avoidanceVector * 10, Color.green);

            //Draw the vector which indicates the actually take in yellow
            Debug.DrawRay(transform.position, avoidanceVector * 10, Color.yellow);

            return;

        }

        //We need assign a default value if we didnt hit any cars before we exit the function;
        newVectorToTarget = vectorToTarget;
    }
}
