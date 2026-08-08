using System;

namespace LegkorSzimulacio;

public class Réteg
{
    public Anyag Anyag { get; }
    public double Vastagság { get; private set; }

    public Réteg(double vastagság, Anyag anyag)
    {
        Vastagság = vastagság;
        Anyag = anyag;
    }

    public Réteg? Accept(IdőjárásVisitor visitor)
    {
        return Anyag.Accept(visitor, this);
    }

    public void Csökkent(double m)
    {
        Vastagság -= m;
    }

    public void Egyesít(Réteg r)
    {
        if (!Anyag.Azonos(r.Anyag))
        {
            throw new InvalidOperationException("Csak azonos anyagú rétegek egyesíthetők.");
        }

        Vastagság += r.Vastagság;
    }
}
