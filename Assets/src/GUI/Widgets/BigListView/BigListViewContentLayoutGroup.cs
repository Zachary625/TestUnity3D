using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.src.GUI.BigListView
{
	public class BigListViewContentLayoutGroup : MonoBehaviour, ILayoutSelfController, ILayoutGroup {
		public GameObject bigListViewGameObject;

		public void SetLayoutHorizontal() {
			BigListView bigListView = bigListViewGameObject.GetComponent<BigListView> ();
			RectTransform bigListViewRectTransform = (bigListViewGameObject.transform as RectTransform);
			float contentWidth = bigListViewRectTransform.rect.width;
			float contentHeight = bigListViewRectTransform.rect.height;
			RectTransform contentRectTransform = this.GetComponent<RectTransform> ();

			switch (bigListView.direction) {
			case BigListView.Direction.Horizontal: {
					contentWidth = bigListView.contentSize.x;

					if (contentWidth != contentRectTransform.rect.width || contentHeight != contentRectTransform.rect.height) {
						contentRectTransform.offsetMin = new Vector2(0, -contentHeight);
						contentRectTransform.offsetMax = new Vector2 (contentWidth, 0);
					}

					for (int childIndex = 0; childIndex < this.transform.childCount; childIndex++) {
						RectTransform itemContainerTransform = this.transform.GetChild (childIndex) as RectTransform;
						BigListViewItemContainer itemContainer = itemContainerTransform.GetComponent<BigListViewItemContainer> ();

						itemContainerTransform.anchorMin = new Vector2(0, 1);
						itemContainerTransform.anchorMax = new Vector2(0, 1);

						itemContainerTransform.offsetMin = new Vector2(bigListView.GetItemPosition(itemContainer.itemIndex).x, -contentHeight);
						itemContainerTransform.offsetMax = new Vector2(bigListView.GetItemPosition(itemContainer.itemIndex).x + bigListView.GetItemSize(itemContainer.itemIndex).x, 0);
					}

					break;
				}					
			}

		}

		public void SetLayoutVertical() {
			BigListView bigListView = bigListViewGameObject.GetComponent<BigListView> ();
			RectTransform bigListViewRectTransform = (bigListViewGameObject.transform as RectTransform);
			float contentWidth = bigListViewRectTransform.rect.width;
			float contentHeight = bigListViewRectTransform.rect.height;
			RectTransform contentRectTransform = this.GetComponent<RectTransform> ();

			switch (bigListView.direction) {
			case BigListView.Direction.Vertical: {
					contentHeight = bigListView.contentSize.y;

					if (contentWidth != contentRectTransform.rect.width || contentHeight != contentRectTransform.rect.height) {
						contentRectTransform.offsetMin = new Vector2(0, -contentHeight);
						contentRectTransform.offsetMax = new Vector2 (contentWidth, 0);
					}

					for (int childIndex = 0; childIndex < this.transform.childCount; childIndex++) {
						RectTransform itemContainerTransform = this.transform.GetChild (childIndex) as RectTransform;
						BigListViewItemContainer itemContainer = itemContainerTransform.GetComponent<BigListViewItemContainer> ();

						itemContainerTransform.anchorMin = new Vector2(0, 1);
						itemContainerTransform.anchorMax = new Vector2(0, 1);

						itemContainerTransform.offsetMin = new Vector2(0, bigListView.GetItemPosition(itemContainer.itemIndex).y);
						itemContainerTransform.offsetMax = new Vector2(contentWidth, bigListView.GetItemPosition(itemContainer.itemIndex).y - bigListView.GetItemSize(itemContainer.itemIndex).y);
					}

					break;
				}					
			}

		}
	}
}

