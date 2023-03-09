using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDashboardUpdate : MonoBehaviour, IDependency<RaceStateTracker>
{
    private const float DASHBOARD_MAX_RPM = 9000f;
    private const float DASHBOARD_MAX_DEG_RPM = 180f;
    private const float DASHBOARD_MAX_SPEED = 260f;
    private const float DASHBOARD_MAX_DEG_SPEED = 260f;


    [SerializeField] private Transform arrowSpeed;
    [SerializeField] private Transform arrowRpm;
    [SerializeField] private Image[] gearsImagesSelect;
    [SerializeField] private Image gearRearImageSelect;
    [SerializeField] private Text speedText;
    [SerializeField] private Car playerCar;

    [SerializeField] private GameObject dashboardUI;

    private float currentSpeedDeg;
    private float currentRpmDeg;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.PeparationStarted += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        dashboardUI.SetActive(false);
    }    

    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void Update()
    {
        UpdateRpmDisplay();
        UpdateSpeedDisplay();
        UpdateGearDisplay();
    }

    private void OnRaceCompleted()
    {
        dashboardUI.SetActive(false);
    }

    private void OnRaceStarted()
    {
        dashboardUI.SetActive(true);
    }

    private void UpdateSpeedDisplay()
    {
        currentSpeedDeg = Mathf.Abs(playerCar.Speed) * (DASHBOARD_MAX_DEG_SPEED / DASHBOARD_MAX_SPEED);

        float angleZSpeed = 0;

        if (Mathf.Abs(arrowSpeed.position.z) > currentSpeedDeg) angleZSpeed = Mathf.Abs(arrowSpeed.position.z) - currentSpeedDeg;
        if (Mathf.Abs(arrowSpeed.position.z) < currentSpeedDeg) angleZSpeed = -(currentSpeedDeg - Mathf.Abs(arrowSpeed.position.z));

        arrowSpeed.rotation = Quaternion.Euler(0, 0, angleZSpeed);

        speedText.text = Mathf.Abs(playerCar.Speed).ToString();
    }

    private void UpdateRpmDisplay()
    {
        currentRpmDeg = Mathf.Abs(playerCar.EngineRpm) * (DASHBOARD_MAX_DEG_RPM / DASHBOARD_MAX_RPM);
                
        float angleZ = 0;

        if(Mathf.Abs(arrowRpm.position.z) > currentRpmDeg) angleZ = Mathf.Abs(arrowRpm.position.z) - currentRpmDeg;
        if(Mathf.Abs(arrowRpm.position.z) < currentRpmDeg) angleZ = -(currentRpmDeg - Mathf.Abs(arrowRpm.position.z));

        arrowRpm.rotation = Quaternion.Euler(0, 0, angleZ);      

    }

    private void UpdateGearDisplay()
    {
        if(playerCar.SelectedGear >= 0)
        {
            int gearIndex = playerCar.GetSelectedRearIndex();

            for (int i = 0; i < gearsImagesSelect.Length; i++)
            {
                if (i == gearIndex)
                {
                    gearsImagesSelect[i].enabled = true;
                }
                else
                {
                    gearsImagesSelect[i].enabled = false;
                }

            }

            gearRearImageSelect.enabled = false;
            
        }
        if(playerCar.SelectedGear < 0 && playerCar.Speed <= 0)
        {            
            OffAllNumbersGearsOnDisplay();
            gearRearImageSelect.enabled = true;
        }
        if(playerCar.SelectedGear == 0)
        {
            OffAllNumbersGearsOnDisplay();
            gearRearImageSelect.enabled = false;
        }
    }
    
    private void OffAllNumbersGearsOnDisplay()
    {
        foreach(Image image in gearsImagesSelect)
        {
            image.enabled = false;
        }
    }
}
