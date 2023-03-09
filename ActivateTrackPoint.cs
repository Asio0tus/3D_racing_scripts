using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrackPoint : TrackPoint
{
    [SerializeField] private GameObject activeTargetPointSFX;

    private void Start()
    {
        activeTargetPointSFX.SetActive(isTarget);
    }

    protected override void OnPassed()
    {
        activeTargetPointSFX.SetActive(false);
    }

    protected override void OnAssignAsTarget()
    {
        activeTargetPointSFX.SetActive(true);
    }
}
