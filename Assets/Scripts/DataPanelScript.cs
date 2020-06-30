using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skytanet.SimpleDatabase;
using System;
using System.Globalization;
using System.IO;

public class DataPanelScript : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    public static DataPanelScript instance;
	public SaveFile saveFile;
    private readonly string DB_NAME = "myDb";
    private readonly string LAST_OPERATOR = "last";

    public RectTransform content;
    public Button backBtn, resetAllBtn, saveDataBtn, refreshBtn;
    public GameObject dataPanel, mainMenuPanel, alertPanel;
    public Text alertText;

    private void Start() {        
        
        backBtn.onClick.AddListener(backBtnClick);
        resetAllBtn.onClick.AddListener(resetAllBtnClick);
        saveDataBtn.onClick.AddListener(saveDataBtnClick);
        refreshBtn.onClick.AddListener(refreshBtnClick);

        DateTime localDate = DateTime.Now;
        Debug.Log(localDate.ToString("yyyy-MM-dd HH-mm-ss"));
    }

    void backBtnClick () {
        mainMenuPanel.SetActive(true);
        dataPanel.SetActive(false);
        saveFile.Close();
    }

    void resetAllBtnClick () {
        List<string> keys = saveFile.GetKeys();
        for (int i = 0 ; i < keys.Count ; i++) {
            saveFile.Delete(keys[i]);
        }

        saveFile.Set(LAST_OPERATOR, "0");
        RefreshData();
    }

    void saveDataBtnClick () {
        // saveFile = new SaveFile(DB_NAME);
        // saveFile.Close();

        // int lastVPlus = GetLastValue() + 1;
        // string lastVStr = lastVPlus.ToString();
        
        // if (lastVPlus < 10)
        //     lastVStr = "0" + lastVStr;

        // Debug.Log("lastVStr: " + lastVStr);
        // AddData(lastVStr, "test");

        WriteText();
    }

    void refreshBtnClick () {
        RefreshData();
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null) instance = this;

        entryContainer = transform.Find("dataPanelContainer");
        Debug.Log(entryContainer);
        entryTemplate = entryContainer.Find("dataTemplate");
        Debug.Log(entryTemplate);

        content = GetComponent<RectTransform>();

        // // entryContainer = GameObject.Find("dataContainer").GetComponent<Transform>();
        // // entryContainer = entryContainer.Find("dataTemplate").GetComponent<Transform>();

        entryTemplate.gameObject.SetActive(false);
        // RefreshData();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        try
        {
            saveFile.Close();
            saveFile = new SaveFile(DB_NAME);  
            RefreshData();
        }
        catch (System.Exception)
        {
            Debug.Log("failed");
            saveFile = new SaveFile(DB_NAME);
            RefreshData();
            throw;
        }
    }

    void RefreshData () {

        int childC = entryContainer.transform.childCount;
        for (int i = 1 ; i < childC ; i++) {
            GameObject.Destroy(entryContainer.transform.GetChild(i).gameObject);
        }

        // saveFile.ReOpen();
        float templateH = 30f;
        int countObj = GetLastValue();        
        List<string> keys = saveFile.GetKeys();
        for (int i = 0 ; i < keys.Count ; i++) {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2 (0, -templateH * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("datakey").GetComponent<Text>().text = keys[i];
            entryTransform.Find("datavalue").GetComponent<Text>().text = saveFile.Get<string>(keys[i]);
        }

        content.sizeDelta = new Vector2(
            content.sizeDelta.x,
            templateH * countObj + templateH
        );

        // saveFile.Close();
    }

    void AddData(string key, string value) {
        // Debug.Log("A");
        // saveFile.ReOpen();
        // Debug.Log("B");
		
        saveFile.Set(key, value);
        // Debug.Log("B1");
        // Debug.Log("2");
        saveFile.Set(LAST_OPERATOR, key);

        // Debug.Log("C");
        RefreshData();
        // Debug.Log("D");

        // saveFile.Close();
	}

    public int GetLastValue () {
        // saveFile = new SaveFile(DB_NAME);
        List<string> keys = saveFile.GetKeys();
        for (int i = 0; i < keys.Count; ++i) {
            if (keys[i] == LAST_OPERATOR) {
                string lastO = saveFile.Get<string>(keys[i]);
                Debug.Log("lastO: " + lastO);
                // saveFile.Close();
                return int.Parse(lastO);
            }
        }

        // saveFile.Close();
        return 0;
    }

    
    // static void WriteString()
    // {
    //     string path = "Assets/Resources/test.txt";

    //     //Write some text to the test.txt file
    //     StreamWriter writer = new StreamWriter(path, true);
    //     writer.WriteLine("Test");
    //     writer.Close();

    //     //Re-import the file to update the reference in the editor
    //     AssetDatabase.ImportAsset(path); 
    //     TextAsset asset = Resources.Load("test");

    //     //Print the text from the file
    //     Debug.Log(asset.text);
    // }

    void WriteText () {
        DateTime localDate = DateTime.Now;
        
        try
        {
            StreamWriter sw = new StreamWriter( Application.persistentDataPath + " " + localDate.ToString("yyyy-MM-dd HH-mm-ss") + ".txt" );

            List<string> keys = saveFile.GetKeys();
            for (int i = 0 ; i < keys.Count ; i++) {
                string myKey = keys[i];
                string myValue = saveFile.Get<string>(keys[i]);
                sw.WriteLine( myValue );
            }

            sw.Close();
            alertText.text = "Saved data into text success!";
        }
        catch (System.Exception)
        {
            alertText.text = "Failed to save data!";
            throw;
        }

        alertPanel.SetActive(true);
    }
}
