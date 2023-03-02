using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{   

    [SerializeField] private Text text;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.PeparationStarted += OnRacePeparationStarted;
        raceStateTracker.Started += OnStarted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnRacePeparationStarted;
        raceStateTracker.Started -= OnStarted;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "GO!";
    }

    private void OnStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnRacePeparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }   
    
}
