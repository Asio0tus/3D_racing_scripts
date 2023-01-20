using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{    
    
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [SerializeField] private float maxSpeed;

        

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;    
    [SerializeField] private float engineMaxTorque;    
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    //public for DEBUG
    [SerializeField] private float selectedGear;
    [SerializeField] private int selectedGearIndex;
    [SerializeField] private float rearGear;


    public float LinearVelocity => carChassis.LinearVelocity;
    public float WheelSpeed => carChassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;
    public float SelectedGear => selectedGear;    


    private CarChassis carChassis;

    //public for DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    public int Speed;
    [SerializeField] private float engineTorque;
    [SerializeField] private float engineRpm;


    public float EngineRpm => engineRpm;


    private void Start()
    {
        carChassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        Speed = (int)LinearVelocity;

        UpdateEngineTorque();
        AutoGearShift();

        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        carChassis.MotorTorque = engineTorque * ThrottleControl;
        carChassis.BrakeTorque = maxBrakeTorque * BrakeControl;
        carChassis.SteerAngle = maxSteerAngle * SteerControl;
    }
       

    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(carChassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear);
    }

    //Gearbox

    private void AutoGearShift()
    {
        if (selectedGear < 0) return;
        if (engineRpm >= upShiftEngineRpm) UpGear();
        if (engineRpm < downShiftEngineRpm) DownGear();
    }

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
    }

    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
    }

    public void ShiftToNeutral()
    {
        selectedGear = 0;
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public int GetSelectedRearIndex()
    {
        return selectedGearIndex;
    }
}
