using System.Collections.Generic;

namespace FPSgame.Scripts.Base.Character.Attributes;

public class AttributeFloat
{
    //TODO: make modifiers die with their creators

    protected readonly float BaseValue;

    private float _currentValue;
    public float CurrentValue
    {
        get
        {
            if (IsDirty)
                RecalculateCurrentValue();
            return _currentValue;
        }
    }

    protected bool IsDirty = true;

    protected readonly List<AttributeModifier> Modifiers = new ();

    public AttributeFloat(float baseValue)
    {
        BaseValue = baseValue;
        _currentValue = BaseValue;
    }

    public virtual void AddModifier(AttributeModifier modifier)
    {
        Modifiers.Add(modifier);
        IsDirty = true;
    }

    public virtual void RemoveModifier(AttributeModifier modifier)
    {
        Modifiers.Remove(modifier);
        IsDirty = true;
    }

    protected virtual void RecalculateCurrentValue()
    {
        float mulBase = 1;
        float add = 0;
        float mul = 1;
        float append = 0;

        foreach (var modifier in Modifiers)
        {
            switch (modifier.Operation)
            {
                case AttributeModifier.ModifierOperation.Add:
                    add += modifier.Value;
                    break;
                
                case AttributeModifier.ModifierOperation.Multiply:
                    mul *= modifier.Value;
                    break;
                
                case AttributeModifier.ModifierOperation.Append:
                    append += modifier.Value;
                    break;
                
                case AttributeModifier.ModifierOperation.MultiplyBase:
                    mulBase *= modifier.Value;
                    break;
            }
        }

        _currentValue = (BaseValue * mulBase + add) * mul + append;
        IsDirty = false;
    }
}