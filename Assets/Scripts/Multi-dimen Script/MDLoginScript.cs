using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDLoginScript : MonoBehaviour
{

    public Text welcomeText, loginText;
    public InputField emailInput, passwordInput;
    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        Debug.Log(welcomeText.text);
        Debug.Log(emailInput.text);
        Debug.Log(emailInput.placeholder);
        Debug.Log(passwordInput.text);
        Debug.Log(passwordInput.placeholder);
        Debug.Log(loginText.text);
    }
}
