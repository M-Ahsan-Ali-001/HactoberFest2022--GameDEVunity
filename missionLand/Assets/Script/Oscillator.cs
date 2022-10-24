using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
   [SerializeField] Vector3 MovementVector;
 [SerializeField] float movementfactor;
 [SerializeField] float period=2f;


    void Start()
    {
        StartingPosition = transform.position;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; };
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementfactor = (rawSineWave + 1f)/2;
        
        Vector3 offset = movementfactor * MovementVector;
        transform.position = offset + StartingPosition;


    }
}
