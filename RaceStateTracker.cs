using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum RaceState
{
    Preparation,
    Timered,
    Race,
    Complete
}

public class RaceStateTracker : MonoBehaviour
{
    public event UnityAction PeparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    [SerializeField] private TrackpointCircuit trackPointCircuit;
    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToComplete;

    private RaceState state;
    public RaceState State => state;

    private void Start()
    {   
        StartState(RaceState.Preparation);

        countdownTimer.enabled = false;

        countdownTimer.Finished += OnCountdownFinished;
        trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackPointCircuit.LapCompleted += OnLapCompleted;
    }
        

    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountdownFinished;
        trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
    }

    private void OnCountdownFinished()
    {
        StartRace();
    }

    private void StartState(RaceState state)
    {
        this.state = state;
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }

    

    public void LaunchPeparationStart()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.Timered);

        countdownTimer.enabled = true;
        PeparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.Timered) return;
        StartState(RaceState.Race);
        Started?.Invoke();
    }

    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Complete);
        Completed?.Invoke();
    }

    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }
}
