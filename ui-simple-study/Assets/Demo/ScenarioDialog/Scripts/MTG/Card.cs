using System;

namespace MTG
{
    [Serializable]
    public class Card
    {
        public string name { get; set; }
        public string manaCost { get; set; }
        public float cmc { get; set; }
        public string[] colors { get; set; }
        public string[] colorIdentity { get; set; }
        public string type { get; set; }
        public string[] supertypes { get; set; }
        public string[] types { get; set; }
        public string[] subtypes { get; set; }
        public string rarity { get; set; }
        public string set { get; set; }
        public string setName { get; set; }
        public string text { get; set; }
        public string artist { get; set; }
        public string number { get; set; }
        public string power { get; set; }
        public string toughness { get; set; }
        public string layout { get; set; }
        public int multiverseid { get; set; }
        public string imageUrl { get; set; }
        public string[] variations { get; set; }
        public Ruling[] rulings { get; set; }
        public Foreignname[] foreignNames { get; set; }
        public string[] printings { get; set; }
        public string originalText { get; set; }
        public string originalType { get; set; }
        public Legality[] legalities { get; set; }
        public string id { get; set; }
        public string flavor { get; set; }
        public object[] names { get; set; }
    }
}
