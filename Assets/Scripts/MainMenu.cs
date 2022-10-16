using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    // Put all PlayerInputs and Inputactions on one script.
    public PlayerInputs pause;

    [SerializeField] GameObject panel;
    [SerializeField] GameObject pauseButtonMobile;
    [SerializeField] bool mobile;

    PauseManger pauseManger;

    private InputAction pausebutton;
    private void Awake()
    {
        pause = new PlayerInputs();
        pauseManger = GetComponent<PauseManger>();
    }
    private void OnEnable()
    {
        pausebutton = pause.Pause.PauseButton;
        pausebutton.Enable();
        pausebutton.performed += Pause;
    }

    private void OnDisable()
    {
        pausebutton.Disable();
    }
    void Update()
    {
    

    }

    private void Pause(InputAction.CallbackContext context)
    {
       
        if(panel.activeInHierarchy == false)
        {
            OpenMenu();
           
        }
        else
        {
            CloseMenu();
        }
       
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        pauseManger.UnPauseGame();

        if(mobile)
        {
            pauseButtonMobile.SetActive(true);
        }
    }

    public void OpenMenu()
    {
        panel.SetActive(true);
        pauseManger.PauseGame();

       if(mobile)
        {
            pauseButtonMobile.SetActive(false);
        }
    }
}
