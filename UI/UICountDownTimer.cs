using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour
{
    [SerializeField] private RaceStateTracker raceStateTracker;

    [SerializeField] private Text text;
    [SerializeField] private Timer countdownTimer;

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
        text.text = countdownTimer.Value.ToString("F0");

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
