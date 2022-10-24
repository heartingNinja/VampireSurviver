using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarOrHumanManager : MonoBehaviour
{
    public bool isHuman;
    public GameObject player;
    public GameObject car;
    [SerializeField] GameObject carUI;
    [SerializeField] GameObject humanUI;
    [SerializeField] PCorMobileManger pCorMobileManger;
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
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 8;

            if(pCorMobileManger.isMobile)
            {
                carUI.SetActive(true);
                humanUI.SetActive(false);
            }
        }
        else
        {
            if(car != null)
            {
                player.transform.position = car.transform.position;
            }

            if(pCorMobileManger.isMobile)
            {
                carUI.SetActive(false);
                humanUI.SetActive(true);
            }
            
            player.SetActive(true);
            cinemachineVirtualCamera.Follow = player.transform;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = 5;
            Destroy(car);
        }
    }
}
