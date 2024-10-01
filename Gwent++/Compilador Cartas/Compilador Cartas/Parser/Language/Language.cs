
namespace Parser.Language;

public class Node { }

/// <summary>
/// Clase donde se guardan las cartas y los efectos.
/// </summary>
public class Definitions : Node, IDefinitions
{
    private List<ICardDef> cards;
    private Dictionary<string, IEffectDef> effects;

    public IEnumerable<ICardDef> Cards => cards;
    public IEnumerable<IEffectDef> EffectDefs => effects.Values;
    public Definitions()
    {
        cards = new List<ICardDef>();
        effects = new Dictionary<string, IEffectDef>();
    }

    public void AddEffectDef(string name, EffectDef def)
    {
        effects.Add(name, def);
    }

    public void AddCardDef(Card def)
    {
        cards.Add(def);
    }
}
/// <summary>
/// Clase que define un efecto, esta clase tiene las siguientes propiedades:
/// <list type="bullet">
/// <item>
/// <description>name: es el nombre del efecto</description>
/// </item>
/// <item>
/// <description>params: son los par metros que recibe el efecto</description>
/// </item>
/// <item>
/// <description>action: es la accion que se va a ejecutar cuando se llame al efecto</description>
/// </item>
/// </list>
/// </summary>
public class EffectDef : Node, IEffectDef
{
    public EffectDef(string name, IParams[] @params, Action<IEnumerable<IContextCard>, IContext, InputParams[]>? action)
    {
        Name = name;
        Params = @params;
        Action = action;
    }

    /// <summary>
    /// El nombre del efecto
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Los par metros que recibe el efecto
    /// </summary>
    public IParams[] Params { get; private set; }

    /// <summary>
    /// La accion que se va a ejecutar cuando se llame al efecto
    /// </summary>
    public Action<IEnumerable<IContextCard>, IContext, InputParams[]>? Action { get; private set; }
}
public class Params : Node, IParams
{
    public Params(string name, VarType paramsType)
    {
        Name = name;
        ParamsType = paramsType;
    }

    public string Name { get; private set; }

    public VarType ParamsType { get; private set; }
}

/// <summary>
/// Clase que representa los parametros de entrada que se puede definir en una carta,
/// esta clase tiene las siguientes propiedades:
/// <list type="bullet">
/// <item>
/// <description>name: es el nombre del parametro</description>
/// </item>
/// <item>
/// <description>value: es el valor del parametro</description>
/// </item>
/// <item>
/// <description>paramsType: es el tipo de parametro</description>
/// </item>
/// </list>
/// </summary>
public class InputParams : Node
{
    public InputParams(string name, object value, VarType paramsType)
    {
        Name = name;
        Value = value;
        ParamsType = paramsType;
    }

    public string Name { get; set; }

    public object Value { get; set; }

    public VarType ParamsType { get; set; }
}
public class Card : Node, ICard, ICardDef
{
    public Card(string name, string type, string faction, int power, CardClassification[] range, IEnumerable<IOnActivation> onActivations)
    {
        Name = name;
        Type = type;
        Faction = faction;
        Power = power;
        Range = range;
        OnActivations = onActivations;
    }

    public string Name { get; private set; }

    public string Type { get; private set; }

    public string Faction { get; private set; }

    public int Power { get; private set; }

    public CardClassification[] Range { get; private set; }

    public IEnumerable<IOnActivation> OnActivations { get; private set; }
}

public class ActionEffect : Node, IActionEffect
{
    public ActionEffect(ICallEffect effect, ISelector selector)
    {
        Effect = effect;
        Selector = selector;
    }

    public ICallEffect Effect { get; private set; }

    public ISelector Selector { get; private set; }
}

public class CallEffect : Node, ICallEffect
{
    public CallEffect(string name, IParams[] @params)
    {
        Name = name;
        Params = @params;
    }

    public string Name { get; private set; }

    public IParams[] Params { get; private set; }
}

public class Selector : Node, ISelector
{
    public Selector(Source source, bool single, Func<IContextCard, bool> predicate)
    {
        Source = source;
        Single = single;
        Predicate = predicate;
    }

    public Source Source { get; private set; }

    public bool Single { get; private set; }

    public Func<IContextCard, bool> Predicate { get; private set; }
}

public class OnActivation : Node, IOnActivation, IActionEffect
{
    public OnActivation(IActionEffect postAction, ICallEffect effect, ISelector selector)
    {
        PostAction = postAction;
        Effect = effect;
        Selector = selector;
    }

    public IActionEffect PostAction { get; private set; }

    public ICallEffect Effect { get; private set; }

    public ISelector Selector { get; private set; }
}
