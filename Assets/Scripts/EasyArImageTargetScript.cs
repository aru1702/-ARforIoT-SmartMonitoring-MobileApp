using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyArImageTargetScript : MonoBehaviour
{

    public Button alertPanelButton;
    public GameObject alertPanel, countdownPanel, timeRecordPanel;
    public Text timeRecordText;
    private float myTime;

    private void OnEnable() {
        timeRecordPanel.SetActive(false);
        // PlayerPrefs.SetString("app__go_time_record", "False");
        // PlayerPrefs.SetString("app__end_time_record", "False");

        string recordTime = PlayerPrefs.GetString("app__time_record");
        if (recordTime == "True") {
            alertPanel.SetActive(true);
            PlayerPrefs.SetString("app__go_time_record", "False");
        }
    }

    public void Start () {
        alertPanelButton.onClick.AddListener(alertButtonClick);
    }

    public void alertButtonClick () {
        alertPanel.SetActive(false);
        countdownPanel.SetActive(true);
    }

    private void Update() {
        string goTimeRecord = PlayerPrefs.GetString("app__go_time_record");
        string endTimeRecord = PlayerPrefs.GetString("app__end_time_record");

        if (goTimeRecord == "True" && endTimeRecord == "False") {
            myTime += Time.deltaTime;
            timeRecordText.text = "Your time: " + myTime.ToString() + " s";
        }

        if (endTimeRecord == "True") {
            timeRecordPanel.SetActive(true);
        }
        
        // Debug.Log(goTimeRecord + " " + endTimeRecord);
    }
    
}
