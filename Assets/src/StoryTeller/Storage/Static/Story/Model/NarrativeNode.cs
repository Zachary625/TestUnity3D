using System;
using System.Collections.Generic;

namespace AssemblyCSharp.StoryTeller.Storage.Static.Story.Model
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

