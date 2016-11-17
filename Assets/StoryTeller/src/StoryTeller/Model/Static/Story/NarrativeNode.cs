using System;
using System.Collections.Generic;

namespace Assets.src.StoryTeller.Model.Static.Story
{
	public class NarrativeNode: BaseStoryNode
	{
		public int NextNodeIndex;
		public List<Paragraph> Paragraphs = new List<Paragraph>();

		public class Paragraph
		{
			public long TimeMS;
			public int CharacterIndex;
			public string Text;
			public string Title;
		}

	}
}

