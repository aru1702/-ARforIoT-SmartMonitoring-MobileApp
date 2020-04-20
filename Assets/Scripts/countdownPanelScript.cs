using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownPanelScript : MonoBehaviour
{

    public Text countdownText;
    public GameObject countdownPanel;
    private int countdown, localCount;

    private void OnEnable() {
        countdownText.text = "3";
        countdown = 3;
        localCount = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0) {
            if (localCount <= 0) {
                localCount = 50;
                countdown--;
                countdownText.text = countdown.ToString();
            } else {
                localCount--;
            }
        } else {
            SetPanelOff();
        }
    }

    void SetPanelOff () {
        Debug.Log("Counting time now!");

        PlayerPrefs.SetString("app__go_time_record", "True");
        
        countdownPanel.SetActive(false);
    }
}
