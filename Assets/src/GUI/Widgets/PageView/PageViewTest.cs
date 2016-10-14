using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.PageView {
	public class PageViewTest : MonoBehaviour {

		// Use this for initialization
		void Start () {
			PageView pageView = this.GetComponent<PageView> ();
			if (pageView) {
				int pages = 10;

				for(int pageIndex = 0; pageIndex < pages; pageIndex++) {
					GameObject content = new GameObject ();
					RectTransform contentRT = content.AddComponent<RectTransform> ();
					Image contentImage = content.AddComponent<Image> ();

					pageView.addPage (content);

					contentRT.anchorMin = Vector2.zero;
					contentRT.anchorMax = Vector2.one;

					contentRT.offsetMin = Vector2.zero;
					contentRT.offsetMax = Vector2.zero;

					contentImage.color = (pageIndex % 2 == 0) ? Color.red : Color.yellow;

				}
			}

			pageView.pageIndexChangeHandler += new PageView.PageIndexChangeHandler (delegate(GameObject sender, PageView.PageViewEventArgs args) {
				Debug.Log(" @ PageViewText.PageIndexChangeHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});

			pageView.pageScrollingHandler += new PageView.PageScrollingHandler (delegate(GameObject sender, PageView.PageViewEventArgs args) {
				Debug.Log(" @ PageViewText.PageScrollingHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});

			pageView.pageScrolledHandler += new PageView.PageScrolledHandler (delegate(GameObject sender, PageView.PageViewEventArgs args) {
				Debug.Log(" @ PageViewText.PageScrolledHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});

		}

		private GameObject _createPageContent() {
			GameObject content = new GameObject ();
			RectTransform contentRT = content.AddComponent<RectTransform> ();
			return content;
		}

		// Update is called once per frame
		void Update () {

		}
	}
}

