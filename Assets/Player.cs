using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{

     [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 30f;
    // speed variable  METERS PER SECONDS
    [Tooltip("In m")] [SerializeField] float xRange = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");//refer to everything to an horizontal axis
        //throw if between 0 and 1 , goes up speed depends on the sensitivity and downs on the gravity
        //
        //print(xThrow);
        float xOffset = xThrow * xSpeed * Time.deltaTime; // if click longer he will move further and faster
                                                          //print(xOffset);


        float newXPos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(newXPos, -xRange, xRange); //position is between x range
        //and give the value to the localPosition  
        transform.localPosition = new Vector3(clampedXpos, transform.localPosition.y, transform.localPosition.z);




    }
}
