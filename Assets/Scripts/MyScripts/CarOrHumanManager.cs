using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOrHumanManager : MonoBehaviour
{
    public bool isHuman;
    public GameObject player;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
       // player = FindObjectOfType<Character>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        HumanOrCar();
    }

    public void HumanOrCar()
    {
        if(isHuman == false)
        {
            car = FindObjectOfType<TopDownCarController>().gameObject;
        }
    }
}
