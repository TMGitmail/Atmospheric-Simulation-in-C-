namespace LegkorSzimulacio;

public class Széndioxid : Anyag
{
    public override string Név => "Széndioxid";

    public override Réteg? Accept(IdőjárásVisitor visitor, Réteg réteg)
    {
        return visitor.Visit(this, réteg);
    }
}
