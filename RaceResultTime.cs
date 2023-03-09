using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    public const string SAVE_MARK_RECORDTIME = "_best_time_record";

    public event UnityAction ResultUpdated;

    [SerializeField] private float goldTime;

    private float playerRecortTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayerRecordTime => playerRecortTime;
    public float CurrentTime => currentTime;
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCopmleted;
        Load();
    }
    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCopmleted;
    }

    private void OnRaceCopmleted()
    {   
        if(raceTimeTracker.CurrentTime > playerRecortTime || playerRecortTime == 0)
            playerRecortTime = raceTimeTracker.CurrentTime;

        if (raceTimeTracker.CurrentTime < goldTime)
        {
            goldTime = raceTimeTracker.CurrentTime;
            Save();
            ResultUpdated?.Invoke();
        }           

        currentTime = raceTimeTracker.CurrentTime;       
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SAVE_MARK_RECORDTIME, goldTime);
    }

    private void Load()
    {
        goldTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SAVE_MARK_RECORDTIME, 115.3f);
    }
   
}
