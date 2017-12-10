using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotDisplayer : MonoBehaviour {

	[SerializeField]
	GameObject screenshotGroup;
	[SerializeField]
	RawImage rawImage;
	[SerializeField]
	Button takeScreenshotButton;
	[SerializeField]
	Button hideScreenshotButton;

	void Awake () {
		takeScreenshotButton.onClick.AddListener(HandleScreenshotButtonPress);
		hideScreenshotButton.onClick.AddListener(HideScreenshot);
	}
	
	void HandleScreenshotButtonPress() {
		TakeScreenshot(null);
	}

	void TakeScreenshot(UnityEngine.Camera.CameraCallback callback) 
	{
		string filePath = MakeScreenshot();
		StartCoroutine(WaitForScreenshotSave(filePath));
	}

	string MakeScreenshot() {
		
		int index = 1;
		string fileName = "screenshot" + index + ".png";
		while (File.Exists(string.Format("{0}{1}{2}", Application.persistentDataPath, Path.DirectorySeparatorChar, fileName))) {
			index++;
			fileName = "screenshot" + index + ".png";
		}
		string filePath = string.Format("{0}{1}{2}", Application.persistentDataPath, Path.DirectorySeparatorChar, fileName, index);
		string captureScreenshotPath = fileName;
		#if UNITY_EDITOR
			captureScreenshotPath = filePath;
		#endif
		ScreenCapture.CaptureScreenshot(captureScreenshotPath);
		return filePath;
	}

	IEnumerator WaitForScreenshotSave(string filePath) {
		takeScreenshotButton.gameObject.SetActive(false);

		while (!File.Exists(filePath)) {
			yield return null;
		}
		takeScreenshotButton.gameObject.SetActive(true);

		byte[] bytes = File.ReadAllBytes(filePath);
		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		texture.LoadImage(bytes);

		ShowScreenshot(texture);
		yield break;
	}


	void ShowScreenshot(Texture2D tex) {
		screenshotGroup.gameObject.SetActive(true);
		rawImage.texture = tex;
	}

	void HideScreenshot() {
		screenshotGroup.SetActive(false);
	}
}
