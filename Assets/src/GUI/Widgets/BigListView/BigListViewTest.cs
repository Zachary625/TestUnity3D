using UnityEngine;
using System.Collections;

namespace Assets.src.GUI.BigListView
{

	public class BigListViewTest : MonoBehaviour, IBigListViewDelegate {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public int GetItems() {
			return 0;			
		}

		public void GetItem(GameObject itemContainer, int itemIndex) {
			
		}

		public Vector2 GetItemSize(int itemIndex) {
			return new Vector2 (300, 100);
		}
	}
}

