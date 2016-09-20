using System;
using System.Collections.Generic;

namespace Assets.src.StoryTeller.Model.Static.Story
{
	public class SelectiveNode: BaseStoryNode
	{
		public List<Option> Options = new List<Option>();
		public int CharacterIndex;
		public string Text;

		public class Option
		{
			public string Title;
			public string Text;
			public int NextNodeIndex;
		}
	}
}

