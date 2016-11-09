using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.src.GUI.BigListView
{
	public class BigListViewItemContainer : MonoBehaviour {

		public int itemIndex;

		public int LayoutPriority;

		public int layoutPriority {
			get {
				return this.LayoutPriority;
			}
		}

		public Vector2 itemSize = Vector2.zero;
	}
}


