using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;
using Models;
using Proyecto26;

public class MainMenuScript : MonoBehaviour
{

    public Toggle timeRecord;
    public Button toMonitoring, toProfile, toQuit, logoutButton;
    public Button yesLogout, noLogout;
    public Text userName, userEmail, user_Id, alertText;
    public GameObject loadingPanel, logoutPanel, alertPanel;
    private RequestHelper currentRequest;

    private bool timeRecordV = false;

    private void OnEnable() {
        // Debug.Log(PlayerPrefs.GetString("app__time_record"));
        if (PlayerPrefs.GetString("app__time_record") == "True") {
            timeRecord.GetComponent<Toggle>().isOn = true;
        } else {
            timeRecord.GetComponent<Toggle>().isOn = false;
        }
    }

    void Start () 
    {
        string userId = PlayerPrefs.GetString("user__id");
        string getUserDetailUrl = "https://myionic-c4817.firebaseapp.com/api/v1/User/GetUser/" + userId;
        
        // get user details for Profile
        RestClient.Get(getUserDetailUrl).Then(res_getUserDetailUrl => {
            string result_getUserDetailUrl = res_getUserDetailUrl.Text;
            GetUserDetailModel model_getUserDetailUrl = JsonUtility.FromJson<GetUserDetailModel>(result_getUserDetailUrl);

            // Debug.Log(model_getUserDetailUrl.result.name);
            // Debug.Log(model_getUserDetailUrl.result.email);

            user_Id.text = userId;
            userName.text = model_getUserDetailUrl.result.name;
            userEmail.text = model_getUserDetailUrl.result.email;
        });

        // when timeRecord toggle has change
        timeRecord.onValueChanged.AddListener((value) => {
            timeRecordV = value;
            Debug.Log("Record " + timeRecordV);
            PlayerPrefs.SetString("app__time_record", timeRecordV.ToString());
        });

        // every button onClick listener
        toMonitoring.onClick.AddListener(GoToMonitoring);
        toProfile.onClick.AddListener(GoToProfile);
        toQuit.onClick.AddListener(GoToQuit);
        
        logoutButton.onClick.AddListener(ConfirmLogoutButton);
        yesLogout.onClick.AddListener(GoLogoutYes);
        noLogout.onClick.AddListener(GoLogoutNo);
    }

    private void GoToMonitoring () {
        Debug.Log("To monitoring");
        SceneManager.LoadScene(3);
    }

    private void GoToProfile () {
        Debug.Log("To Profile");
        // change scene is controlled from GUI
    }

    private void GoToQuit () {
        Debug.Log("To Quit");
        Application.Quit();
    }

    private void ConfirmLogoutButton () {
        logoutPanel.SetActive(true);
    }

    private void GoLogoutNo () {
        logoutPanel.SetActive(false);
    }

    private void GoLogoutYes () {
        logoutPanel.SetActive(false);
        loadingPanel.SetActive(true);

        string userId = PlayerPrefs.GetString("user__id");
        string userLogoutUrl = "https://myionic-c4817.firebaseapp.com/api/v1/User/Logout";

        // POST method to logout
        currentRequest = new RequestHelper {
            Uri = userLogoutUrl,
            Body = new LogoutPostModel {
                id = userId.ToString()
            },
            EnableDebug = true
        };

        RestClient.Post<LoginModelRes>(currentRequest).Then(res => {

            if (res.code == 200) {
                // remove all data
                PlayerPrefs.SetString("user__id", "");
                PlayerPrefs.SetString("user__email_address", "");
                PlayerPrefs.SetString("app__time_record", "False");
                PlayerPrefs.SetString("device__id", "");

                loadingPanel.SetActive(false);
                SceneManager.LoadScene(1);
            } else {
                loadingPanel.SetActive(false);
                alertPanel.SetActive(true);
                alertText.text = "Error while performing logout, check your connection and please try again!";
            }

        }).Catch(err => {
            loadingPanel.SetActive(false);
            alertPanel.SetActive(true);
            alertText.text = "Error while performing logout, check your connection and please try again!";
        });
    }
    
}
