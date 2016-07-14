using System;
using System.IO;

namespace AssemblyCSharp.StoryTeller.Utility
{
    public enum NodeType
    {
        None = 0,
        Narrative = 1,
        Selective = 2,
        Wait = 3,
    }

    public static class Utility
    {
        public static string getRootPath() {
            return "StoryTeller";
        }

        public static string getStaticPath() {
            return getRootPath() + Path.PathSeparator + "Static";
        }

        public static string getDynamicPath()
        {
            return getRootPath() + Path.PathSeparator + "Dynamic";
        }

        public static string getStaticStoryPath()
        {
            return getStaticPath() + Path.PathSeparator + "Story";
        }

        public static string getDynamicStoryPath()
        {
            return getDynamicPath() + Path.PathSeparator + "Story";
        }

    }

    public static class Constants
	{

	}
}

