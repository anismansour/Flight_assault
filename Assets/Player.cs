using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{

     [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 30f;
    // speed variable  METERS PER SECONDS
    [Tooltip("In m")] [SerializeField] float xRange = 24f;
    [Tooltip("In m")] [SerializeField] float yRange = 13f;
    [SerializeField] float RotationXFactor = -2f; //rotate the ship using factor *position
    //"each position multiplied by the factor will give the needed rotation
    [SerializeField] float controlFrontX = -5f; //factor that will controll the noise of the ship when moved up and down 
    [SerializeField] float RotationYFactor = 2f;
    [SerializeField] float RotationZFactor = 1000f;
    [SerializeField] float controlFrontY = -20f;


    float xThrow, yThrow, zThrow; // moved them outside of movex and y () to be able to use them on rotation for moving noise of the ship
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       MoveX();
        MoveY();
       //woopa();
        Rotation();
        
       

    }

   
    private void Rotation()
    {

        // FRONT OF THE SHIP UP AND DOWN MOVEMENT X
        float NewRotationX = transform.localPosition.y * RotationXFactor;
        float ControlNoisePos = yThrow * controlFrontX; //will controll the noise of the ship
        float PosRotationX = NewRotationX + ControlNoisePos;
        // FRONT OF THE SHIP WHEN MOVED LEFT AND RIGHT Y

        float PosRotationY = transform.localPosition.x * RotationYFactor; ;
        float PosRotationZ = xThrow * controlFrontY;
        transform.localRotation = Quaternion.Euler(PosRotationX, PosRotationY, PosRotationZ);



    }

    private void MoveX()
    {
         xThrow = CrossPlatformInputManager.GetAxis("Horizontal");//refer to everything to an horizontal axis
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

    private void MoveY()
    {
         yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        //print(yThrow);
        float yoffset = yThrow * xSpeed * Time.deltaTime;
        float newYPos = transform.localPosition.y + yoffset;
        float clampedYpos = Mathf.Clamp(newYPos, -yRange, yRange);
        transform.localPosition = new Vector3(transform.localPosition.x, clampedYpos, transform.localPosition.z);
    }

    private void woopa()
        //360 
    {
        { 
        zThrow = CrossPlatformInputManager.GetAxis("Jump");
        float zOffset = zThrow * RotationZFactor * Time.deltaTime;
            print(zOffset);
            transform.localRotation = Quaternion.Euler(0f, 0f, zOffset);


        }


    }
}
