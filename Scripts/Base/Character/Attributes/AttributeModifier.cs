namespace FPSgame.Scripts.Base.Character.Attributes;

public class AttributeModifier
{
    public float Value { get; }
    public ModifierOperation Operation { get; }

    public AttributeModifier(float value, ModifierOperation operation)
    {
        Value = value;
        Operation = operation;
    }
    
    public enum ModifierOperation
    {
        // out = (_baseValue * MultiplyBase + Add) * Multiply + Append
        
        MultiplyBase,
        Add,
        Multiply,
        Append,
    }
}