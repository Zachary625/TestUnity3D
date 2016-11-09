using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;


namespace Assets.src.GUI.BigListView
{
	public class BigListView : MonoBehaviour, IDragHandler {
		public enum Direction
		{
			None,
			Vertical,
			Horizontal,
		}

		public Direction direction;

		public GameObject itemContainerPrefab;

		public int items {
			get { 
				return this._items;
			}
		}

		public IBigListViewDelegate bigListViewDelegate {
			get {
				return this._bigListViewDelegate;
			}
			set { 
				this._bigListViewDelegate = value;
				this.UpdateItems ();
			}
		}

		private GameObject _contentPanel;
		private List<GameObject> _itemContainerCache = new List<GameObject>();

		private int _items = 0;

		private int _itemCacheSize = 2;
		private int _minItemIndex = 0;
		private int _maxItemIndex = 0;

		private IBigListViewDelegate _bigListViewDelegate = null;

		private float _normalizedPosition {
			get { 
				float result = 0;
				ScrollRect scrollRect = this.GetComponent<ScrollRect> ();

				switch (this.direction) {
				case Direction.Horizontal:
					{
						result = scrollRect.horizontalNormalizedPosition;
						break;
					}
				case Direction.Vertical:
					{
						result = scrollRect.verticalNormalizedPosition;
						break;
					}
				}
				return result;
			}
			set { 
				ScrollRect scrollRect = this.GetComponent<ScrollRect> ();

				switch (this.direction) {
				case Direction.Horizontal:
					{
						scrollRect.horizontalNormalizedPosition = value;
						break;
					}
				case Direction.Vertical:
					{
						scrollRect.verticalNormalizedPosition = value;
						break;
					}
				}
			}
		}

		private Vector2 _contentSize = Vector2.zero;
		private List<Vector2> _itemSizeList = new List<Vector2> ();
		private List<Vector2> _itemPositionList = new List<Vector2> ();

		public Vector2 contentSize {
			get {
				return this._contentSize;
			}
		}

		public Vector2 GetItemSize(int itemIndex) {
			return this._itemSizeList [itemIndex];
		}

		public Vector2 GetItemPosition(int itemIndex) {
			return this._itemPositionList [itemIndex];
		}

		void Awake() {
			this._contentPanel = this.transform.Find("Viewport/Content").gameObject;
		}


		// Use this for initialization
		void Start () {
			ScrollRect scrollRect = this.GetComponent<ScrollRect> ();
			scrollRect.horizontal = (this.direction == Direction.Horizontal);
			scrollRect.vertical = (this.direction == Direction.Vertical);
		}

		// Update is called once per frame
		void Update () {
		}

		private GameObject _allocItemContainer() {
			GameObject itemContainerGameObject;

			//			if (this._pageContainerCache.transform.childCount > 0) {
			//				pageContainerGameObject = this._pageContainerCache.transform.GetChild (0).gameObject;
			//				pageContainerGameObject.SetActive (true);
			if(this._itemContainerCache.Count > 0) {
				itemContainerGameObject = this._itemContainerCache[0];				
				this._itemContainerCache.RemoveAt (0);
				itemContainerGameObject.SetActive (true);
			} else {
				itemContainerGameObject = Instantiate (this.itemContainerPrefab);
			}
			itemContainerGameObject.transform.SetParent (this._contentPanel.transform);
			return itemContainerGameObject;
		}

		private void _freeItemContainer(GameObject itemContainerGameObject) {
			this._itemContainerCache.Add(itemContainerGameObject);
			itemContainerGameObject.SetActive (false);
		}

