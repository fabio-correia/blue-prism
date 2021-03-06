using BluePrism.TechTest.Domain;

namespace BluePrism.TechTest
{
    public class WordComparer : IWordDistanceComparer
    {
        public int Compare(Word x, Word y)
        {
            if (!(x is null || y is null))
                if (x.Value.Length == y.Value.Length)
                {

                    var characterX = x.Value.ToCharArray();
                    var characterY = y.Value.ToCharArray();

                    var differentCharactersCount = 0;
                    for (int i = 0; i < characterX.Length; i++)
                    {
                        differentCharactersCount += characterX[i] == characterY[i] ? 0 : 1;
                    }
                    return differentCharactersCount;
                }
            return -1;
        }
    }
}