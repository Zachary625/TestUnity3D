using System;

namespace Assets.src.StoryTeller.Utility
{
    public enum PropertyType
    {
        String = 0,
        Int = 1,
        Double = 2,
        Bool = 3,
        TimePointSec = 4,
        TimeSpanSec = 5,
    }

    public class PropertyPair
	{
		private string key;
		private string value;
        private PropertyType type;

        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public PropertyType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public PropertyPair ()
		{
		}
	}
}

