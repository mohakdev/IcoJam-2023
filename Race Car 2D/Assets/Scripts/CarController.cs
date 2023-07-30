using RadiantTools.AudioSystem;
using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float accelerationPower = 30;
    public float turnPower = 3;
    public int maxVelocity;
    [SerializeField] float driftFactor = 0.8f;
    [Header("References")]
    [SerializeField] TrailRenderer[] skidRenderers;
 
    [NonSerialized] public float accelerationInput; //Lies b/w 0 to 1
    float steeringInput; //Lies b/w 0 to 1
    //Additional Variables
    float rotationAngle;
    float forwardVelocity = 0;
    Rigidbody2D rbCar;

    void Start()
    {
        rbCar = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        EngineForce();
        KillSideVelocity();
        SteeringForce();
    }

    void SteeringForce()
    {
        //Limiting rotation if velocity is less
        float minVelBeforeRot = rbCar.velocity.magnitude / 8;
        minVelBeforeRot = Mathf.Clamp01(minVelBeforeRot);

        //Applying centripetal acceleration to Car
        rotationAngle -= steeringInput * turnPower * minVelBeforeRot;
        rbCar.MoveRotation(rotationAngle);
    }

    void EngineForce()
    {
        forwardVelocity = Vector2.Dot(transform.right, rbCar.velocity);

        //Limiting the velocity so it doesnt go higher than max Velocity
        if(forwardVelocity > maxVelocity && accelerationInput > 0) { return; }
        if (forwardVelocity < -maxVelocity * 0.5f && accelerationInput < 0) { return; }
        if (rbCar.velocity.sqrMagnitude > maxVelocity && accelerationInput > 0) { return; }


        //To Slow down the car
        if (accelerationInput == 0)
        {
            rbCar.drag = Mathf.Lerp(rbCar.drag, 2f, Time.deltaTime * 2);
        }
        else
        {
            rbCar.drag = 0;
        }

        //Applying tangential acceleration to car
        Vector2 engineForce = transform.right * accelerationInput * accelerationPower;
        rbCar.AddForce(engineForce, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("EnemyCar")) { return; }
        AudioManager.Instance.PlayAudio(AudioManager.SoundTypes.CollisionSound);    
    }

    public void DriftTires(bool drift)
    {
        if (drift)
        {
            turnPower *= 1.6f;
            accelerationPower /= 1.2f;
            
            AudioManager.Instance.PlayAudio(AudioManager.SoundTypes.DriftSound);
            SetSkidmarksTrail(true);
        }
        else
        {
            turnPower /= 1.6f;
            accelerationPower *= 1.2f;
            
            SetSkidmarksTrail(false);
        }
    }
    void SetSkidmarksTrail(bool state)
    {
        foreach(TrailRenderer trail in skidRenderers)
        {
            trail.emitting = state;
        }
    }

    //Killing side velocity
    void KillSideVelocity()
    {
        Vector2 forwardVelocity = transform.right * Vector2.Dot(rbCar.velocity, transform.right);
        Vector2 sideVelocity = transform.up * Vector2.Dot(rbCar.velocity, transform.up);

        rbCar.velocity = forwardVelocity + sideVelocity * driftFactor;
    }

    public void SetInputVector(float accInput, float steerInput)
    {
        accelerationInput = accInput;
        steeringInput = steerInput;
    }
}
