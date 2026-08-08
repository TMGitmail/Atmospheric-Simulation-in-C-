namespace LegkorSzimulacio;

public interface IdőjárásVisitor
{
    Réteg? Visit(Ózon anyag, Réteg réteg);
    Réteg? Visit(Oxigén anyag, Réteg réteg);
    Réteg? Visit(Széndioxid anyag, Réteg réteg);
}

public class NaposVisitor : IdőjárásVisitor
{
    public Réteg? Visit(Ózon anyag, Réteg réteg)
    {
        return null;
    }

    public Réteg? Visit(Oxigén anyag, Réteg réteg)
    {
        double m = réteg.Vastagság * 0.05;
        réteg.Csökkent(m);
        return new Réteg(m, new Ózon());
    }

    public Réteg? Visit(Széndioxid anyag, Réteg réteg)
    {
        double m = réteg.Vastagság * 0.05;
        réteg.Csökkent(m);
        return new Réteg(m, new Oxigén());
    }
}

public class ZivatarosVisitor : IdőjárásVisitor
{
    public Réteg? Visit(Ózon anyag, Réteg réteg)
    {
        return null;
    }

    public Réteg? Visit(Oxigén anyag, Réteg réteg)
    {
        double m = réteg.Vastagság * 0.5;
        réteg.Csökkent(m);
        return new Réteg(m, new Ózon());
    }

    public Réteg? Visit(Széndioxid anyag, Réteg réteg)
    {
        return null;
    }
}

public class MásVisitor : IdőjárásVisitor
{
    public Réteg? Visit(Ózon anyag, Réteg réteg)
    {
        double m = réteg.Vastagság * 0.05;
        réteg.Csökkent(m);
        return new Réteg(m, new Oxigén());
    }

    public Réteg? Visit(Oxigén anyag, Réteg réteg)
    {
        double m = réteg.Vastagság * 0.15;
        réteg.Csökkent(m);
        return new Réteg(m, new Széndioxid());
    }

    public Réteg? Visit(Széndioxid anyag, Réteg réteg)
    {
        return null;
    }
}
