using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.src.GUI.ListView
{
	public class ListViewTest : MonoBehaviour {
		private enum _Direction
		{
			None,
			Vertical,
			Horizontal,
		}

		private _Direction _direction = _Direction.None;

		public Font TextFont;
		public Color TextColor;

		// Use this for initialization
		void Start () {
			string[] fontNames = Font.GetOSInstalledFontNames ();
			foreach (string fontName in fontNames) {
				Debug.Log (" @ ListViewTest: Font.GetOSInstalledFontNames(): " + fontName);
			}

			bool hasVertical = this.GetComponent<VerticalLayoutGroup> () != null;
			bool hasHorizontal = this.GetComponent<HorizontalLayoutGroup> () != null;

			if (hasVertical && !hasHorizontal) {
				this._direction = _Direction.Vertical;
			} else if(!hasVertical && hasHorizontal) {
				this._direction = _Direction.Horizontal;
			}

			foreach (string paragraph in Assets.src.App.AppDebug.TextParagraphs) {
				GameObject textObj = new GameObject ();
				Text textComponent = textObj.AddComponent<Text> ();
				switch (this._direction) {
				case _Direction.Vertical:
					{
						textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
						textComponent.verticalOverflow = VerticalWrapMode.Truncate;

						break;
					}
				case _Direction.Horizontal:
					{
						textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
						textComponent.verticalOverflow = VerticalWrapMode.Truncate;
						break;
					}
				}

				textComponent.color = this.TextColor;
				textComponent.font = this.TextFont;
				textComponent.text = paragraph;
				textComponent.transform.SetParent(this.transform);
			}


		}

		// Update is called once per frame
		void Update () {

		}
	}
}


