using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownSceneScript : MonoBehaviour
{

    public GameObject alertPanel, countdownPanel;
    public Button alertPanelButton, backButton;
    public Text countdownText;
    private int countdown, localCount;
    private bool isReady;

    // Start is called before the first frame update
    void Start()
    {
        countdownText.text = "3";
        countdown = 3;
        localCount = 50;
        isReady = false;

        alertPanel.SetActive(true);
        countdownPanel.SetActive(false);

        alertPanelButton.onClick.AddListener(alertButtonClick);
        backButton.onClick.AddListener(GoBack);

        PlayerPrefs.SetString("app__go_time_record", "False");
    }

    void GoBack () {
		SceneManager.LoadScene(2);
	}

    // Update is called once per frame
    void Update()
    {
        if (isReady) {
            Counting();
        }
    }

    void Counting () {
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

    public void alertButtonClick () {
        alertPanel.SetActive(false);
        countdownPanel.SetActive(true);
        isReady = true;
    }

    void SetPanelOff () {
        Debug.Log("Counting time now!");
        // PlayerPrefs.SetString("app__go_time_record", "True");
        Debug.Log(PlayerPrefs.GetString("app__time_record") + " " + PlayerPrefs.GetString("app__go_time_record"));
        SceneManager.LoadScene(3);
    }
}
