namespace LegkorSzimulacio;

public class Oxigén : Anyag
{
    public override string Név => "Oxigén";

    public override Réteg? Accept(IdőjárásVisitor visitor, Réteg réteg)
    {
        return visitor.Visit(this, réteg);
    }
}
