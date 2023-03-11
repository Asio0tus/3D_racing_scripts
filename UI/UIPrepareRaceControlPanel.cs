using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrepareRaceControlPanel : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject panelUI;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.PeparationStarted += OnPrepareRaceStarted;

        panelUI.SetActive(true);
    }

    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnPrepareRaceStarted;
    }

    private void OnPrepareRaceStarted()
    {
        panelUI.SetActive(false);
    }
}
