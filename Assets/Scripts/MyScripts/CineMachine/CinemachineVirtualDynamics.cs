using Cinemachine;
using UnityEngine;

public class CinemachineVirtualDynamics : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    void Start()
    {
        Camera.main.gameObject.transform.TryGetComponent<CinemachineBrain>(out var brain);
        if( brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
