using System;
using System.Collections.Generic;

namespace AssemblyCSharp.StoryTeller.Serialization.Static.Story.Model
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

