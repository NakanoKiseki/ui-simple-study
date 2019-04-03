using System;

namespace MTG
{
    [Serializable]
    public class Foreignname
    {
        public string name { get; set; }
        public string text { get; set; }
        public string flavor { get; set; }
        public string imageUrl { get; set; }
        public string language { get; set; }
        public int multiverseid { get; set; }
    }
}
