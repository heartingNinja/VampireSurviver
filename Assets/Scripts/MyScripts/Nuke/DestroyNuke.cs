using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNuke : MonoBehaviour
{
   
    [SerializeField] float timeToDestroy = 10;

    
    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy < 0)
        {
            Destroy(gameObject);
        }
    }
}
