using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{    
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [SerializeField] private float maxSpeed;

    [SerializeField] private AnimationCurve engineTorqueCurve;    

    public float LinearVelocity => carChassis.LinearVelocity;
    public float WheelSpeed => carChassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    private CarChassis carChassis;

    //public for DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;

    private void Start()
    {
        carChassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;

        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        carChassis.MotorTorque = engineTorque * ThrottleControl;
        carChassis.BrakeTorque = maxBrakeTorque * BrakeControl;
        carChassis.SteerAngle = maxSteerAngle * SteerControl;
    }
}
