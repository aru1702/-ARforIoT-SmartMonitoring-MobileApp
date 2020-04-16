using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Toggle timeRecord;
    public Button toMonitoring, toProfile, toQuit;

    private bool timeRecordV = false;

    void Start () 
    {
        timeRecord.onValueChanged.AddListener((value) => {
            timeRecordV = value;
            Debug.Log(timeRecordV);
        });

        toMonitoring.onClick.AddListener(GoToMonitoring);
        toProfile.onClick.AddListener(GoToProfile);
        toQuit.onClick.AddListener(GoToQuit);
    }

    public void GoToMonitoring () {
        Debug.Log("To monitoring");
    }

    public void GoToProfile () {
        Debug.Log("To Profile");
    }

    public void GoToQuit () {
        Debug.Log("To Quit");
    }
    
}
