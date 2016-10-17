using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.ListView
{
	public class ListViewItemContainer : MonoBehaviour, ILayoutElement {

		public GameObject listView;
		public GameObject content;
		public int itemIndex;

		private float _width;
		private float _height;

		public float minWidth {
			get { 
				return this._width;
			}
		}

		public float flexibleWidth {
			get { 
				return this._width;
			}
		}

		public float preferredWidth {
			get {
				return this._width;
			}
		}

		public float minHeight {
			get { 
				return this._height;
			}
		}

		public float flexibleHeight {
			get { 
				return this._height;
			}
		}

		public float preferredHeight {
			get {
				return this._height;
			}
		}

		public int LayoutPriority;

		public int layoutPriority {
			get {
				return this.LayoutPriority;
			}
		}

		public void CalculateLayoutInputHorizontal() {
			ListView listViewComponent = listView.GetComponent<ListView> ();
			if (listViewComponent != null && listViewComponent.direction == ListView.Direction.Horizontal) {
				this._width = listView.GetComponent<RectTransform> ().rect.size.x;
			} else {
				this._width = 0;
			}
		}

		public void CalculateLayoutInputVertical() {
			ListView listViewComponent = listView.GetComponent<ListView> ();
			if (listViewComponent != null && listViewComponent.direction == ListView.Direction.Vertical) {
				this._height = listView.GetComponent<RectTransform>().rect.size.y;
			} else {
				this._height = 0;
			}
		}
	}
}

