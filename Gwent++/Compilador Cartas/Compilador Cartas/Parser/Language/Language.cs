
using Parser.Language;

namespace Parser.Language;

public class Node { }

public class Definitions : Node, IDefinitions
{
    private List<ICardDef> cards;
    private List<IEffectDef> effectDefs;

    public IEnumerable<ICardDef> Cards => cards;
    public IEnumerable<IEffectDef> EffectDefs => effectDefs;
    public Definitions()
    {
        cards = new List<ICardDef>();
        effectDefs = new List<IEffectDef>();
    }

    public void AddEffectDef(EffectDef def)
    {
        effectDefs.Add(def);
    }

    public void AddCardDef(Card def)
    {
        cards.Add(def);
    }
}

public class EffectDef : Node, IEffectDef
{
    public EffectDef(string name, IParams[] @params, Action<IEnumerable<IContextCard>, IContext>? action)
    {
        Name = name;
        Params = @params;
        Action = action;
    }

    public string Name { get; private set; }

    public IParams[] Params { get; private set; }

    public Action<IEnumerable<IContextCard>, IContext>? Action { get; private set; }
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
