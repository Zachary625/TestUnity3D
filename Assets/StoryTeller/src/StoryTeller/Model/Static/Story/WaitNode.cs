using System;

namespace Assets.src.StoryTeller.Model.Static.Story
{
	public class WaitNode: BaseStoryNode
	{
		public int NextNodeIndex;
		public int CharacterIndex;
		public long DelayMS;
		public string Text;
	}
}

