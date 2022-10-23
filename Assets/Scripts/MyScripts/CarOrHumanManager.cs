using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarOrHumanManager : MonoBehaviour
{
    public bool isHuman;
    public GameObject player;
    public GameObject car;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    

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
            cinemachineVirtualCamera.Follow = car.transform;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 7;
        }
        else
        {
            if(car != null)
            {
                player.transform.position = car.transform.position;
            }
            
            player.SetActive(true);
            cinemachineVirtualCamera.Follow = player.transform;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 5;
            Destroy(car);
        }
    }
}
