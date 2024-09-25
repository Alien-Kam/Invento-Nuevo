
using Parser.Language;
using Parser.Tokenstools;

namespace Parser;

public abstract class BaseParser : IParser<Node, IToken>
{
    public abstract Node Parse(IToken[] tokens);
}
