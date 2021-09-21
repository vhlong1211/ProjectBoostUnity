using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody bufferRb;
    AudioSource bufferAuS;
    [SerializeField] float mainThrust=1f;
    [SerializeField] float swingThrust=1f;
    [SerializeField] AudioClip mainSound;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem thrustLeftParticle;
    [SerializeField] ParticleSystem thrustRightParticle;
    // Start is called before the first frame update
    void Start()
    {
        bufferRb = GetComponent<Rigidbody>();
        bufferAuS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrusting();
        Swinging();
    }

    void Thrusting(){
        if (Input.GetKey(KeyCode.Space)){
            if(!bufferAuS.isPlaying){
                thrustParticle.Play();
                bufferAuS.PlayOneShot(mainSound);
            } 
            bufferRb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
        } else {
            thrustParticle.Stop();
            if(bufferAuS.isPlaying){
                bufferAuS.Stop();
            }    
        }
    }

    void Swinging(){
        if (Input.GetKey(KeyCode.A)){
            if ( !thrustRightParticle.isPlaying){
                thrustRightParticle.Play();
            }
            handleRotation(1f);
        }else if (Input.GetKey(KeyCode.D)){
            if( !thrustLeftParticle.isPlaying){
                thrustLeftParticle.Play();
            }
            handleRotation(-1f);
        }else{
            thrustLeftParticle.Stop();
            thrustRightParticle.Stop();
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
