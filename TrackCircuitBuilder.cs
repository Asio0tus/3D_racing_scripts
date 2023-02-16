using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackCircuitBuilder
{
   public static TrackPoint[] Build(Transform trackTransform, TrackType trackType)
    {
        TrackPoint[] trackPoints = new TrackPoint[trackTransform.childCount];

        ResetPoints(trackTransform, trackPoints);

        MakeLinks(trackPoints, trackType);

        InitializePoint(trackPoints, trackType);

        return trackPoints;
    }

    private static void ResetPoints(Transform trackTransform, TrackPoint[] trackPoints)
    {
        for (int i = 0; i < trackPoints.Length; i++)
        {
            trackPoints[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

            if (trackPoints[i] == null)
            {
                Debug.LogError($"No TrackPoint script on {i} child object");
                return;
            }

            trackPoints[i].ResetProperties();
        }
    }

    private static void MakeLinks(TrackPoint[] trackPoints, TrackType trackType)
    {
        for (int i = 0; i < trackPoints.Length - 1; i++)
        {
            trackPoints[i].NextPoint = trackPoints[i + 1];
        }

        if (trackType == TrackType.Circular)
            trackPoints[trackPoints.Length - 1].NextPoint = trackPoints[0];
    }

    private static void InitializePoint (TrackPoint[] trackPoints, TrackType trackType)
    {
        trackPoints[0].IsFirst = true;

        if (trackType == TrackType.Sprint)
            trackPoints[trackPoints.Length - 1].IsLast = true;

        if (trackType == TrackType.Circular)
            trackPoints[0].IsLast = true;
    }
}
