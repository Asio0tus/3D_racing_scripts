using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>
{    
    [SerializeField] private new Camera camera;
    
    [SerializeField] private CarCameraFollow carFollower;
    [SerializeField] private CameraPathFollower pathFollower;
    [SerializeField] private CarCameraFovCorrector fovCorrector;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private Car car;
    public void Construct(Car obj) => car = obj;

    private void Awake()
    {
        carFollower.SetProperties(car, camera);
        fovCorrector.SetProperties(car, camera);
    }

    private void Start()
    {
        raceStateTracker.PeparationStarted += OnPeparationStarted;
        raceStateTracker.Completed += OnCompleted;

        carFollower.enabled = false;
        pathFollower.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnPeparationStarted;
        raceStateTracker.Completed -= OnCompleted;
    }

    private void OnPeparationStarted()
    {
        carFollower.enabled = true;
        pathFollower.enabled = false;
    }

    private void OnCompleted()
    {
        pathFollower.enabled = true;
        pathFollower.StartMoveToNearestPoint();
        pathFollower.SetLookTarget(car.transform);

        carFollower.enabled = false;
    }


}
