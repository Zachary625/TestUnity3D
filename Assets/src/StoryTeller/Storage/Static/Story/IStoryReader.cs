using System;

namespace Assets.src.StoryTeller.Storage.Static.Story
{
	public interface IStoryReader
	{
		StoryTeller.Model.Static.Story.Story readStory(string path);
	}
}

