using Compiler.Parsers;
using Parser;
using Parser.Language;
using Parser.Tokenstools;

string[] MySplit(string text, string separadores)
{
    List<string> TextSplit = new List<string>();

    int start = 0;
    int count = 0;
    do
    {

        char actualChar = text[count];

        if (!separadores.Contains(actualChar))
        {
            // Es un separador
            count++;
            continue;
        }

        // No es un separador
        string element = text.Substring(start, count - start);

        if (element.Length > 0)
        {
            TextSplit.Add(element);
        }
        if (actualChar != ' ' && actualChar != '\r' && actualChar != '\n')
        {
            TextSplit.Add(actualChar.ToString());
        }

        count++;
        start = count;
    } while (text.Length > count);
    return TextSplit.ToArray();
}

TokenType GetReservedValue(string[] words, Dictionary<string, TokenType> valuePairs, ref int i, out string word)
{
    word = "";
    string actualWord = words[i];
    IEnumerable<string>? collisions = null;
    IEnumerable<string> actualCollisions = valuePairs.Keys.Where(x => x.StartsWith(actualWord));

    while (actualCollisions.Count() > 0 && words.Length > i)
    {
        collisions = actualCollisions;
        word = actualWord;

        i++;
        if (words.Length <= i)
        {
            break;
        }
        actualWord = word + words[i];
        actualCollisions = valuePairs.Keys.Where(x => x.StartsWith(actualWord));
    }

    TokenType type = TokenType.Identifier;
    if (collisions != null)
    {
        i = word.Length - 1 == i ? i : i - 1;
        type = valuePairs[word];
    }
    return type;
}

IToken[] Lexer(string[] words, Dictionary<string, TokenType> valuePairs)
{
    List<IToken> tokens = new List<IToken>();
    for (int i = 0; i < words.Length; i++)
    {
        string word;
        TokenType type = GetReservedValue(words, valuePairs, ref i, out word);
        if (type == TokenType.Identifier)
        {
            word = words[i];
            if (int.TryParse(words[i], out _))
            {
                type = TokenType.Number;
            }
        }
        tokens.Add(new Token(word, type));
    }

    tokens.Add(new Token("$", TokenType.EOF));
    return tokens.ToArray();
}

string url = "../../../../File.txt";
string text = File.ReadAllText(url);

string separadores = ",;:. {[()]}/*-+=<>&|@\r\n\"";

string[] words = MySplit(text, separadores);

IToken[] tokens = Lexer(words, Storage.Tokens);

foreach (var item in tokens)
{
    Console.WriteLine(item);
}

ParserCards parserCards = new ParserCards();
Node node = parserCards.Parse(tokens);

Console.ReadKey();