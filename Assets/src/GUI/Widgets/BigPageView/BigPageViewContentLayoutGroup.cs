using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.BigPageView {
	public class BigPageViewContentLayoutGroup : MonoBehaviour, ILayoutGroup, ILayoutSelfController {

		public GameObject bigPageViewGameObject;
		
		public void SetLayoutHorizontal() {
			BigPageView bigPageView = bigPageViewGameObject.GetComponent<BigPageView> ();
			RectTransform bigPageViewRectTransform = (bigPageViewGameObject.transform as RectTransform);
			float contentWidth = bigPageViewRectTransform.rect.width;
			float contentHeight = bigPageViewRectTransform.rect.height;

			switch (bigPageView.direction) {
			case BigPageView.Direction.Horizontal: {
					contentWidth *= bigPageView.bigPageViewDelegate.getPages ();

					for (int childIndex = 0; childIndex < this.transform.childCount; childIndex++) {
						RectTransform pageContainerTransform = this.transform.GetChild (childIndex) as RectTransform;
						BigPageViewPageContainer pageContainer = pageContainerTransform.GetComponent<BigPageViewPageContainer> ();

						pageContainerTransform.anchorMin = Vector2.zero;
						pageContainerTransform.anchorMax = Vector2.zero;

						pageContainerTransform.offsetMin = new Vector2(bigPageViewRectTransform.rect.width * pageContainer.pageIndex, 0);
						pageContainerTransform.offsetMax = new Vector2(bigPageViewRectTransform.rect.width * (pageContainer.pageIndex + 1), bigPageViewRectTransform.rect.height);
					}
					break;
				}					
			case BigPageView.Direction.Vertical: {
					contentWidth *= bigPageView.bigPageViewDelegate.getPages ();

					for (int childIndex = 0; childIndex < this.transform.childCount; childIndex++) {
						RectTransform pageContainerTransform = this.transform.GetChild (childIndex) as RectTransform;
						BigPageViewPageContainer pageContainer = pageContainerTransform.GetComponent<BigPageViewPageContainer> ();

						pageContainerTransform.anchorMin = Vector2.zero;
						pageContainerTransform.anchorMax = Vector2.zero;

						pageContainerTransform.offsetMin = new Vector2(0, bigPageViewRectTransform.rect.height * pageContainer.pageIndex);
						pageContainerTransform.offsetMax = new Vector2(bigPageViewRectTransform.rect.width, bigPageViewRectTransform.rect.height * (pageContainer.pageIndex + 1));
					}
					break;
				}					
			}

			bigPageViewRectTransform.anchorMin = Vector2.zero;
			bigPageViewRectTransform.anchorMax = Vector2.zero;

			bigPageViewRectTransform.offsetMin = Vector2.zero;
			bigPageViewRectTransform.offsetMax = new Vector2 (contentWidth, contentHeight);

		}

		public void SetLayoutVertical() {
			this._updateSelfRectTransform ();
		}

		private void _updateSelfRectTransform() {
			BigPageView bigPageView = bigPageViewGameObject.GetComponent<BigPageView> ();
			RectTransform bigPageViewRectTransform = (bigPageViewGameObject.transform as RectTransform);
			float contentWidth = bigPageViewRectTransform.rect.width;
			float contentHeight = bigPageViewRectTransform.rect.height;
			switch (bigPageView.direction) {
			case BigPageView.Direction.Horizontal: {
					contentWidth *= bigPageView.bigPageViewDelegate.getPages ();
					break;
				}					
			case BigPageView.Direction.Vertical:
				{
					contentHeight *= bigPageView.bigPageViewDelegate.getPages ();
					break;
				}
			}
		}
	}
}

