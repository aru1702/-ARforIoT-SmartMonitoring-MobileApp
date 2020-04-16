using System.Collections;
using System.Collections.Generic;
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

			if (res.code == 200) {
				SceneManager.LoadScene(2);
			}

		}).Catch(err => this.LogMessage("Error", err.Message));
    }
}
