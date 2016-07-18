using System;
using System.Collections.Generic;
using AssemblyCSharp.StoryTeller.Utility;

namespace AssemblyCSharp.StoryTeller.Storage.Static.Story.Model
{
	public class Character
	{
        public string Index;
        public string Name;
        public string Description;
        public List<Property> Properties = new List<Property>();
	}
}

