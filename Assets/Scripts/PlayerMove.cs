using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public Vector2 movementVector;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVertivalVector;
    Rigidbody2D rb;
    [SerializeField] float speed = 2;

    Animate animate;
   
    
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animate>();
               
    }

    private void Start()
    {
        lastHorizontalVector = -1f;
        lastVertivalVector = 1f;
    }

    private void OnMovement(InputValue value)
    {
        movementVector = value.Get<Vector2>();
       
    }

    private void FixedUpdate()
    {
       // lastHorizontalVector = movementVector.x;
      //  lastVertivalVector = movementVector.y;
        //  if(moveDirection.x != 0 || moveDirection.y != 0) //Add friction for this
        //  {
        rb.velocity = movementVector * speed;

            animate.horizontal = movementVector.x;
            animate.vertical = movementVector.y;
        // }
       

        if(movementVector.x != 0)
        {
            lastHorizontalVector = movementVector.x;
        }

        if(movementVector.y != 0)
        {
            lastVertivalVector = movementVector.y;
        }

    }




}
