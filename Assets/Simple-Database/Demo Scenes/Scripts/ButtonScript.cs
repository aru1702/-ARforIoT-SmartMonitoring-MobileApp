using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public Text text;
	public Text text2;

	public void DeleteSavefile() {
		SaveFileManager.instance.DeleteSavefile(text.text);
		Debug.Log("DeleteSaveFile");
		Debug.Log(text.text);
	}

	public void Create() {
		SaveFileManager.instance.Create(text.text);
		Debug.Log("Create");
		Debug.Log(text.text);
	}

	public void Refresh() {
		SaveFileManager.instance.RefreshSaveFileList();
		Debug.Log("Refresh");
	}

	public void Open() {
		SaveFileManager.instance.Open(text.text);
		Debug.Log("Open");
		Debug.Log(text.text);
	}

	public void Add() {
		SaveFileManager.instance.Add(text.text, text2.text);
		Debug.Log("Add");
		Debug.Log(text.text);
		Debug.Log(text2.text);
	}

	public void Close() {
		SaveFileManager.instance.Close();
		Debug.Log("Close");
	}

	public void DeleteKey() {
		SaveFileManager.instance.DeleteKey(text.text);
		Debug.Log("DeleteKey");
		Debug.Log(text.text);
	}
}
