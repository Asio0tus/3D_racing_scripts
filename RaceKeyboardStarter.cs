using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour
{
    [SerializeField] private RaceStateTracker raceStateTracker;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) == true)
        {
            raceStateTracker.LaunchPeparationStart();
        }
    }
}
