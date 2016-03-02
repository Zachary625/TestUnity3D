using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class UIEventHandler : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void onCreateButtonClicked(GameObject srcObj) {
		GameObject scrollViewObj = GameObject.Find ("Scroll View");
		GameObject contentObj = scrollViewObj.transform.Find("Viewport").transform.Find("Content").gameObject;

		GameObject panelObj = new GameObject ();
		Image image = panelObj.AddComponent<Image> ();
		image.color = Random.ColorHSV();
		LayoutElement layoutElement = panelObj.AddComponent<LayoutElement> ();
		layoutElement.preferredWidth = Random.Range(300, 600);
		layoutElement.preferredHeight = Random.Range (100, 200);

		Debug.Log (" preferred height: " + layoutElement.preferredHeight);

		panelObj.transform.SetParent (contentObj.transform);
		return;

		GameObject inputFieldObj = GameObject.Find ("InputField");
		InputField inputField = inputFieldObj.GetComponent<InputField>();
		GameObject textObj = new GameObject ();
		Text text = textObj.AddComponent<Text> ();
		text.text = inputField.text;
		text.font = inputFieldObj.transform.Find("Text").GetComponent<Text>().font;
		textObj.AddComponent<ScrollViewContentController.ListViewItemBehaviour>();

		text.transform.SetParent (contentObj.transform);
	}

	public void onClearButtonClicked(GameObject srcObj) {
		Debug.Log (" @ UIEventHandler.onCreateButtonClicked()");
		GameObject scrollViewObj = GameObject.Find ("Scroll View");
		GameObject contentObj = scrollViewObj.transform.Find("Viewport").transform.Find("Content").gameObject;

		contentObj.transform.DetachChildren ();
	}
}
