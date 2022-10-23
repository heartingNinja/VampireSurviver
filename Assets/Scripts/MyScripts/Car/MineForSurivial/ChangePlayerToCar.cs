using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerToCar : MonoBehaviour
{
    
    [SerializeField] GameObject carToDrive;
    CarOrHumanManager carOrHumanManager;

    private void Start()
    {
        carOrHumanManager = FindObjectOfType<CarOrHumanManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Character c = collision.GetComponent<Character>();
        if(c != null && carOrHumanManager.isHuman)
        {
            Instantiate(carToDrive, gameObject.transform.position, gameObject.transform.rotation);
            carOrHumanManager.isHuman = false;
            c.gameObject.SetActive(false);                   
            Destroy(gameObject);
        }
    }



}
