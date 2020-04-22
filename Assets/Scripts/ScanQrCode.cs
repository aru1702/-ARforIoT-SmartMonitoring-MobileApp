using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using Models;
using Proyecto26;

public class ScanQrCode : MonoBehaviour {

	public Text TextHeader;
	public RawImage Image;
	public Button backButton;
	
	private float RestartTime;
	private IScanner BarcodeScanner;	
	private bool readyToScan;

	// Disable Screen Rotation on that screen
	void Awake()
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
	}

	void GoBack () {
		Image = null;
		BarcodeScanner.Destroy();
		BarcodeScanner = null;

		SceneManager.LoadScene(3);
	}

	void Start () {
		// initialize
		readyToScan = false;
		backButton.onClick.AddListener(GoBack);

		// Create a basic scanner
		BarcodeScanner = new Scanner();
		BarcodeScanner.Camera.Play();

		// Display the camera texture through a RawImage
		BarcodeScanner.OnReady += (sender, arg) => {
			// Set Orientation & Texture
			Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
			Image.transform.localScale = BarcodeScanner.Camera.GetScale();
			Image.texture = BarcodeScanner.Camera.Texture;

			// Keep Image Aspect Ratio
			var rect = Image.GetComponent<RectTransform>();
			var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
			rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);

			RestartTime = Time.realtimeSinceStartup;
			readyToScan = true;
		};
	}

	/// <summary>
	/// Start a scan and wait for the callback (wait 1s after a scan success to avoid scanning multiple time the same element)
	/// </summary>
	private void StartScanner()
	{
		BarcodeScanner.Scan((barCodeType, barCodeValue) => {
			BarcodeScanner.Stop();

			string deviceGetDeviceUrl = "https://myionic-c4817.firebaseapp.com/api/v1/Device/GetDevice/" + barCodeValue;
			TextHeader.text = "Please wait...";
			string userId = PlayerPrefs.GetString("user__id");

			RestClient.Get(deviceGetDeviceUrl).Then(async res => {
				string result = res.Text;
				GetDeviceDetailsModel2 deviceDetail = JsonUtility.FromJson<GetDeviceDetailsModel2>(result);

				if (deviceDetail.code != 200) {
					TextHeader.text = "Device is not found!";
				} else if (deviceDetail.result.id_user != userId) {
					TextHeader.text = "No authorization to access device!";
				} else {
					TextHeader.text = "Receive device authentication\n\nID: " + deviceDetail.result.id + "\nName: " + deviceDetail.result.name;
					PlayerPrefs.SetString("device__id", deviceDetail.result.id);
					await Task.Delay(TimeSpan.FromSeconds(2));
					
					Image = null;
					BarcodeScanner.Destroy();
					BarcodeScanner = null;

					SceneManager.LoadScene(3);
				}

				readyToScan = true;
			});

			// if (TextHeader.text.Length > 250)
			// {
			// 	TextHeader.text = "Text inside QR are too long";
			// 	return;
			// }

			// TextHeader.text += "Found: " + barCodeType + " / " + barCodeValue + "\n";
			RestartTime += Time.realtimeSinceStartup + 1f;

			// #if UNITY_ANDROID || UNITY_IOS
			// Handheld.Vibrate();
			// #endif
		});
	}

	/// <summary>
	/// The Update method from unity need to be propagated
	/// </summary>
	void Update()
	{
		if (BarcodeScanner != null)
		{
			BarcodeScanner.Update();
		}

		// Debug.Log(readyToScan);

		// Check if the Scanner need to be started or restarted
		// if (RestartTime != 0 && RestartTime < Time.realtimeSinceStartup)
		// {
		// 	Debug.Log("true");
		// 	StartScanner();
		// 	RestartTime = 0;
		// }

		if (readyToScan == true) {
			Debug.Log("true");
			StartScanner();
			readyToScan = false;
		}
	}

	// #region UI Buttons

	// public void ClickBack()
	// {
	// 	// Try to stop the camera before loading another scene
	// 	StartCoroutine(StopCamera(() => {
	// 		SceneManager.LoadScene("Boot");
	// 	}));
	// }

	// /// <summary>
	// /// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
	// /// Trying to stop the camera in OnDestroy provoke random crash on Android
	// /// </summary>
	// /// <param name="callback"></param>
	// /// <returns></returns>
	// public IEnumerator StopCamera(Action callback)
	// {
	// 	// Stop Scanning
	// 	Image = null;
	// 	BarcodeScanner.Destroy();
	// 	BarcodeScanner = null;

	// 	// Wait a bit
	// 	yield return new WaitForSeconds(0.1f);

	// 	callback.Invoke();
	// }

	// #endregion
}
