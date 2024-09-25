using Parser;
using Parser.Language;
using Parser.Tokenstools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Parsers
{
    public class ParserCards : BaseParser, IParser<Node, IToken>
    {
        public override Node Parse(IToken[] tokens)
        {
            return Start(tokens);
        }

        public Node Start(IToken[] tokens)
        {
            var definitions = new Definitions();
            for (int i = 0; tokens[i].Type != TokenType.EOF; i++)
            {
                Def(tokens, definitions, ref i);
            }
            return definitions;
        }

        public void Def(IToken[] tokens, Definitions definitions, ref int index)
        {
            if (tokens[index].Type != TokenType.Identifier)
            {
                throw new Exception();
            }

            if (tokens[index].Lex == "effect")
            {
                if (tokens[++index].Type != TokenType.Colon || tokens[++index].Type != TokenType.OpenKey)
                {
                    throw new Exception();
                }
                var def = Effect(tokens, ref index);
                
                if (tokens[++index].Type != TokenType.CloseKey)
                {
                    throw new Exception();
                }
                definitions.AddEffectDef(def);
            }
            else if (tokens[index].Lex == "card")
            {
                if (tokens[++index].Type != TokenType.Colon || tokens[++index].Type != TokenType.OpenKey)
                {
                    throw new Exception();
                }
                var def = Card(tokens, ref index);
                if (tokens[++index].Type != TokenType.CloseKey)
                {
                    throw new Exception();
                }
                definitions.AddCardDef(def);
            }
            else
            {
                throw new Exception();
            }
        }

        private EffectDef Effect(IToken[] tokens, ref int index)
        {
            string name = Name(tokens, ref index);

            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            Params[] @params = ParamsDef(tokens, ref index);

            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            var action = ActionDef(tokens, ref index);

            return new EffectDef(name, @params, action);
        }

        private string Name(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.Identifier || tokens[index].Lex != "Name")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            string name = "";
            while (tokens[index + 1].Type != TokenType.Marks && tokens[index + 1].Type != TokenType.EOF)
            {
                name += tokens[++index].Lex;
            }

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            return name;
        }

        private Params[] ParamsDef(IToken[] tokens, ref int index)
        {
            List<Params> @params = new List<Params>();
            if (tokens[++index].Lex != "Params")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.OpenKey)
            {
                throw new Exception();
            }

            do
            {
                if (tokens[index + 1].Type == TokenType.CloseKey || tokens[index + 1].Type == TokenType.EOF)
                {
                    index++;
                    break;
                }

                Params param = ParamDef(tokens, ref index);
                @params.Add(param);
            } while (tokens[++index].Type == TokenType.Comma);

            if (tokens[index].Type != TokenType.CloseKey)
            {
                throw new Exception();
            }

            return @params.ToArray();
        }

        private Params ParamDef(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.Identifier)
            {
                throw new Exception();
            }

            string name = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            VarType type = DeclarationVarType(tokens, ref index);

            return new Params(name, type);
        }

        private VarType DeclarationVarType(IToken[] tokens, ref int index)
        {
            var type = tokens[++index];

            switch (type.Lex)
            {
                case "Number":
                    return VarType.Int;
                case "Bool":
                    return VarType.Bool;
                case "String":
                    return VarType.Str;
                default:
                    throw new Exception();
            }
        }

        private Action<IEnumerable<IContextCard>, IContext>? ActionDef(IToken[] tokens, ref int index)
        {
            return null;
        }

        private Card Card(IToken[] tokens, ref int index)
        {

            string type = TypeCard(tokens, ref index);
            string name = NameCard(tokens, ref index);
            string faccion = Faccion(tokens, ref index);
            int power = Power(tokens, ref index);
            CardClassification[] cardClassifications = Classification(tokens, ref index);
           // OnActivation();

           Card card = new Card(name, type,faccion, power, cardClassifications, null);
           return card;
        }

        private void OnActivation(IToken[] tokens, ref int index)
        {
            if(tokens[++index].Lex != "OnAction")
            {
                throw new Exception("");
            }
            if(tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }
            if(tokens[++index].Type != TokenType.OpenBracket)
            {
                throw new Exception();
            }
        }

        private string TypeCard(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "Type")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            string type = "";
            while (tokens[index + 1].Type != TokenType.Marks && tokens[index + 1].Type != TokenType.EOF)
            {
                type += tokens[++index].Lex;
            }

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }
            return type;
        }

        private string NameCard(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "Name")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }
            if(tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            string name = "";
           do
            {
                name += tokens[++index].Lex;

            } while (tokens[index + 1].Type != TokenType.Marks && tokens[index + 1].Type != TokenType.EOF);

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }
            return name;
        }

        private string Faccion(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "Faction")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            if(tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            string faccion = "";

            do
            {
                faccion +=tokens[++index].Lex;
                faccion += "";

            } while (tokens[index + 1].Type != TokenType.Marks && tokens[index + 1].Type != TokenType.EOF);

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }
            return faccion;
        }

        private int Power(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "Power")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Number)
            {
                throw new Exception();
            }
            int power = int.Parse(tokens[index].Lex);

            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            return power;
        }

        private CardClassification[] Classification(IToken[] tokens, ref int index)
        {
            List<CardClassification> classifications = new List<CardClassification>();
            if (tokens[++index].Lex != "Range")
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }

            if (tokens[++index].Type != TokenType.OpenBracket)
            {
                throw new Exception();
            }

           do
            {
                if (tokens[++index].Type != TokenType.Marks)
                {
                    throw new Exception();
                }

                switch (tokens[++index].Lex)
                {
                    case "Melee":
                        classifications.Add(CardClassification.Melee);
                        break;

                    case "Ranged":
                        classifications.Add(CardClassification.LongRange);
                        break;
                    case "Siege":
                        classifications.Add(CardClassification.Siege);
                        break;
                    default:
                        throw new Exception();
                }

                if(tokens[++index].Type != TokenType.Marks)
                {
                    throw new Exception();
                }
            } while (tokens[++index].Type == TokenType.Comma && tokens[index + 1].Type != TokenType.EOF );

            if(tokens[index].Type != TokenType.CloseBracket)
            {
                throw new Exception();
            }
            if(tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            return classifications.ToArray();
        }


    }
}
