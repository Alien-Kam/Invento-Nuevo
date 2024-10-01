
namespace Parser.Language;

class CallVar<T> : IOperationExp<T>
{

    public string name { get; protected set; }

    public CallVar(string name)
    {
        this.name = name;
    }

    public T Execute(IBlockContext blockContext)
    {
        (VarType type, object value) value;
        bool existvar = blockContext.LocalVar.TryGetValue(name, out value) || blockContext.GlobalVar.TryGetValue(name, out value);
        
        if(!existvar)
        {
            throw new Exception();
        }
        
        switch (value.type)
        {
            case VarType.Int:
             if(typeof(T) != int)
             {
                throw new Exception();
             }
            case VarType.Bool:
             if(typeof(T) != bool)
             {
                throw new Exception();
             }
            case VarType.Str:
             if(typeof(T) != int)
             {
                throw new Exception();
             }
            case VarType.Card:
             if(typeof(T) != int)
             {
                throw new Exception();
             }
            default:
                throw new Exception();
        }
    }
}