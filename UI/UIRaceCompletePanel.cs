using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceCompletePanel : MonoBehaviour, IDependency<RaceResultTime>
{
    [SerializeField] private GameObject raceCompletePanel;
    [SerializeField] private Text textRaceComplete;
    [SerializeField] private Text textNewRecord;
    [SerializeField] private Text textYourResult;
    [SerializeField] private Text textGoldTime;

    private bool isNewRecordSet = false;
    private float goldTimeOnStart;
    private float playerTime;
       

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;


    private void Start()
    {        
        raceResultTime.ResultUpdated += OnNewRecordSet;

        raceCompletePanel.SetActive(false);
        goldTimeOnStart = raceResultTime.GoldTime;
    }

    private void OnDestroy()
    {        
        raceResultTime.ResultUpdated -= OnNewRecordSet;
    }    

    private void OnNewRecordSet()
    {
        playerTime = raceResultTime.CurrentTime;

        isNewRecordSet = (playerTime < goldTimeOnStart);

        raceCompletePanel.SetActive(true);

        textGoldTime.text = ("BEST RESULT:  " + StringTime.SecondToTimeString(goldTimeOnStart));
        textYourResult.text = ("YOUR RESULT:  " + StringTime.SecondToTimeString(raceResultTime.CurrentTime));

        if (isNewRecordSet)
        {
            textNewRecord.enabled = true;
            textRaceComplete.enabled = false;
        }
        else
        {
            textNewRecord.enabled = false;
            textRaceComplete.enabled = true;
        }
    }

}
