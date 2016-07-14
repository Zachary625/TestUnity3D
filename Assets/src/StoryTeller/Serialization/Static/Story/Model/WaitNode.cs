﻿using System;

namespace AssemblyCSharp.StoryTeller.Serialization.Static.Story.Model
{
	public class WaitNode: BaseStoryNode
	{
        public int NextNodeIndex;
        public int CharacterIndex;
        public long DelayMS;
        public string Text;
	}
}

