
namespace Parser.Language;

public abstract class CompareExp : BooleanExp, IDoubleOperationExp<bool, double>
{

    public CompareExp(IOperationExp<double> left, IOperationExp<double> rigth)
    {
        Left = left;
        Rigth = rigth;
    }

    public IOperationExp<double> Left { get; protected set; }
    public IOperationExp<double> Rigth { get; protected set; }
}

public class EqualsExp : CompareExp
{
    public EqualsExp(IOperationExp<double> left, IOperationExp<double> rigth) : base(left, rigth) { }

    public override bool Execute(IBlockContext blockContext)
    {
        return Left.Execute(blockContext) == Rigth.Execute(blockContext);
    }
}

public class DistExp : CompareExp
{
    public DistExp(IOperationExp<double> left, IOperationExp<double> rigth) : base(left, rigth) { }

    public override bool Execute(IBlockContext blockContext)
    {
        return Left.Execute(blockContext) != Rigth.Execute(blockContext);
    }
}

public class LessExp : CompareExp
{
    public LessExp(IOperationExp<double> left, IOperationExp<double> rigth) : base(left, rigth) { }

    public override bool Execute(IBlockContext blockContext)
    {
        return Left.Execute(blockContext) < Rigth.Execute(blockContext);
    }
}

public class LessEqualExp : CompareExp
{
    public LessEqualExp(IOperationExp<double> left, IOperationExp<double> rigth) : base(left, rigth) { }

    public override bool Execute(IBlockContext blockContext)
    {
        return Left.Execute(blockContext) <= Rigth.Execute(blockContext);
    }
}

public class GreaterExp : CompareExp
{
    public GreaterExp(IOperationExp<double> left, IOperationExp<double> rigth) : base(left, rigth) { }

    public override bool Execute(IBlockContext blockContext)
    {
        return Left.Execute(blockContext) > Rigth.Execute(blockContext);
    }
}

public class GreaterEqualExp : CompareExp
{
    public GreaterEqualExp(IOperationExp<double> left, IOperationExp<double> rigth) : base(left, rigth) { }

    public override bool Execute(IBlockContext blockContext)
    {
        return Left.Execute(blockContext) >= Rigth.Execute(blockContext);
    }
}
