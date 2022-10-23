using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispose : MonoBehaviour
{
    Transform playerTransform;
    CarOrHumanManager carOrHumanManager; // my add
    Transform carTransform; // my add
    float maxDistance = 25f;

    private void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>(); // my add
    }
    private void Update()
    { // not working correct for items spawning, think they start too far from player
        if(carOrHumanManager.isHuman)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance > maxDistance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            carTransform = FindObjectOfType<TopDownCarController>().gameObject.transform;

            float distance = Vector3.Distance(transform.position, carTransform.position);

            if (distance > maxDistance)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
