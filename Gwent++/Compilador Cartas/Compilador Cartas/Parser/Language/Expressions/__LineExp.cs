
namespace Parser.Language;

public class BlockExp : Exp
{
    private List<LineExp> lines;

    public BlockExp(List<LineExp> lines)
    {
        this.lines = lines;
    }

    public override void Execute(IBlockContext blockContext)
    {
        foreach (LineExp exp in lines)
        {
            exp.Execute(blockContext);
        }
    }
}

public abstract class LineExp : Exp
{
    public override void Execute(IBlockContext blockContext) { }
}

public class Conditional : LineExp
{
    public Conditional(IOperationExp<bool> boolean, BlockExp ifTrue, BlockExp? ifFalse = null)
    {
        booleanExp = boolean;
        this.ifTrue = ifTrue;
        this.ifFalse = ifFalse;
    }

    IOperationExp<bool> booleanExp { get; set; }
    BlockExp ifTrue { get; set; }
    BlockExp? ifFalse { get; set; }

    public override void Execute(IBlockContext blockContext)
    {
        if (booleanExp.Execute(blockContext))
        {
            ifTrue.Execute(blockContext);
        }
        else if (ifFalse != null)
        {
            ifFalse?.Execute(blockContext);
        }
    }
}

public class WhileExp : LineExp
{
    public WhileExp(IOperationExp<bool> booleanExp, BlockExp block)
    {
        this.booleanExp = booleanExp;
        this.block = block;
    }

    IOperationExp<bool> booleanExp { get; set; }
    BlockExp block { get; set; }

    public override void Execute(IBlockContext blockContext)
    {
        while (booleanExp.Execute(blockContext))
        {
            block.Execute(blockContext);
        }
    }
}

public class ForExp : LineExp
{
    IEnumerable<IContextCard> contextCards;
    BlockExp block;
    string name;

    public ForExp(string name, IEnumerable<IContextCard> contextCards, BlockExp block)
    {
        this.name = name;
        this.contextCards = contextCards;
        this.block = block;
    }

    public override void Execute(IBlockContext blockContext)
    {
        foreach (IContextCard contextCard in contextCards)
        {
            blockContext.LocalVar[name] = (VarType.Card, contextCard);
            block.Execute(blockContext);
        }
        blockContext.LocalVar.Remove(name);
    }
}

public class DeclarationVarExp : LineExp
{
    public string Name { get; private set; }
    public IOperationExp<(VarType type, object value)> ValueExp { get; private set; }

    public DeclarationVarExp(string name, IOperationExp<(VarType type, object value)> exp)
    {
        Name = name;
        ValueExp = exp;
    }

    public override void Execute(IBlockContext blockContext)
    {
        (VarType type, object value) value = ValueExp.Execute(blockContext);
        (VarType type, object value) dicValue;
        if (blockContext.LocalVar.TryGetValue(Name, out dicValue) || blockContext.GlobalVar.TryGetValue(Name, out dicValue))
        {
            if (!dicValue.type.Equals(value.type))
            {
                throw new ArgumentException("");
            }
            dicValue.value = value.value;
        }
        else
        {
            blockContext.LocalVar[Name] = value;
        }
    }
}
