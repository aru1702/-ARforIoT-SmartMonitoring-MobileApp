using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class EmptySceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async private void OnEnable() {
        int goToScene = PlayerPrefs.GetInt("app__scene_number");
        Debug.Log(goToScene);
        await Task.Delay(TimeSpan.FromSeconds(1));
        SceneManager.LoadScene(goToScene);
    }
}
