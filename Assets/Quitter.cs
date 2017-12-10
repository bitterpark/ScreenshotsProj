using UnityEngine;
using System.Collections;

public class Quitter : MonoBehaviour
{

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}


}
