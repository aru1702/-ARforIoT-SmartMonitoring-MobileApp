using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyArImageTargetScript : MonoBehaviour
{

    public Button alertPanelButton, resetIdButton;
    public GameObject alertPanel, countdownPanel, timeRecordPanel;
    public Text timeRecordText;
    private float myTime;

    private void OnEnable() {
        timeRecordPanel.SetActive(false);
        // PlayerPrefs.SetString("app__go_time_record", "False");
        // PlayerPrefs.SetString("app__end_time_record", "False");

        string recordTime = PlayerPrefs.GetString("app__time_record");
        // if (recordTime == "True") {
        //     alertPanel.SetActive(true);
        //     PlayerPrefs.SetString("app__go_time_record", "False");
        // }

        if (recordTime == "True")
            timeRecordPanel.SetActive(true);

        if (PlayerPrefs.GetString("device__id") != "" || PlayerPrefs.GetString("device__id") != null) 
            resetIdButton.interactable = true;
    }

    public void Start () {
        alertPanelButton.onClick.AddListener(alertButtonClick);
        resetIdButton.onClick.AddListener(resetIdButtonClick);
        myTime = 0.0f;
    }

    public void alertButtonClick () {
        alertPanel.SetActive(false);
        countdownPanel.SetActive(true);
    }

    public void resetIdButtonClick () {
        PlayerPrefs.SetString("device__id", "");
        resetIdButton.interactable = false;
    }

    // counting delta time as duration of marker detection
    private void Update() {
        string goTimeRecord = PlayerPrefs.GetString("app__go_time_record");
        string endTimeRecord = PlayerPrefs.GetString("app__end_time_record");

        timeRecordText.text = "Your time: " + myTime.ToString() + " s";

        if (goTimeRecord == "True" && endTimeRecord == "False") {
            myTime += Time.deltaTime;
            timeRecordText.text = "Your time: " + myTime.ToString() + " s";
        }

        // if (goTimeRecord == "False" && endTimeRecord == "True") {
        //     timeRecordText.text = "Your time: " + myTime.ToString() + " s";
        //     timeRecordPanel.SetActive(true);
        // }
        
        // Debug.Log(goTimeRecord + " " + endTimeRecord);
    }
    
}
