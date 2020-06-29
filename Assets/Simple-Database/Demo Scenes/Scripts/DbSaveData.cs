using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skytanet.SimpleDatabase;

public class DbSaveData : MonoBehaviour
{

    public static SaveFileManager instance;
	public SaveFile saveFile;
    
    private readonly string DB_NAME = "myDb";
    private readonly string LAST_OPERATOR = "last";

    private int lastOperator;
    
    void Start()
    {
        // create db
        SaveFile sdb = new SaveFile(DB_NAME);
		sdb.Close();
        saveFile.Set(LAST_OPERATOR, "0");
    }

	public void AddData(string key, string value) {
		saveFile.Set(key, value);

        int count = GetLastValue(DB_NAME);
        saveFile.Set(LAST_OPERATOR, count.ToString());
	}

	public void DeleteData(string key) {
		saveFile.Delete(key);
	}

    public void GetData(string dbname) {
        saveFile = new SaveFile(dbname);

        List<string> keys = saveFile.GetKeys();
        for (int i = 0; i < keys.Count; ++i) {
            Debug.Log("Keys: " + keys[i] + " // " + "String: " + saveFile.Get<string>(keys[i]));
        }
    }

    public int GetLastValue (string dbName) {
        saveFile = new SaveFile(dbName);

        List<string> keys = saveFile.GetKeys();
        for (int i = 0; i < keys.Count; ++i) {
            if (keys[i] == LAST_OPERATOR) {
                string lastO = saveFile.Get<string>(keys[i]);
                return int.Parse(lastO);
            }
        }

        return 0;
    }

    public void DeleteAllData (string dbName) {
        int count = GetLastValue(DB_NAME);

        for (int i = 1 ; i <= count ; i++) {
            DeleteData(i.ToString());
        }

        saveFile.Set(LAST_OPERATOR, "0");
    }

}
