using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // to load scene


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject deasthFX;
    // Start is called before the first frame update
   
    //related to rigidbody 
    //void OnCollisionEnter(Collision collision)
    //{
    //    print("Collision");
    //}

    //related to box collider tigger when tocuh terrain 
    void OnTriggerEnter(Collider other)
    {
        Playerdead();
        
        deasthFX.SetActive(true);
        Invoke("Restart", levelLoadDelay);
    }

    private void Restart()
    {
        SceneManager.LoadScene(1);
    }


   private void Playerdead()
    {
        print("dead");
        SendMessage("OnDeath");   //send onDeath to player script
    }
}
