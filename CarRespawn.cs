using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRespawn : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
{
    [SerializeField] private float respawnHeight;
    private TrackPoint respawnTrackPoint;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private Car car;
    public void Construct(Car obj) => car = obj;

    private CarInputControl carInputControl;
    public void Construct(CarInputControl obj) => carInputControl = obj;

    private void Start()
    {
        raceStateTracker.TrackPointPassed += OnTrackPointpassed;
    }

    private void OnDestroy()
    {
        raceStateTracker.TrackPointPassed -= OnTrackPointpassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
            RespawnCar();
    }

    private void OnTrackPointpassed(TrackPoint point)
    {
        respawnTrackPoint = point;
    }

    public void RespawnCar()
    {
        if (respawnTrackPoint == null) return;

        if (raceStateTracker.State != RaceState.Race) return;

        Vector3 position = new Vector3(respawnTrackPoint.transform.position.x, respawnTrackPoint.transform.position.y * respawnHeight, 
            respawnTrackPoint.transform.position.z);

        car.Respawn(position + respawnTrackPoint.transform.up * respawnHeight, respawnTrackPoint.transform.rotation);

        //print(respawnTrackPoint.transform.up);

        carInputControl.Reset();
    }
}
