using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;
using Models;
using Proyecto26;

public class LoginScript : MonoBehaviour
{

    public InputField email;
    public InputField pass;
    public Button login;
	public GameObject loadingPanel, alertPanel;
	public Text errorText;

    private RequestHelper currentRequest;

    public void Start () {
        login.onClick.AddListener(buttonClick);
    }

    private void LogMessage(string title, string message) {
#if UNITY_EDITOR
		EditorUtility.DisplayDialog (title, message, "Ok");
#else
		Debug.Log(message);
#endif
	}

    public void buttonClick () {
		loadingPanel.SetActive(true);

        string emailV = email.text;
        string passV = pass.text;

        string mainUrl = "https://myionic-c4817.firebaseapp.com/api/v1/User/Login";

        // We can add default query string params for all requests
		// RestClient.DefaultRequestParams["param1"] = "My first param";
		// RestClient.DefaultRequestParams["param3"] = "My other param";

		currentRequest = new RequestHelper {
			Uri = mainUrl,
			// Params = new Dictionary<string, string> {
			// 	{ "param1", "value 1" },
			// 	{ "param2", "value 2" }
			// },
			Body = new LoginModel {
				email = emailV,
                password = passV
			},
			EnableDebug = true
		};

		RestClient.Post<LoginModelRes>(currentRequest).Then(res => {

			// And later we can clear the default query string params for all requests
			// RestClient.ClearDefaultParams();
			// this.LogMessage("Success", JsonUtility.ToJson(res, true));
            // Debug.Log(res.result);

			// success
			if (res.code == 200) {

				// get user ID
				string userGetIdUrl = "https://myionic-c4817.firebaseapp.com/api/v1/User/GetId/" + emailV;
				RestClient.Get(userGetIdUrl).Then(res2 => {
					string getResult = res2.Text;
					GetUserIdModel itemUserId = JsonUtility.FromJson<GetUserIdModel>(getResult);

					if (itemUserId.code != 200) {
						
						// failed
						loadingPanel.SetActive(false);
						alertPanel.SetActive(true);
						errorText.text = "Failed to get your data, please try again!";
						return;

					} else {
						
						// success
						PlayerPrefs.SetString("user__email_address", emailV);
						PlayerPrefs.SetString("user__id", itemUserId.result.id);
						PlayerPrefs.SetString("app__time_record", "False");
                		PlayerPrefs.SetString("device__id", "");

						loadingPanel.SetActive(false);
						SceneManager.LoadScene(2);
						
					}
				});
			} else {
				loadingPanel.SetActive(false);
				alertPanel.SetActive(true);
				errorText.text = "Incorrect email address or password!";
			}

		}).Catch(err => {
			loadingPanel.SetActive(false);
			alertPanel.SetActive(true);
			errorText.text = "Failed to authenticate your account, check your internet connection and please try again!";

			this.LogMessage("Error", err.Message);
		});
    }
}
