using UnityEngine.SceneManagement;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    //Serialize variable
    [SerializeField] float delayTime=1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;
    
    //Cache variable
    AudioSource bufferAus;

    //State variable
    bool isTransitioning = false;
    bool isImmortal = false;

    void Start(){
        bufferAus=GetComponent<AudioSource>();
    }

    void Update(){
        cheatNextLevel();
        cheatImmortal();
    }

    void OnCollisionEnter(Collision other) {

        if ( isTransitioning ) return;
        if ( isImmortal) return;
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("this is starting point");
                break;
            case "Finish":
                Debug.Log("this is curr level"+SceneManager.GetActiveScene().buildIndex);
                StartCrashSequences("LoadNextLevel",successSound,successParticle);
                break;
            case "Obstacle":
                Debug.Log("you dead");
                StartCrashSequences("ReloadLevel",deathSound,deathParticle);
                break;            
        }
    }

    void StartCrashSequences(string methodType,AudioClip sound,ParticleSystem particle){
        isTransitioning=true;
        particle.Play();
        bufferAus.Stop();
        bufferAus.PlayOneShot(sound);
        GetComponent<Movement>().enabled=false;
        Invoke(methodType,delayTime);
    }
    void ReloadLevel(){
        isTransitioning=false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel(){
        isTransitioning=false;
        int nextLevelIndex=SceneManager.GetActiveScene().buildIndex+1;
        if(nextLevelIndex==SceneManager.sceneCountInBuildSettings){
            nextLevelIndex=0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    void cheatNextLevel(){
        if (Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
    }

    void cheatImmortal(){
        if (Input.GetKeyDown(KeyCode.C)){
            Debug.Log("immortal");
            isImmortal=!isImmortal;
        }
    }
}
