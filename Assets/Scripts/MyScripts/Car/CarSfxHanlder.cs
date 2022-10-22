using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarSfxHanlder : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer audioMixer;

    [Header("Audio Sources")]
    public AudioSource tiresScreechingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = .5f;
    float tireScreechPitch = .5f;

    //Components 
    TopDownCarController topDownCarController;
    // Start is called before the first frame update
    void Start()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        audioMixer.SetFloat("SFXVolume", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateTiresScreechingSFX();
    }

    private void UpdateEngineSFX()
    {
        // Handle engine SFX
        float velocityMagnitude = topDownCarController.GetVelocityMagnitude();

        //Increase the engine volume as the car goes faster
        float desiredEngineVolume = velocityMagnitude * .05f;

        //But keep a minium level so it playes even if the car is idle
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, .2f, 1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        //To add more varitation to the engine sound we also change the pitch
        desiredEnginePitch = velocityMagnitude * .2f;
        desiredEnginePitch = Mathf.Clamp(desiredEngineVolume, .5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);

    }

    private void UpdateTiresScreechingSFX()
    {
        //Handle tire screeching SFX
        if(topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            //if the car is braking we want the tire screech to be louder and also change the pitch
            if(isBraking)
            {
                tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch, .5f, Time.deltaTime * 10);
            }
            else
            {
                //If we are not breaking we still want to play this screech sound if the player is drifting
                tiresScreechingAudioSource.volume = Math.Abs(lateralVelocity) * .05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * .1f;
            }
        }
        //Fade out the tire screech SFX if we are not screeching
        else
        {
            tiresScreechingAudioSource.volume = Mathf.Lerp(tiresScreechingAudioSource.volume, 0, Time.deltaTime * 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Get the relative velocity of the collision
        float relativeVelocity = collision.relativeVelocity.magnitude;

        float volume = relativeVelocity * .1f;

        carHitAudioSource.pitch = UnityEngine.Random.Range(.95f, 1.05f);
        carHitAudioSource.volume = volume;

        if(!carHitAudioSource.isPlaying)
        {
            carHitAudioSource.Play();
        }
    }


}
