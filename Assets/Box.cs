using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{

	private void OnMouseDrag() {
		Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = newPos;
	}
}
