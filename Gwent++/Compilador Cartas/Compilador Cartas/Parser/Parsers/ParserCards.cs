using Parser;
using Parser.Language;
using Parser.Tokenstools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Parsers
{
    public class ParserCards : BaseParser, IParser<Node, IToken>
    {
        Dictionary<string, (VarType type, object value)> latumzambarqueber;

        public ParserCards()
        {
            latumzambarqueber = new Dictionary<string, (VarType type, object value)>();
        }

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
                EffectDef def = Effect(tokens, ref index);

                if (tokens[++index].Type != TokenType.CloseKey)
                {
                    throw new Exception();
                }
                definitions.AddEffectDef(def.Name, def);
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

        #region Definisión de efectos
        public EffectDef Effect(IToken[] tokens, ref int index)
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

            var action = ActionDef(tokens, ref index, @params);

            return new EffectDef(name, @params, action);
        }

        public string Name(IToken[] tokens, ref int index)
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

            if (tokens[++index].Type != TokenType.String)
            {
                throw new Exception();
            }

            string name = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            return name;
        }

        public Params[] ParamsDef(IToken[] tokens, ref int index)
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

        public Params ParamDef(IToken[] tokens, ref int index)
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

        public VarType DeclarationVarType(IToken[] tokens, ref int index)
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

        public Action<IEnumerable<IContextCard>, IContext, InputParams[]>? ActionDef(IToken[] tokens, ref int index, Params[] @params)
        {
            if (tokens[++index].Lex != "Action")
            {
                throw new Exception("");
            }

            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception("");
            }

            if (tokens[++index].Type != TokenType.OpenParenthesis)
            {
                throw new Exception("");
            }
            if (tokens[++index].Lex.ToLower() != "targets")
            {
                throw new Exception("");
            }
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception("");
            }
            if (tokens[++index].Lex.ToLower() != "context")
            {
                throw new Exception("");
            }
            if (tokens[++index].Type != TokenType.CloseParenthesis)
            {
                throw new Exception("");
            }
            if (tokens[++index].Type != TokenType.Lambda)
            {
                throw new Exception("");
            }

            latumzambarqueber = new Dictionary<string, (VarType type, object value)>();
            BlockExp block = BlockExpDef(tokens, ref index);

            Action<IEnumerable<IContextCard>, IContext, InputParams[]>? action = (cards, contex, inputParams) =>
            {
                // TODO: implementar Revision de parametros!!!!
                block.Execute(new BlockContext(cards, contex, latumzambarqueber));
            };
            return action;
        }
        #endregion

        #region Expresiones de lineas
        public BlockExp BlockExpDef(IToken[] tokens, ref int index)
        {
            List<LineExp> lines = new List<LineExp>();
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

                LineExp line = LineExps(tokens, ref index);
                lines.Add(line);
            } while (tokens[index + 1].Type != TokenType.EOF);

            if (tokens[index].Type != TokenType.CloseKey)
            {
                throw new Exception();
            }

            return new BlockExp(lines);
        }

        public LineExp LineExps(IToken[] tokens, ref int index)
        {
            IToken token = tokens[++index];
            if (token.Lex == "if")
            {
                return IF(tokens, ref index);
            }
            else if (token.Lex == "while")
            {
                return WhileExp(tokens, ref index);
            }

            index--;
            DeclarationVarExp declarationVar = DeVar(tokens, ref index);

            if (tokens[++index].Type != TokenType.Semicolon)
            {
                throw new Exception();
            }
            return declarationVar;
        }

        public DeclarationVarExp DeVar(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.Identifier)
            {
                throw new Exception("");
            }

            string namevar = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Equal)
            {
                throw new Exception("");
            }

            int lastIndex = index;

            try
            {
                IOperationExp<bool> valueOp = BoolExp(tokens, ref index);
                return new DeclarationVarExp(namevar, new ValueExp(valueOp));
            }
            catch (Exception)
            {
                index = lastIndex;
            }

            try
            {
                IOperationExp<double> valueOp = AritmExp(tokens, ref index);
                return new DeclarationVarExp(namevar, new ValueExp(valueOp));
            }
            catch (Exception)
            {
                index = lastIndex;
            }

            try
            {
                IOperationExp<string> valueOp = StringExp(tokens, ref index);
                return new DeclarationVarExp(namevar, new ValueExp(valueOp));
            }
            catch (Exception) { }

            throw new Exception();
        }

        public Conditional IF(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.OpenParenthesis)
            {
                throw new Exception();
            }

            IOperationExp<bool> boolean = BoolExp(tokens, ref index);

            if (tokens[++index].Type != TokenType.CloseParenthesis)
            {
                throw new Exception();
            }

            BlockExp ifTrue = BlockExpDef(tokens, ref index);
            BlockExp? ifFalse = default;

            var lastIndex = index;

            if (tokens[++index].Lex == "else")
            {
                ifFalse = BlockExpDef(tokens, ref index);
            }
            else
            {
                index = lastIndex;
            }

            return new Conditional(boolean, ifTrue, ifFalse);
        }

        public WhileExp WhileExp(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.OpenParenthesis)
            {
                throw new Exception();
            }

            IOperationExp<bool> boolean = BoolExp(tokens, ref index);

            if (tokens[++index].Type != TokenType.CloseParenthesis)
            {
                throw new Exception();
            }

            BlockExp block = BlockExpDef(tokens, ref index);
            return new WhileExp(boolean, block);
        }
        #endregion

        #region Expresiones Booleanas
        public IOperationExp<bool> BoolExp(IToken[] tokens, ref int index)
        {
            return LoopExp(tokens, ref index);
        }

        //Verifica si hay && o || 
        public IOperationExp<bool> LoopExp(IToken[] tokens, ref int index)
        {
            IOperationExp<bool> left = SimpleBoolExp(tokens, ref index);
            int lastIndex = index;
            IToken tokenlog = tokens[++index];
            IOperationExp<bool> rigth;
            try
            {
                rigth = LoopExp(tokens, ref index);
            }
            catch (Exception)
            {
                index = lastIndex;
                return left;
            }

            switch (tokenlog.Type)
            {
                case TokenType.And:
                    return new AndExp(left, rigth);
                case TokenType.Or:
                    return new OrExp(left, rigth);
                default:
                    throw new Exception();
            }
        }

        //Para comparadores 
        public IOperationExp<bool> ComparerExp(IToken[] tokens, ref int index)
        {
            IOperationExp<double> left = AritmExp(tokens, ref index);
            IToken token = tokens[++index];
            IOperationExp<double> right = AritmExp(tokens, ref index);
            switch (token.Type)
            {
                case TokenType.EqualEqual:
                    return new EqualsExp(left, right);
                case TokenType.NotEqual:
                    return new DistExp(left, right);
                case TokenType.GreaterThan:
                    return new GreaterExp(left, right);
                case TokenType.LessThan:
                    return new LessExp(left, right);
                case TokenType.GreaterThanEqual:
                    return new GreaterEqualExp(left, right);
                case TokenType.LessThanEqual:
                    return new LessEqualExp(left, right);
                default:
                    throw new Exception();
            }
        }

        public IOperationExp<bool> SimpleBoolExp(IToken[] tokens, ref int index)
        {
            IToken token = tokens[++index];

            switch (token.Type)
            {
                case TokenType.True:
                case TokenType.False:
                    index--;
                    return Bool(tokens, ref index);

                case TokenType.Not:
                    return new NotExp(BoolExp(tokens, ref index));

                case TokenType.OpenParenthesis:
                    IOperationExp<bool> boolExp = BoolExp(tokens, ref index);

                    if (tokens[++index].Type != TokenType.CloseParenthesis) throw new Exception();
                    return boolExp;

                case TokenType.Identifier:
                    

                //case TokenType.Identifier para despues

                default:
                    index--;
                    return ComparerExp(tokens, ref index);
            }
        }

        public IOperationExp<bool> Bool(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.True && tokens[index].Type != TokenType.False)
            {
                throw new Exception();
            }

            bool result;
            if (!bool.TryParse(tokens[index].Lex, out result))
            {
                throw new Exception();
            }
            return new Bool(result);
        }
        #endregion

        #region Expresiones Aritméticas
        public IOperationExp<double> AritmExp(IToken[] tokens, ref int index) // Por ahora
        {
            return PlusMinus(tokens, ref index);
        }

        public IOperationExp<double> PlusMinus(IToken[] tokens, ref int index)
        {
            IOperationExp<double> left = MultDiv(tokens, ref index);
            int lastIndex = index;
            IToken token = tokens[++index];
            IOperationExp<double> rigth;

            try
            {
                rigth = PlusMinus(tokens, ref index);
            }
            catch (Exception)
            {
                index = lastIndex;
                return left;
            }

            switch (token.Type)
            {
                case TokenType.Plus:
                    return new PlusExp(left, rigth);

                case TokenType.Minus:
                    return new MinusExp(left, rigth);

                default:
                    index = lastIndex;
                    return left;
            }
        }

        public IOperationExp<double> MultDiv(IToken[] tokens, ref int index)
        {
            IOperationExp<double> left = AritmSimpleExp(tokens, ref index);
            int lastIndex = index;
            IToken token = tokens[++index];
            IOperationExp<double> rigth;

            try
            {
                rigth = MultDiv(tokens, ref index);
            }
            catch (Exception)
            {
                index = lastIndex;
                return left;
            }

            switch (token.Type)
            {
                case TokenType.Multiply:
                    return new MultiplyExp(left, rigth);

                case TokenType.Divide:
                    return new DivideExp(left, rigth);

                default:
                    index = lastIndex;
                    return left;
            }
        }

        public IOperationExp<double> AritmSimpleExp(IToken[] tokens, ref int index)
        {
            IToken token = tokens[++index];

            switch (token.Type)
            {
                case TokenType.Number:
                    double result;
                    if (!double.TryParse(tokens[index].Lex, out result))
                    {
                        throw new Exception();
                    }
                    return new Number(result);

                case TokenType.OpenParenthesis:
                    IOperationExp<double> aritmExp = AritmExp(tokens, ref index);
                    
                    if (tokens[++index].Type != TokenType.CloseParenthesis)
                        throw new Exception();

                    return aritmExp;
                
                default:
                    throw new Exception();
            }
        }
        #endregion

        #region Expresiones de Cadenas
        public IOperationExp<string> StringExp(IToken[] tokens, ref int index)
        {
            return StringConcatExp(tokens, ref index);
        }

        public IOperationExp<string> StringConcatExp(IToken[] tokens, ref int index)
        {
            IOperationExp<string> left = StringSimple(tokens, ref index);
            IToken token = tokens[++index];
            int lastIndex = index;
            IOperationExp<string> rigth;
            try
            {
                rigth = StringConcatExp(tokens, ref index);
            }
            catch (Exception)
            {
                index = lastIndex;
                return left;
            }

            switch (token.Type)
            {
                case TokenType.Concat:
                    return new ConcatExp(left, rigth);

                case TokenType.ConcatEsp:
                    return new ConcatExp2(left, rigth);

                default:
                    throw new Exception();
            }
        }

        public IOperationExp<string> StringSimple(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            if (tokens[++index].Type != TokenType.String)
            {
                throw new Exception();
            }

            string result = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            return new StringValue(result);
        }
        #endregion

        #region Definisión de Cartas
        public Card Card(IToken[] tokens, ref int index)
        {

            string type = TypeCard(tokens, ref index);
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            string name = NameCard(tokens, ref index);
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            string faccion = Faccion(tokens, ref index);
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            int power = Power(tokens, ref index);
            if (tokens[++index].Type != TokenType.Comma)
            {
                throw new Exception();
            }

            CardClassification[] cardClassifications = Classifications(tokens, ref index);
            // OnActivation();

            Card card = new Card(name, type, faccion, power, cardClassifications, null);
            return card;
        }

        public void OnActivation(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "OnAction")
            {
                throw new Exception("");
            }
            if (tokens[++index].Type != TokenType.Colon)
            {
                throw new Exception();
            }
            if (tokens[++index].Type != TokenType.OpenBracket)
            {
                throw new Exception();
            }
        }

        public string TypeCard(IToken[] tokens, ref int index)
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

            if (tokens[++index].Type != TokenType.String)
            {
                throw new Exception();
            }

            string type = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            return type;
        }

        public string NameCard(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "Name")
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

            if (tokens[++index].Type != TokenType.String)
            {
                throw new Exception();
            }

            string name = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            return name;
        }

        public string Faccion(IToken[] tokens, ref int index)
        {
            if (tokens[++index].Lex != "Faction")
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

            if (tokens[++index].Type != TokenType.String)
            {
                throw new Exception();
            }

            string faccion = tokens[index].Lex;

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            return faccion;
        }

        public int Power(IToken[] tokens, ref int index)
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

            return power;
        }

        public CardClassification[] Classifications(IToken[] tokens, ref int index)
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
                CardClassification classification = Classification(tokens, ref index);
                classifications.Add(classification);
            } while (tokens[++index].Type == TokenType.Comma && tokens[index + 1].Type != TokenType.EOF);

            if (tokens[index].Type != TokenType.CloseBracket)
            {
                throw new Exception();
            }

            return classifications.ToArray();
        }

        public CardClassification Classification(IToken[] tokens, ref int index)
        {
            CardClassification classification;
            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }

            switch (tokens[++index].Lex)
            {
                case "Melee":
                    classification = CardClassification.Melee;
                    break;
                case "Ranged":
                    classification = CardClassification.LongRange;
                    break;
                case "Siege":
                    classification = CardClassification.Siege;
                    break;
                default:
                    throw new Exception();
            }

            if (tokens[++index].Type != TokenType.Marks)
            {
                throw new Exception();
            }
            return classification;
        }
        #endregion
    }
}
