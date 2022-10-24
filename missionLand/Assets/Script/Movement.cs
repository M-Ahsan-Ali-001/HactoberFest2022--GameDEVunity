using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [SerializeField] float ThrustForce = 100f;
    [SerializeField] float Rotation = 5f;
    [SerializeField] AudioClip ThrustSound;
    [SerializeField] ParticleSystem upThrust;
    [SerializeField] ParticleSystem rightThrust;
    [SerializeField] ParticleSystem leftThrust;
    AudioSource ads;
    Rigidbody Rigd;

    void Start()
    {
        Rigd = GetComponent<Rigidbody>();
        ads=GetComponent<AudioSource>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
       
    }

   

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            ads.Stop();
           
                upThrust.Stop();

        }
        
       

    }

    private void StartThrusting()
    {
        //transform.position=new Vector3(0,1,0)*Time.deltaTime;
        Rigd.AddRelativeForce(Vector3.up * ThrustForce * Time.deltaTime);
        if (!ads.isPlaying)
        {
            ads.PlayOneShot(ThrustSound);

        }
        if (!upThrust.isPlaying)
        {
            upThrust.Play();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            startRotationLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            startRotationRight();
        }
        else
        {
            stopThrusting();
        }
    }

    private void stopThrusting()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }

    private void startRotationRight()
    {
        ApplyRotation(-Rotation);

        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();

        }
    }

    private void startRotationLeft()
    {
        ApplyRotation(Rotation);

        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();

        }
    }

    void ApplyRotation( float rotat)
    {
        Rigd.freezeRotation = true; // freezing roration so we can manually rotate
        transform.Rotate(Vector3.forward * rotat * Time.deltaTime);
        Rigd.freezeRotation = false; // doing this so physics system can controll
    }
   
}
