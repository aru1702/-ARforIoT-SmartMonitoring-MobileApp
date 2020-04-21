using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        bool checkuser = checkUser();
        
        if (checkuser == true) {
            SceneManager.LoadScene(2);
        } else {
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool checkUser () {
        string a = PlayerPrefs.GetString("user__id");
        if (a.Length > 0) {
            return true;
        } else {
            return false;
        }
    }
}
