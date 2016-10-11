using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.src.GUI.PageView
{
	public class PageView : MonoBehaviour {
		private enum _Direction
		{
			None,
			Vertical,
			Horizontal,
		}
		private _Direction _direction = _Direction.None;

		public GameObject pagePrefab;

		public float scrollDuration = 0.2f;

		private GameObject _contentPanel;
		private List<GameObject> _pageContents = new List<GameObject>();

		// Use this for initialization
		void Start () {
			this._contentPanel = this.transform.Find("Viewport").Find("Content").gameObject;

			bool hasVertical = this._contentPanel.GetComponent<VerticalLayoutGroup> () != null;
			bool hasHorizontal = this._contentPanel.GetComponent<HorizontalLayoutGroup> () != null;

			if (hasVertical && !hasHorizontal) {
				this._direction = _Direction.Vertical;
			} else if(!hasVertical && hasHorizontal) {
				this._direction = _Direction.Horizontal;
			}
		}

		// Update is called once per frame
		void Update () {

		}

		private bool _isValidPageContent(GameObject content) {
			if (content == null) {
				return false;
			}
			if (content.GetComponent<RectTransform> () == null) {
				return false;
			}
			if (this._pageContents.IndexOf (content) >= 0) {
				return false;
			}
			return true;
		}

		public void addPage(GameObject content) {
			if (!this._isValidPageContent (content)) {
				return;
			}
			this._pageContents.Add (content);
			this._updatePages ();
 		}

		public void addPage(GameObject content, int index) {
			if (!this._isValidPageContent (content)) {
				return;
			}
			this._pageContents.Insert (index, content);
			this._updatePages ();
		}

		public GameObject getPage(int index) {
			try {
				return this._pageContents[index];				
			}
			catch {
				return null;
			}
		}

		public void removePage(GameObject content) {
			this._pageContents.Remove (content);
			this._updatePages ();
		}

		public void removePage(int index) {
			this._pageContents.RemoveAt (index);
			this._updatePages ();
		}

		public void removeAllPages() {
			this._pageContents.Clear ();
			this._updatePages ();
		}

		private void _updatePages() {
			if (this._pageContents.Count < this._contentPanel.transform.childCount) {
				while (true) {
					GameObject page = this._contentPanel.transform.GetChild (this._pageContents.Count).gameObject;
					if (page) {
						this._removePage (page);
					} else {
						break;
					}
				}
			}
			else {
				while (this._pageContents.Count > this._contentPanel.transform.childCount) {
					this._createPage ();
				}
			}

			for (int pageIndex = 0; pageIndex < this._pageContents.Count; pageIndex++) {
				GameObject page = this._contentPanel.transform.GetChild (pageIndex).gameObject;
				PageViewPage pageComponent = page.GetComponent<PageViewPage> ();

				page.transform.DetachChildren ();
				this._pageContents [pageIndex].transform.SetParent (page.transform);

				pageComponent.pageIndex = pageIndex;
				pageComponent.content = this._pageContents[pageIndex];
			}
		}

		private GameObject _createPage() {
			GameObject page = Instantiate (this.pagePrefab);
			PageViewPage pageComponent = page.GetComponent<PageViewPage> ();

			page.transform.SetParent (this._contentPanel.transform);

			pageComponent.pageView = this.gameObject;

			return page;
		}

		private void _removePage(GameObject page) {
			PageViewPage pageComponent = page.GetComponent<PageViewPage> ();

			pageComponent.pageView = null;
			pageComponent.content = null;

			page.transform.SetParent (null);
			page.transform.DetachChildren ();

		}

		public void jumpToPage(int index) {
			
		}

		public void scrollToPage(int index) {
			
		}
	}
}
