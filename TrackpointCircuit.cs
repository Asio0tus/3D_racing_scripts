using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}

public class TrackpointCircuit : MonoBehaviour
{
    public event UnityAction<int> LapCompleted;
    public event UnityAction<TrackPoint> TrackPointTriggered;

    [SerializeField] private TrackType trackType;
    public TrackType Type => trackType;

    private TrackPoint[] trackPoints;

    private int lapsCompleted = -1;

    private void Start()
    {
        BuildCircuit();

        for(int i = 0; i < trackPoints.Length; i++)
        {
            trackPoints[i].Triggered += OnTrackPointTriggered;
        }

        trackPoints[0].AssignAsTarget();
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        trackPoints = TrackCircuitBuilder.Build(transform, trackType);
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.NextPoint?.AssignAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if(trackPoint.IsLast == true)
        {
            lapsCompleted++;

            if(trackType == TrackType.Sprint)
                LapCompleted?.Invoke(lapsCompleted);

            if(trackType == TrackType.Circular)
            {
                if(lapsCompleted > 0)
                    LapCompleted?.Invoke(lapsCompleted);
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < trackPoints.Length; i++)
        {
            trackPoints[i].Triggered -= OnTrackPointTriggered;
        }
    }
}
