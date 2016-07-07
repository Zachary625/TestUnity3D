using System;

namespace AssemblyCSharp.StoryTeller.Serialization.Static.Story
{
	public interface IStoryReader
	{
		StoryTeller.Model.Static.Story.Story readStory(string path);
	}
}

