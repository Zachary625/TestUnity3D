﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.src.GUI.PageView
{
	public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler {
		private enum _Direction
		{
			None,
			Vertical,
			Horizontal,
		}
		private _Direction _direction = _Direction.None;

		public GameObject pagePrefab;

		public float scrollDuration = 0.2f;

		public int Pages {
			get { 
				return this._pageContents.Count;
			}
		}

		private GameObject _contentPanel;
		private List<GameObject> _pageContents = new List<GameObject>();

		private bool _dragging = false;
		private bool _scrolling = false;
		private float _beginPosition = 0;
		private float _endPosition = 0;
		private float _scrollTime = 0;
		private float _scrollAcceleration = 0;

		private float _normalizedPosition {
			get { 
				float result = 0;
				ScrollRect scrollRect = this.GetComponent<ScrollRect> ();

				switch (this._direction) {
				case _Direction.Horizontal:
					{
						result = scrollRect.horizontalNormalizedPosition;
						break;
					}
				case _Direction.Vertical:
					{
						result = scrollRect.verticalNormalizedPosition;
						break;
					}
				}
				return result;
			}
			set { 
				ScrollRect scrollRect = this.GetComponent<ScrollRect> ();

				switch (this._direction) {
				case _Direction.Horizontal:
					{
						scrollRect.horizontalNormalizedPosition = value;
						break;
					}
				case _Direction.Vertical:
					{
						scrollRect.verticalNormalizedPosition = value;
						break;
					}
				}
			}
		}


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
			if (!this._dragging && this._scrolling) {
				this._scrollTime += Time.deltaTime;
				if (this._scrollTime >= this.scrollDuration) {
					this._normalizedPosition = this._endPosition;
					this._endScroll ();
				} else {
					this._normalizedPosition = (float)(this._beginPosition + 0.5 * this._scrollAcceleration * this._scrollTime * this._scrollTime);
				}
			}
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

		public GameObject getPage(int pageIndex) {
			try {
				return this._pageContents[pageIndex];				
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

		private float _pageIndexToNormalizedPosition(int pageIndex) {
			return (float)(pageIndex * 1.0 / (this._pageContents.Count - 1));
		}

		private int _normalizedPositionToPageIndex(float position) {
			return Mathf.Clamp( Mathf.RoundToInt(this._normalizedPosition * (this._pageContents.Count - 1)), 0, this._pageContents.Count - 1);
		}

		public void jumpToPage(int pageIndex) {
			if (this._pageContents.Count <= 1) {
				return;
			}

			if (this._dragging) {
				return;
			}

			this._endScroll ();
			this._normalizedPosition = this._pageIndexToNormalizedPosition(pageIndex);
		}

		public void scrollToPage(int pageIndex) {
			if (this._pageContents.Count <= 1) {
				return;
			}

			if (this._dragging) {
				return;
			}

			this._beginScroll (pageIndex);
		}

		public void OnBeginDrag(PointerEventData data) {
			this._dragging = true;
			this._endScroll ();
		}

		public void OnEndDrag(PointerEventData data) {
			this._dragging = false;

			this._beginScroll (this._normalizedPositionToPageIndex(this._normalizedPosition));
		}

		private void _beginScroll(int pageIndex) {
			if (this._pageContents.Count <= 1) {
				return;
			}

			this._endScroll ();

			this._beginPosition = this._normalizedPosition;
			this._endPosition = this._pageIndexToNormalizedPosition (pageIndex);

			this._scrollAcceleration = 2 * (this._endPosition - this._beginPosition) / this.scrollDuration / this.scrollDuration;

			this._scrollTime = 0;
			this._scrolling = true;
		}

		private void _endScroll() {
			if (this._scrolling) {
				this._scrolling = false;
				this._scrollTime = 0;

				this._beginPosition = 0;
				this._endPosition = 0;

				this._scrollAcceleration = 0;
			}
		}

	}
}
