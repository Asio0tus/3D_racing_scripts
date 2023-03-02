using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve brakeCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField] [Range(0.0f, 1.0f)] private float autoBrakeStrength = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handBrakeAxis;

    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBrake();
        
        UpdateSteer();     
                
        UpdateAutoBrake();

        //print(wheelSpeed);
    }    

    private void UpdateThrottleAndBrake()
    {
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }        

        //Gear

        if(verticalAxis < 0 && car.Speed <= 0) //wheelSpeed > -0.5f && wheelSpeed <= 0.5f
        {
            car.ShiftToReverseGear();
        }

        if ((verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)) //|| (verticalAxis > 0 && car.Speed > 0 && car.SelectedGear < 0)
        {
            car.ShiftToFirstGear();
        }
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(wheelSpeed / car.MaxSpeed) * horizontalAxis;
    }
        
    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handBrakeAxis = Input.GetAxis("Jump");
    }

    private void UpdateAutoBrake()
    {
        if(verticalAxis == 0)
        {
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBrakeStrength;
        }
    }

    public void StopCar()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handBrakeAxis = 0;

        car.ThrottleControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 1;
    }

}
