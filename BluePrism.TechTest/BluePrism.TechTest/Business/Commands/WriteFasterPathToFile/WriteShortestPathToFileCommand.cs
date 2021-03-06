using MediatR;

namespace BluePrism.TechTest
{
    public class WriteShortestPathToFileCommand : IRequest<Unit>
    {
        public WriteShortestPathToFileCommand(string start, string end)
        {
            Start = start;
            End = end;
        }
        public string Start { get; }
        public string End { get; }
    }
}