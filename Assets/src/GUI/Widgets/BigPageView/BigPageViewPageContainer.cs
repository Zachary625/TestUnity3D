using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.BigPageView
{
	public class BigPageViewPageContainer : MonoBehaviour, ILayoutElement {

		public GameObject pageView;
		public int pageIndex;

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
			if (pageView.GetComponent<BigPageView> () != null) {
//				this._width = this.transform.parent.GetComponent<RectTransform> ().rect.size.x;
				this._width = this.pageView.GetComponent<RectTransform> ().rect.size.x;
			} else {
				this._width = 0;
			}
		}

		public void CalculateLayoutInputVertical() {
			if (pageView.GetComponent<BigPageView> () != null) {
//				this._height = this.transform.parent.GetComponent<RectTransform>().rect.size.y;
				this._height = this.pageView.GetComponent<RectTransform> ().rect.size.y;
			} else {
				this._height = 0;
			}
		}
	}
}

