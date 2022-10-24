using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
   
    [SerializeField] float delay = 1f;
    [SerializeField] float  LevelLoaddelay= 2f;
    [SerializeField] AudioClip Crash;
    [SerializeField] AudioClip Success;
    [SerializeField] ParticleSystem CrashPart;
    [SerializeField] ParticleSystem SuccessPart;
   
    Movement script;
    AudioSource ads;
    bool isTransitionining = false;
    bool collisionDisable = false;

    void Start()
    {
       script =  GetComponent<Movement>();
        ads = GetComponent<AudioSource>();
    }

  
    void Update()
    {
        keys();
    }

    private void keys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("rer");
            collisionDisable = !collisionDisable; // toggle collision
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitionining || collisionDisable)
        {
            return;
        }
            switch (collision.gameObject.tag)
            {

                case "freindly":
                    {
                        Debug.Log("lauch");
                        break;
                    }



                case "Finish":
                    {
                        StartNextLevelSequence();
                        break;
                    }
                /* case "Fuel":
                     {
                         Debug.Log("fuel refilled");
                         break;
                     }*/

                default:
                    StartCrashSequence();



                    break;


            
        }
       
    }

     void StartNextLevelSequence()
    {

   
        ads.Stop();
        SuccessPart.Play();
            ads.PlayOneShot(Success);
            isTransitionining = true;

       
        script.enabled = false;
        Invoke("LoadScene", LevelLoaddelay);
    }

     void LoadScene()
    {
        int CurrenScenidx = SceneManager.GetActiveScene().buildIndex;
        int NextScenIdx = CurrenScenidx + 1;
        if(NextScenIdx == SceneManager.sceneCountInBuildSettings)
        {
            NextScenIdx = 0;
        }
    
        SceneManager.LoadScene( NextScenIdx);
        
     }


    void StartCrashSequence()
    {

        
        ads.Stop();
        CrashPart.Play();
        ads.PlayOneShot(Crash);
            isTransitionining = true;
       
        script.enabled = false;
    
        Invoke("ReloadLevel", delay);

    }

    void ReloadLevel()
    {
        int CurrenScenidx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrenScenidx);
    }
}
