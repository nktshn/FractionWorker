using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractionWorkerLibrary.MainClasses;

namespace FractionWorkerLibraryTest {

    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void Adding1() {
            Fraction firstFraction = new Fraction(3, 5);
            Fraction secondFraction = new Fraction(4, 7);
            Fraction result = firstFraction + secondFraction;
            Fraction expect = new Fraction(1, 6, 35);
            Assert.AreEqual(expect.ToString(), result.ToString());
        }
        [TestMethod]
        public void Adding2()
        {
            Fraction firstFraction = new Fraction(3, 1);
            Fraction secondFraction = new Fraction(1, 3);
            Fraction result = firstFraction + secondFraction;
            Fraction expect = new Fraction(3, 1, 3);
            Assert.AreEqual(expect.ToString(), result.ToString());
        }
    }

}