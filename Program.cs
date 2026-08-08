using System.IO;
using LegkorSzimulacio;

Console.Write("Kérlek add meg a bemeneti fájl nevét!: ");
string? fajlnev = Console.ReadLine();

if (string.IsNullOrWhiteSpace(fajlnev))
{
    Console.WriteLine("Nem adtál meg fájlnevet.");
    return;
}

Légkör légkör = new(fajlnev);

Console.WriteLine("A megadott fájl tartalma:");
foreach (string line in File.ReadLines(fajlnev))
{
    Console.WriteLine(line);
}

Console.WriteLine("---------------------------------------------------");
Console.WriteLine("A bevitt adatok alapján a légkör szimulációja: \n");

légkör.Szimulál();
