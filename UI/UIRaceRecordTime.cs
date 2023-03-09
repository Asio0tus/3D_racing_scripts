using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
{
    [SerializeField] private GameObject goldRecordOblect;    
    [SerializeField] private Text golgRecordTime;    

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        goldRecordOblect.SetActive(false);        
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        goldRecordOblect.SetActive(true);
        golgRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
    }

    private void OnRaceCompleted()
    {
        goldRecordOblect.SetActive(false);       
    }
}
