using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependency<T>
{
    void Construct(T obj);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private TrackpointCircuit trackPointCircuit;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;

    private void Bind(MonoBehaviour mono)
    {
        if (mono is IDependency<TrackpointCircuit>) (mono as IDependency<TrackpointCircuit>).Construct(trackPointCircuit);
        if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(carInputControl);
        if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(raceStateTracker);
        if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(car);
        if (mono is IDependency<CarCameraController>) (mono as IDependency<CarCameraController>).Construct(carCameraController);
        if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>).Construct(raceTimeTracker);
    }

    private void Awake()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();             

        for(int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]); 
        }
    }
}
 