using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.src.GUI.BigPageView {
	public class BigPageViewTest : MonoBehaviour, IBigPageViewDelegate {

		// Use this for initialization
		void Start () {
			BigPageView bigPageView = this.GetComponent<BigPageView> ();

			bigPageView.pageIndexChangeHandler += new BigPageView.PageIndexChangeHandler (delegate(GameObject sender, BigPageView.BigPageViewEventArgs args) {
				Debug.Log(" @ BigPageViewTest.PageIndexChangeHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});
			bigPageView.pageScrollStartHandler += new BigPageView.PageScrollStartHandler (delegate(GameObject sender, BigPageView.BigPageViewEventArgs args) {
				Debug.Log(" @ BigPageViewTest.PageScrollStartHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});
			bigPageView.pageScrollStopHandler += new BigPageView.PageScrollStopHandler (delegate(GameObject sender, BigPageView.BigPageViewEventArgs args) {
				Debug.Log(" @ BigPageViewTest.PageScrollStopHandler(" + args.PrevPageIndex + " -> " + args.NextPageIndex + ")");
			});

			bigPageView.bigPageViewDelegate = this;

		}

		private GameObject _createPageContent() {
			GameObject content = new GameObject ();
			RectTransform contentRT = content.AddComponent<RectTransform> ();
			return content;
		}

		// Update is called once per frame
		void Update () {

		}

		public int GetPages() {
			Debug.Log (" @ BigPageViewTest.getPages()");
			return 10;
		}

		public void GetPage(GameObject pageContainer, int pageIndex) {
			Debug.Log (" @ BigPageViewTest.getPage(" + pageIndex + ")");

		}
	}
}

