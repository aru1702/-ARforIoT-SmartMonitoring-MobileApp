using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Globalization;
using System.IO;
using Skytanet.SimpleDatabase;

public class EasyArImageTargetScript : MonoBehaviour
{

    public Button alertPanelButton, resetIdButton, retryButton, saveDataBtn;
    public Toggle autoSaveTgl;
    public GameObject alertPanel, countdownPanel, timeRecordPanel;
    public Text timeRecordText;
    private float myTime;
    private bool autoSaveB, alreadySaved;
    public SaveFile saveFile;
    public Text autoSavedText;

    private readonly string AUTO_SAVED = "autosave";
    private readonly string DB_NAME = "myDb";
    private readonly string LAST_OPERATOR = "last";

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

        // Debug.Log("ID: " + PlayerPrefs.GetString("device__id"));

        if (PlayerPrefs.GetString("device__id") != "") 
            resetIdButton.interactable = true;
        else
            resetIdButton.interactable = false;

        if (PlayerPrefs.GetString(AUTO_SAVED) == "True") {
            autoSaveTgl.GetComponent<Toggle>().isOn = true;
            autoSaveB = true;
        } else {
            autoSaveTgl.GetComponent<Toggle>().isOn = false;
            autoSaveB = false;
        }
    }

    public void CloseDb () {
        saveFile.Close();
    }

    public void Start () {
        alertPanelButton.onClick.AddListener(alertButtonClick);
        resetIdButton.onClick.AddListener(resetIdButtonClick);
        saveDataBtn.onClick.AddListener(saveDataBtnClick);

        myTime = 0.0f;

        saveDataBtn.interactable = false;
        retryButton.interactable = false;

        autoSaveTgl.onValueChanged.AddListener((value) => {
            autoSaveB = value;
            Debug.Log("Save: " + autoSaveB);
            PlayerPrefs.SetString(AUTO_SAVED, autoSaveB.ToString());
        });      

        OpenDb();
    }

    void OpenDb () {
        try
        {
            saveFile.Close();
            saveFile = new SaveFile(DB_NAME);  
        }
        catch (System.Exception)
        {
            Debug.Log("failed");
            saveFile = new SaveFile(DB_NAME);
            throw;
        }
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
        // Debug.Log(GetLastKeyString());
        autoSavedText.text = GetLastKeyString();

        string goTimeRecord = PlayerPrefs.GetString("app__go_time_record");
        string endTimeRecord = PlayerPrefs.GetString("app__end_time_record");

        timeRecordText.text = "Your time: " + myTime.ToString() + " s";

        if (goTimeRecord == "True" && endTimeRecord == "False") {
            myTime += Time.deltaTime;
            timeRecordText.text = "Your time: " + myTime.ToString() + " s";
            alreadySaved = false;
        }

        if (goTimeRecord == "False" && endTimeRecord == "True") {
            // timeRecordText.text = "Your time: " + myTime.ToString() + " s";
            // timeRecordPanel.SetActive(true);
            retryButton.interactable = true;
            saveDataBtn.interactable = true;

            if (autoSaveB && !alreadySaved) {
                saveDataBtnClick();
                alreadySaved = true;
            }
        }
        
        // Debug.Log(goTimeRecord + " " + endTimeRecord);
    }

    private void SaveData (string key, string value) {
        saveFile.Set(key, value);
        saveFile.Set(LAST_OPERATOR, key);
    }

    void saveDataBtnClick () {
        OpenDb();
        string lastKeyS = GetLastKeyString();

        SaveData(lastKeyS, myTime.ToString());
    }

    private string GetLastKeyString() {
        int lastKey = GetLastValue() + 1;
        string lastKeyS = lastKey.ToString();
        
        if (lastKey < 100)
            lastKeyS = "0" + lastKeyS;
        if (lastKey < 10)
            lastKeyS = "0" + lastKeyS;

        return lastKeyS;
    }

    private int GetLastValue () {
        List<string> keys = saveFile.GetKeys();
        
        for (int i = 0; i < keys.Count; ++i) {
            if (keys[i] == LAST_OPERATOR) {
                string lastO = saveFile.Get<string>(keys[i]);
                Debug.Log("lastO: " + lastO);
                return int.Parse(lastO);
            }
        }

        return 0;
    }
    
}
