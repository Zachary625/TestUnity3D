using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewContentController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTransformChildrenChanged() {
		RectTransform rectTransform = this.GetComponent<RectTransform> ();
		Vector2 sizeDelta = rectTransform.sizeDelta;
		sizeDelta.y = 0;
		foreach (RectTransform rt in rectTransform) {
//			sizeDelta.y += rt.sizeDelta.y;
			sizeDelta.y += rt.GetComponent<LayoutElement>().preferredHeight;
		}
		Debug.Log (" recalc: " + sizeDelta.y);
		rectTransform.sizeDelta = sizeDelta;
	}
	public class ListViewItemBehaviour: UIBehaviour {
		protected override void OnRectTransformDimensionsChange() {
			RectTransform rt = this.GetComponent<RectTransform> ();
//			Debug.Log (transform.gameObject.name + " sizeDelta.y: " + rt.sizeDelta.y);
//			Debug.Log (transform.gameObject.name + " rect.height: " + rt.rect.height);
		}
	}
}