		private void _updateItems() {
			// TODO
			//			Debug.Log(" @ BigPageView._updatePages(): " + this._pageIndex);
			bool[] cacheStatus = new bool[2 * this._itemCacheSize + (this._maxItemIndex - this._minItemIndex)];
			for (int childIndex = 0; childIndex < this._contentPanel.transform.childCount; childIndex++) {
				GameObject itemContainerGameObject = this._contentPanel.transform.GetChild (childIndex).gameObject;
				BigListViewItemContainer itemContainer = itemContainerGameObject.GetComponent<BigListViewItemContainer> ();
				if (itemContainer.itemIndex < this._minItemIndex - this._itemCacheSize || itemContainer.itemIndex > this._maxItemIndex + this._itemCacheSize) {
					this._freeItemContainer (itemContainerGameObject);
					//					Debug.Log (" @ BigPageview._free(" + pageContainer.pageIndex + ")");
				} else {
					cacheStatus [itemContainer.itemIndex - (this._minItemIndex - this._itemCacheSize)] = true;
				}
			}

			for(int itemIndex = this._minItemIndex - this._itemCacheSize; itemIndex <= this._maxItemIndex + this._itemCacheSize; itemIndex++) {
				if (itemIndex >= 0 && itemIndex <= this.items - 1 && !cacheStatus [itemIndex - (this._minItemIndex - this._itemCacheSize)]) {
					GameObject itemContainerGameObject = this._allocItemContainer ();
					BigListViewItemContainer itemContainer = itemContainerGameObject.GetComponent<BigListViewItemContainer> ();
					itemContainer.itemIndex = itemIndex;
					this.bigListViewDelegate.GetItem (itemContainerGameObject, itemIndex);
				}
			}


		}

//		private float _pageIndexToNormalizedPosition(int pageIndex) {
//			if (this.pages <= 1) {
//				return 0;
//			}
//			return (float)(pageIndex * 1.0 / (this.pages - 1));
//		}
//
//		private int _normalizedPositionToPageIndex(float position) {
//			if (this.pages <= 1) {
//				return 0;
//			}
//			return Mathf.Clamp( Mathf.RoundToInt(this._normalizedPosition * (this.pages - 1)), 0, this.pages - 1);
//		}

//
//		public void jumpToItem(int pageIndex) {
//			if (this.pages < 1) {
//				return;
//			}
//
//			if (this._dragging) {
//				return;
//			}
//
//			this._endScroll ();
//
//			pageIndex = Mathf.Clamp (pageIndex, 0, this.pages - 1);
//
//			int prevPageIndex = this._pageIndex;
//			this._pageIndex = pageIndex;
//			this._updatePages ();
//			this._normalizedPosition = this._pageIndexToNormalizedPosition (pageIndex);
//
//
//			if (prevPageIndex != this._pageIndex) {
//				if (this.pageIndexChangeHandler != null) {
//					this.pageIndexChangeHandler (this.gameObject, new BigPageViewEventArgs (prevPageIndex, this._pageIndex));
//				}
//			}
//		}

		public void OnDrag(PointerEventData data) {
			this._updateItems();
		}

		public void UpdateItems() {
			if (this.bigListViewDelegate == null) {
				for (int childIndex = 0; childIndex < this._contentPanel.transform.childCount; childIndex++) {
					GameObject pageContainerGameObject = this._contentPanel.transform.GetChild (childIndex).gameObject;
					this._freeItemContainer (pageContainerGameObject);
				}

				return;
			}

			this._items = this.bigListViewDelegate.GetItems ();
			this._minItemIndex = 0;
			this._maxItemIndex = 0;

			this._updateItems();
		}

		private void _updateContentPanelRect() {
			RectTransform bigPageViewRectTransform = this.GetComponent<RectTransform>();
			float listViewWidth = bigPageViewRectTransform.rect.width;
			float listViewHeight = bigPageViewRectTransform.rect.height;
			RectTransform contentRectTransform = this._contentPanel.GetComponent<RectTransform> ();

			float contentWidth = listViewWidth;
			float contentHeight = listViewHeight;

			switch (this.direction) {
			case Direction.Horizontal: {
					contentWidth = 0;
					if (this.bigListViewDelegate != null) {
						for (int itemIndex = 0; itemIndex < this.items; itemIndex++) {
							this._itemPositionList.Add (new Vector2(contentWidth, 0));
							this._itemSizeList.Add (this.bigListViewDelegate.GetItemSize(itemIndex));
							contentWidth += this._itemSizeList[itemIndex].x;
						}
					}
					break;
				}					
			case Direction.Vertical: {
					contentHeight = 0;
					if (this.bigListViewDelegate != null) {
						for (int itemIndex = 0; itemIndex < this.items; itemIndex++) {
							this._itemPositionList.Add (new Vector2(0, -contentHeight));
							this._itemSizeList.Add (this.bigListViewDelegate.GetItemSize(itemIndex));
							contentHeight += this._itemSizeList[itemIndex].y;
						}
					}
					break;
				}					
			}

			contentRectTransform.anchorMin = new Vector2(0, 1);
			contentRectTransform.anchorMax = new Vector2(0, 1);

			contentRectTransform.offsetMin = new Vector2(0, -contentHeight);
			contentRectTransform.offsetMax = new Vector2 (contentWidth, 0);
		}
	}

	public interface IBigListViewDelegate {
		int GetItems();

		void GetItem(GameObject itemContainer, int itemIndex);

		Vector2 GetItemSize(int itemIndex);
	}
}

