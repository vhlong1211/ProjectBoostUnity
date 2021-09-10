using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody bufferRb;
    [SerializeField] float mainThrust=1f;
    [SerializeField] float swingThrust=1f;
    // Start is called before the first frame update
    void Start()
    {
        bufferRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrusting();
        Swinging();
    }

    void Thrusting(){
        if (Input.GetKey(KeyCode.Space)){
            bufferRb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
        }
    }

    void Swinging(){
        if (Input.GetKey(KeyCode.A)){
            handleRotation(1f);
        }else if (Input.GetKey(KeyCode.D)){
            handleRotation(-1f);
        }
    }

    void handleRotation(float side){
        bufferRb.freezeRotation = true;
        transform.Rotate(Vector3.forward*swingThrust*Time.deltaTime*side);
        bufferRb.constraints = RigidbodyConstraints.FreezeRotationX |
         RigidbodyConstraints.FreezeRotationY |
         RigidbodyConstraints.FreezePositionZ;

    }
}
