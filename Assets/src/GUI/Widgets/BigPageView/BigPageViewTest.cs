using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.BigPageView {
	public class BigPageViewTest : MonoBehaviour, IBigPageViewDelegate {

		// Use this for initialization
		void Start () {
			BigPageView bigPageView = this.GetComponent<BigPageView> ();
			if (bigPageView) {
				int pages = 10;

				for(int pageIndex = 0; pageIndex < pages; pageIndex++) {
					GameObject content = new GameObject ();
					RectTransform contentRT = content.AddComponent<RectTransform> ();
					Image contentImage = content.AddComponent<Image> ();

					bigPageView.addPage (content);

					contentRT.anchorMin = Vector2.zero;
					contentRT.anchorMax = Vector2.one;

					contentRT.offsetMin = Vector2.zero;
					contentRT.offsetMax = Vector2.zero;

					contentImage.color = (pageIndex % 2 == 0) ? Color.red : Color.yellow;

				}
			}

			bigPageView.pageIndexChangeHandler += new BigPageView.PageIndexChangeHandler (delegate(GameObject sender, BigPageView.BigPageViewEventArgs args) {
				Debug.Log(" @ BigPageViewTest.PageIndexChangeHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});
			bigPageView.pageScrollStartHandler += new BigPageView.PageScrollStartHandler (delegate(GameObject sender, BigPageView.BigPageViewEventArgs args) {
				Debug.Log(" @ BigPageViewTest.PageScrollStartHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});
			bigPageView.pageScrollStopHandler += new BigPageView.PageScrollStopHandler (delegate(GameObject sender, BigPageView.BigPageViewEventArgs args) {
				Debug.Log(" @ BigPageViewTest.PageScrollStopHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
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

		public int getPages() {
			return 0;
		}

		public GameObject createPage(int pageIndex) {
			return null;
		}

		public void removePage(int pageIndex, GameObject page) {
			
		}

	}
}

