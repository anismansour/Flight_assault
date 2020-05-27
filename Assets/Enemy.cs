using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    // Start is called before the first frame update
    void Start()
    {

        //add box collider from code 
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    // method copy paste works from documentation 
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        print("hit enemy" + gameObject.name);
        Destroy(gameObject);
    }
}
