using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alertPanelScript : MonoBehaviour
{

    public Button alertPanelButton;
    public GameObject alertPanel;

    public void Start () {
        alertPanelButton.onClick.AddListener(alertButtonClick);
    }

    public void alertButtonClick () {
        alertPanel.SetActive(false);
    }
    
}
