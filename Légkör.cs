using System.Globalization;

namespace LegkorSzimulacio;

public class Légkör
{
    private readonly List<Réteg> rétegek = new();
    public IReadOnlyList<Réteg> Rétegek => rétegek;
    private readonly string időjárások;
    private int poz;
    private static Anyag AnyagotLétrehoz(char jel)
    {
        switch (jel)
        {
            case 'z':
                return new Ózon();

            case 'x':
                return new Oxigén();

            case 's':
                return new Széndioxid();

            default:
                throw new ArgumentException("Ismeretlen anyagjel: " + jel);
        }
    }
    
    private static IdőjárásVisitor VisitortLétrehoz(char időjárásJel)
    {
        switch (időjárásJel)
        {
            case 'n':
                return new NaposVisitor();

            case 'z':
                return new ZivatarosVisitor();

            case 'm':
                return new MásVisitor();

            default:
                throw new InvalidDataException($"Ismeretlen időjárásjel: {időjárásJel}");
        }
    }

    public Légkör(string fájlnév)
    {
        string[] sorok = File.ReadAllLines(fájlnév);

        if (sorok.Length == 0)
        {
            throw new InvalidDataException("A bemeneti fájl üres.");
        }

        időjárások = sorok[0];

        for (int i = 1; i < sorok.Length; i++)
        {

            string[] adatok = sorok[i].Split(' ');
            if (adatok.Length != 2)
            {
                throw new InvalidDataException($"Hibás sor: {sorok[i]}");
            }

            char anyagJel = adatok[0][0];
            double vastagság = double.Parse(adatok[1], CultureInfo.InvariantCulture);
            
            Anyag anyag = AnyagotLétrehoz(anyagJel);
            rétegek.Add(new Réteg(vastagság, anyag));
        }
    }

    public void Szimulál()
    {
        while (VanAnyag<Ózon>() && VanAnyag<Oxigén>() && VanAnyag<Széndioxid>())
        {
            SzimulálEgyKört();
        }
    }

    private IdőjárásVisitor KövetkezőVisitor()
    {
        if (string.IsNullOrEmpty(időjárások))
        {
            throw new InvalidOperationException("Nincs megadva időjárássorozat.");
        }

        char c = időjárások[poz];
        poz = (poz + 1) % időjárások.Length;

        return VisitortLétrehoz(c);
    }

    private void SzimulálEgyKört()
    {
        IdőjárásVisitor visitor = KövetkezőVisitor();
        List<(int forrásIndex, Réteg új)> újak = new();

        for (int i = 0; i < rétegek.Count; i++)
        {
            Réteg? új = rétegek[i].Accept(visitor);
            if (új != null)
            {
                újak.Add((i, új));
            }
        }

        foreach (var (forrásIndex, új) in újak)
        {
            int felső = -1;

            for (int j = forrásIndex + 1; j < rétegek.Count; j++)
            {
                if (rétegek[j].Anyag.Azonos(új.Anyag))
                {
                    felső = j;
                    break;
                }
            }

            if (felső != -1)
            {
                rétegek[felső].Egyesít(új);
            }
            else if (új.Vastagság >= 0.5)
            {
                rétegek.Add(új);
            }
        }

        Rendez();
        Kiírás();
    }

    private void Rendez()
    {
        for (int i = 0; i < rétegek.Count; i++)
        {
            if (rétegek[i].Vastagság < 0.5)
            {
                int felső = -1;

                for (int j = i + 1; j < rétegek.Count; j++)
                {
                    if (rétegek[j].Anyag.Azonos(rétegek[i].Anyag))
                    {
                        felső = j;
                        break;
                    }
                }

                if (felső != -1)
                {
                    rétegek[felső].Egyesít(rétegek[i]);
                }

                rétegek.RemoveAt(i);
                i--;
            }
        }
    }

    private bool VanAnyag<TAnyag>() where TAnyag : Anyag
    {
        return rétegek.Any(r => r.Anyag is TAnyag && r.Vastagság > 0);
    }

    private void Kiírás()
    {
        Console.WriteLine("Kör vége – rétegek alulról felfelé:");

        foreach (Réteg r in rétegek)
        {
            Console.WriteLine($"{r.Anyag.Név} réteg: {r.Vastagság:0.###} km");
        }

        Console.WriteLine("------------------------------");
    }
    
}
