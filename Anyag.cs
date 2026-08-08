namespace LegkorSzimulacio;

public abstract class Anyag
{
    public abstract string Név { get; }

    public abstract Réteg? Accept(IdőjárásVisitor visitor, Réteg réteg);

    public bool Azonos(Anyag másik)
    {
        return GetType() == másik.GetType();
    }
}
