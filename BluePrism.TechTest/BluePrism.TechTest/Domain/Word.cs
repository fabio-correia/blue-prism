namespace BluePrism.TechTest.Domain
{
    public class Word
    {
        public Word(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            if (obj is string sWord)
                return sWord == Value;

            if (obj is Word word)
                return Value == word.Value;

            return false;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
