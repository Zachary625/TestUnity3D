using System;

namespace AssemblyCSharp.StoryTeller.Storage.Static.Story
{
	public interface IStoryReader
	{
		Model.Story readStory(string path);
	}
}

