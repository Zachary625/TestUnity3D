using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.BigListView
{

	public class BigListViewTest : MonoBehaviour, IBigListViewDelegate {
		public Font font;



		// Use this for initialization
		void Start () {
			BigListView bigListView = this.GetComponent<BigListView> ();

			bigListView.bigListViewDelegate = this;
		}

		// Update is called once per frame
		void Update () {

		}

		public int GetItems() {
			Debug.Log (" @ BigListViewTest.GetItems()");
			return 1000;			
		}

		public void GetItem(GameObject itemContainer, int itemIndex) {
			Debug.Log (" @ BigListViewTest.GetItem(" + itemIndex + ")");
			Transform itemContentTransform = itemContainer.transform.Find ("ItemContent");
			if (!itemContentTransform) {
				GameObject itemContent = new GameObject ();
				itemContent.name = "ItemContent";
				RectTransform contentRT = itemContent.AddComponent<RectTransform> ();
				Text contentText = itemContent.AddComponent<Text> ();

				contentRT.SetParent (itemContainer.transform);

				contentRT.anchorMin = Vector2.zero;
				contentRT.anchorMax = Vector2.one;

				contentRT.offsetMin = Vector2.zero;
				contentRT.offsetMax = Vector2.zero;

				contentText.alignment = TextAnchor.MiddleCenter;
				contentText.fontSize = 16;
				contentText.color = Color.red;
				contentText.text = "ItemIndex: " + itemIndex;
				contentText.font = this.font;

			} else {
				itemContentTransform.GetComponent<Text> ().text = "ItemIndex: " + itemIndex;
			}
		}

		public Vector2 GetItemSize(int itemIndex) {
			return new Vector2 (400, 40);
		}
	}
}

