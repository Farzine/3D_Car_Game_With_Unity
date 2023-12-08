using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCarController : MonoBehaviour
{
    [Header("Wheels collider")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider; 
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;

    [Header("Car Engine")]
    public float accelerationForce = 300f;
    public float breakingForce = 3000f;
    private float presentBreakForce = 0f;
    private float presentAcceleration = 0f;

    [Header("Car Steering")]
    public float WheelsTorque = 35f;
    private float presentTurnAngle = 0f;

    [Header("Car Sounds")]
    public AudioSource audioSource;
    public AudioClip accelerationSound;
    public AudioClip slowAccelerationSound;
    public AudioClip stopSound;



    private void Update()
    {
        MoveCar();
        CarSteering();
    }

    private void MoveCar()
    {
        //forward 
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        //backword
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * SimpleInput.GetAxis("Vertical");

        if(presentAcceleration > 0)
        {
            audioSource.PlayOneShot(accelerationSound, 0.5f);
        }
        else if(presentAcceleration < 0)
        {
            audioSource.PlayOneShot(slowAccelerationSound, 0.5f);
        }
        else if(presentAcceleration == 0)
        {
            audioSource.PlayOneShot(stopSound, 0.5f);
        }

    }   
    
    private void CarSteering()
    {
        presentTurnAngle = WheelsTorque * SimpleInput.GetAxis("Horizontal");
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
        frontRightWheelCollider.steerAngle = presentTurnAngle;


        /*SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);*/
    }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position; 
        WT.rotation = rotation;

    }

    public void ApplyBreaks()
    {
        StartCoroutine(carBreaks());
    }

    IEnumerator carBreaks()
    {
        presentBreakForce = breakingForce;

        frontLeftWheelCollider.brakeTorque = presentBreakForce; 
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;

        yield return new WaitForSeconds(2f);

        presentBreakForce = 0f;

        frontLeftWheelCollider.brakeTorque = presentBreakForce; 
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
    }
}
