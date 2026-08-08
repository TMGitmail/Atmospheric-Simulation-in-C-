namespace LegkorSzimulacio;

public class Ózon : Anyag
{
    public override string Név => "Ózon";

    public override Réteg? Accept(IdőjárásVisitor visitor, Réteg réteg)
    {
        return visitor.Visit(this, réteg);
    }
}
