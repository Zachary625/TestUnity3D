using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.src.StoryTeller.Model.Static.Story
{
    public class Story
    {
		public string Name;
		public string Description;
		public int RootNodeIndex;

		public List<Character> Characters = new List<Character>();

		public List<WaitNode> WaitNodes = new List<WaitNode>();
		public List<SelectiveNode> SelectiveNodes = new List<SelectiveNode>();
		public List<NarrativeNode> NarrativeNodes = new List<NarrativeNode>();
		public List<Property> Properties = new List<Property>();
    }
}
