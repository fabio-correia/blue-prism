using System;
using System.IO;

namespace BluePrism.TechTest.Settings
{
    public class WordFilesStoredSettings
    {
        public WordFilesStoredSettings(string sourceLocation, string destinyLocation)
        {
            SourceLocation = sourceLocation ?? throw new ArgumentNullException(nameof(sourceLocation));
            DestinyLocation = destinyLocation ?? throw new ArgumentNullException(nameof(destinyLocation));
        }
        public string SourceLocation { get; }
        public string DestinyLocation { get; }
    }
}
