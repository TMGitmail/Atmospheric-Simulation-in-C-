using LegkorSzimulacio;

namespace LegkorSzimulacio.Test
{
    [TestClass]
    public class TestRéteg
    {
        [TestMethod]
        public void TestÓzonRétegLétrehozás()
        {
            Réteg r = new Réteg(5.0, new Ózon());
            Assert.IsInstanceOfType(r.Anyag, typeof(Ózon));
            Assert.AreEqual("Ózon", r.Anyag.Név);
            Assert.AreEqual(5.0, r.Vastagság);
        }

        [TestMethod]
        public void TestOxigénRétegLétrehozás()
        {
            Réteg r = new Réteg(3.0, new Oxigén());
            Assert.IsInstanceOfType(r.Anyag, typeof(Oxigén));
            Assert.AreEqual("Oxigén", r.Anyag.Név);
            Assert.AreEqual(3.0, r.Vastagság);
        }

        [TestMethod]
        public void TestSzéndioxidRétegLétrehozás()
        {
            Réteg r = new Réteg(4.0, new Széndioxid());
            Assert.IsInstanceOfType(r.Anyag, typeof(Széndioxid));
            Assert.AreEqual("Széndioxid", r.Anyag.Név);
            Assert.AreEqual(4.0, r.Vastagság);
        }

        [TestMethod]
        public void TestCsökkent()
        {
            Réteg r = new Réteg(10.0, new Oxigén());
            r.Csökkent(3.0);
            Assert.AreEqual(7.0, r.Vastagság);
        }

        [TestMethod]
        public void TestEgyesítAzonosAnyag()
        {
            Réteg r1 = new Réteg(5.0, new Oxigén());
            Réteg r2 = new Réteg(3.0, new Oxigén());
            r1.Egyesít(r2);
            Assert.AreEqual(8.0, r1.Vastagság);
        }

        [TestMethod]
        public void TestEgyesítKülönbözőAnyag()
        {
            Réteg r1 = new Réteg(5.0, new Oxigén());
            Réteg r2 = new Réteg(3.0, new Ózon());
            Assert.ThrowsException<InvalidOperationException>(() => r1.Egyesít(r2));
        }

        [TestMethod]
        public void TestAnyagAzonosAzonosTípusraIgaz()
        {
            Anyag a1 = new Oxigén();
            Anyag a2 = new Oxigén();
            Assert.IsTrue(a1.Azonos(a2));
        }

        [TestMethod]
        public void TestAnyagAzonosKülönbözőTípusraHamis()
        {
            Anyag a1 = new Oxigén();
            Anyag a2 = new Ózon();
            Assert.IsFalse(a1.Azonos(a2));
        }

        [TestMethod]
        public void TestRétegAcceptDelegálAzAnyagnak()
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
        public void TestAnyagNévHelyes()
        {
            Assert.AreEqual("Ózon", new Ózon().Név);
            Assert.AreEqual("Oxigén", new Oxigén().Név);
            Assert.AreEqual("Széndioxid", new Széndioxid().Név);
        }
    }
}
