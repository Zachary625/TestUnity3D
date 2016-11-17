using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.src.StoryTeller.Model.Static.Story
{
    public class Character
    {
		public string Index;
		public string Name;
		public string Description;
		public List<Property> Properties = new List<Property>();
    }
}
