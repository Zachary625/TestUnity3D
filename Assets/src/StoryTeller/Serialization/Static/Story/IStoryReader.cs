using System;

namespace AssemblyCSharp.StoryTeller.Serialization.Static.Story
{
	public interface IStoryReader
	{
		Model.Story readStory(string path);
	}
}

