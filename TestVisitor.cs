using LegkorSzimulacio;

namespace LegkorSzimulacio.Test
{
    [TestClass]
    public class TestVisitor
    {
        [TestMethod]
        public void TestNaposÓzonNemVáltozik()
        {
            Réteg r = new Réteg(10.0, new Ózon());
            NaposVisitor v = new NaposVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNull(új);
            Assert.AreEqual(10.0, r.Vastagság);
        }

        [TestMethod]
        public void TestNaposOxigénÓzonLesz()
        {
            Réteg r = new Réteg(10.0, new Oxigén());
            NaposVisitor v = new NaposVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNotNull(új);
            Assert.IsInstanceOfType(új.Anyag, typeof(Ózon));
            Assert.AreEqual(0.5, új.Vastagság, 0.0001);
            Assert.AreEqual(9.5, r.Vastagság, 0.0001);
        }

        [TestMethod]
        public void TestNaposSzéndioxidOxigénLesz()
        {
            Réteg r = new Réteg(10.0, new Széndioxid());
            NaposVisitor v = new NaposVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNotNull(új);
            Assert.IsInstanceOfType(új.Anyag, typeof(Oxigén));
            Assert.AreEqual(0.5, új.Vastagság, 0.0001);
            Assert.AreEqual(9.5, r.Vastagság, 0.0001);
        }

        [TestMethod]
        public void TestZivatarosÓzonNemVáltozik()
        {
            Réteg r = new Réteg(10.0, new Ózon());
            ZivatarosVisitor v = new ZivatarosVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNull(új);
            Assert.AreEqual(10.0, r.Vastagság);
        }

        [TestMethod]
        public void TestZivatarosOxigénÓzonLesz()
        {
            Réteg r = new Réteg(10.0, new Oxigén());
            ZivatarosVisitor v = new ZivatarosVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNotNull(új);
            Assert.IsInstanceOfType(új.Anyag, typeof(Ózon));
            Assert.AreEqual(5.0, új.Vastagság, 0.0001);
            Assert.AreEqual(5.0, r.Vastagság, 0.0001);
        }

        [TestMethod]
        public void TestZivatarosSzéndioxidNemVáltozik()
        {
            Réteg r = new Réteg(10.0, new Széndioxid());
            ZivatarosVisitor v = new ZivatarosVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNull(új);
            Assert.AreEqual(10.0, r.Vastagság);
        }

        [TestMethod]
        public void TestMásÓzonOxigénLesz()
        {
            Réteg r = new Réteg(10.0, new Ózon());
            MásVisitor v = new MásVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNotNull(új);
            Assert.IsInstanceOfType(új.Anyag, typeof(Oxigén));
            Assert.AreEqual(0.5, új.Vastagság, 0.0001);
            Assert.AreEqual(9.5, r.Vastagság, 0.0001);
        }

        [TestMethod]
        public void TestMásOxigénSzéndioxidLesz()
        {
            Réteg r = new Réteg(10.0, new Oxigén());
            MásVisitor v = new MásVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNotNull(új);
            Assert.IsInstanceOfType(új.Anyag, typeof(Széndioxid));
            Assert.AreEqual(1.5, új.Vastagság, 0.0001);
            Assert.AreEqual(8.5, r.Vastagság, 0.0001);
        }

        [TestMethod]
        public void TestMásSzéndioxidNemVáltozik()
        {
            Réteg r = new Réteg(10.0, new Széndioxid());
            MásVisitor v = new MásVisitor();
            Réteg? új = r.Accept(v);
            Assert.IsNull(új);
            Assert.AreEqual(10.0, r.Vastagság);
        }
    }
}
