using LegkorSzimulacio;
using System.IO;

namespace LegkorSzimulacio.Test
{
    [TestClass]
    public class TestLégkör
    {
        private string CreateTempFile(string content)
        {
            string path = Path.GetTempFileName();
            File.WriteAllText(path, content);
            return path;
        }

        private static void AssertRéteg(Réteg réteg, Type elvártAnyagTípus, double elvártVastagság)
        {
            Assert.IsInstanceOfType(réteg.Anyag, elvártAnyagTípus);
            Assert.AreEqual(elvártVastagság, réteg.Vastagság, 0.0001);
        }

        [TestMethod]
        public void TestÜresFájlDobKivételt()
        {
            string path = CreateTempFile("");

            Assert.ThrowsException<InvalidDataException>(() => new Légkör(path));
        }

        [TestMethod]
        public void TestHibásSorDobKivételt()
        {
            string path = CreateTempFile("n\nz 5 extra");

            Assert.ThrowsException<InvalidDataException>(() => new Légkör(path));
        }

        [TestMethod]
        public void TestIsmeretlenAnyagJelDobKivételt()
        {
            string path = CreateTempFile("n\nz 5\nq 3\nx 5\ns 5");

            Assert.ThrowsException<ArgumentException>(() => new Légkör(path));
        }

        [TestMethod]
        public void TestÜresSorHibásSornakSzámít()
        {
            string content = "n\nz 5\n\nx 5\ns 5";
            string path = CreateTempFile(content);

            Assert.ThrowsException<InvalidDataException>(() => new Légkör(path));
        }

        [TestMethod]
        public void TestPontosVastagságBeolvasás()
        {
            string content = "n\nz 5\nx 0.8\ns 3\nx 4";
            string path = CreateTempFile(content);

            Légkör l = new Légkör(path);

            Assert.AreEqual(4, l.Rétegek.Count);
            AssertRéteg(l.Rétegek[0], typeof(Ózon), 5);
            AssertRéteg(l.Rétegek[1], typeof(Oxigén), 0.8);
            AssertRéteg(l.Rétegek[2], typeof(Széndioxid), 3);
            AssertRéteg(l.Rétegek[3], typeof(Oxigén), 4);
        }

        [TestMethod]
        public void TestSzimulálMegáll_HaHiányzikAzOxigén()
        {
            string content = "n\nz 5\ns 5";
            string path = CreateTempFile(content);

            Légkör l = new Légkör(path);
            l.Szimulál();

            Assert.AreEqual(2, l.Rétegek.Count);
            AssertRéteg(l.Rétegek[0], typeof(Ózon), 5);
            AssertRéteg(l.Rétegek[1], typeof(Széndioxid), 5);
        }

        [TestMethod]
        public void TestIdőjárásKörbenForogÉsKonkrétVégeredménytAd()
        {
            string content = "zn\nz 5\nx 10\ns 5";
            string path = CreateTempFile(content);

            Légkör l = new Légkör(path);
            l.Szimulál();

            Assert.AreEqual(3, l.Rétegek.Count);
            AssertRéteg(l.Rétegek[0], typeof(Ózon), 5);
            AssertRéteg(l.Rétegek[1], typeof(Széndioxid), 4.07253125);
            AssertRéteg(l.Rétegek[2], typeof(Ózon), 9.7454671875);
        }

        [TestMethod]
        public void TestMásIdőjárásKarakterVisitorraFordulÉsRendeziARétegeket()
        {
            string content = "m\nz 0.4\nx 10\ns 10";
            string path = CreateTempFile(content);

            Légkör l = new Légkör(path);
            l.Szimulál();

            Assert.AreEqual(2, l.Rétegek.Count);
            AssertRéteg(l.Rétegek[0], typeof(Oxigén), 8.52);
            AssertRéteg(l.Rétegek[1], typeof(Széndioxid), 11.5);
        }

        [TestMethod]
        public void TestIsmeretlenIdőjárásJelDobKivételt()
        {
            string content = "a\nz 5\nx 10\ns 10";
            string path = CreateTempFile(content);

            Légkör l = new Légkör(path);

            Assert.ThrowsException<InvalidDataException>(() => l.Szimulál());
        }

        [TestMethod]
        public void TestÜresIdőjárásSorozatDobKivételtSzimuláláskor()
        {
            string content = "\nz 5\nx 10\ns 10";
            string path = CreateTempFile(content);

            Légkör l = new Légkör(path);

            Assert.ThrowsException<InvalidOperationException>(() => l.Szimulál());
        }
    }
}
