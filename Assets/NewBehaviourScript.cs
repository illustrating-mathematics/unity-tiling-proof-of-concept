using UnityEngine;
using System.Collections;




public class NewBehaviourScript : MonoBehaviour {
	Texture2D texture;
	Vector2 last_uv;

	void reset () {
		int i, j;
		for (i = 1; i < texture.width - 1; i++) {
			for (j = 1; j < texture.height - 1; j++) {
				texture.SetPixel (i, j, Color.white);
			}
		}
		for (i = 0; i < texture.width; i++) {
			texture.SetPixel (i, 0, Color.gray);
			texture.SetPixel (i, texture.height-1, Color.gray);
		}
		for (j = 0; j < texture.width; j++) {
			texture.SetPixel (0, j, Color.gray);
			texture.SetPixel (texture.width-1, j, Color.gray);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log (GetComponent<Renderer> ());
		Debug.Log (GetComponent<Renderer> ().sharedMaterial);
		Debug.Log (GetComponent<Renderer> ().sharedMaterial.mainTexture);

		//texture = (Texture2D) Resources.Load ("icermlogo");
		texture = new Texture2D(256,256);
		Renderer rnd = GetComponent<Renderer> ();
		rnd.material.SetTexture("_MainTex", texture);

		reset ();
	}

	private Color c1 = new Color (0.8f, 0.9f, 1.0f, 0.3f);
	private Color c2 = new Color (4f, 0.6f, 1.0f, 0.5f);
	private Color c4 = new Color (0f, 0.3f, 1.0f, 1.0f);

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("space")) {
			reset ();
		}
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (GetComponent<Collider> ().Raycast (ray, out hit, Mathf.Infinity)) {
				// Find the u,v coordinate of the Texture
				Vector2 uv;
				// center and scale onto [0,1]x[0,1]
				uv.x = (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x;
				uv.y = (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y;
				uv = 3 * uv; 
				uv.x = uv.x % 1.0f;
				uv.y = uv.y % 1.0f;

				// scale back up to pixel coordinate on actual texture
				int uvx = (int) (uv.x*texture.width);
				int uvy = (int) (uv.y*texture.height);
				Debug.Log ("drawing at " + uvx + ", " + uvy);
				// Paint it
				texture.SetPixel (uvx-1, uvy-1, c1);
				texture.SetPixel (uvx-1, uvy+0, c2);
				texture.SetPixel (uvx-1, uvy+1, c1);
				texture.SetPixel (uvx+0, uvy-1, c2);
				texture.SetPixel (uvx+0, uvy+0, c4);
				texture.SetPixel (uvx+0, uvy+1, c2);
				texture.SetPixel (uvx+1, uvy-1, c1);
				texture.SetPixel (uvx+1, uvy+0, c2);
				texture.SetPixel (uvx+1, uvy+1, c1);

				texture.Apply ();
			}
		}
	}
}
