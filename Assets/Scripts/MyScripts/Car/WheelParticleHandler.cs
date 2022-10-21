using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    //Local cariables
    float particleEmissionRate = 0;

    //Components 
    TopDownCarController topDownCarController;
    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    // Start is called before the first frame update
    void Start()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();

        particleSystem = GetComponent<ParticleSystem>();

        particleSystemEmissionModule = particleSystem.emission;

        particleSystemEmissionModule.rateOverDistance = 0;


    }

    // Update is called once per frame
    void Update()
    {
        //Reduce the particles over time
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;

        //If the car tires are screeching then we will emmit a trail.
        if (topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            // If the car tires are screeching then we will emitt smoke. If the player is breaking then emitt a lot of smoke.
            if(isBraking)
            {
                particleEmissionRate = 30;
            }
            // If the player is driffing we will wmitt smoke based on how much the playe is drifting
            else
            {
                particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
            }
        }
    }
}
