using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if ( period <= Mathf.Epsilon) return;
        float cyclesCount = Time.time / period;
        float rawSinCount = Mathf.Sin(cyclesCount);
        movementFactor = (1 + rawSinCount) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
