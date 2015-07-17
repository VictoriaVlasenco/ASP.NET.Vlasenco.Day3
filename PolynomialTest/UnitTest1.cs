using System;
using System.Diagnostics;
using ClassLibrary.Task1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PolynomialTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreationPolynomialCoeffs_0_1_2_3()
        {
            var p = new Polynomial(0,1,2,3,0,0);
            Assert.AreEqual(new Polynomial(0,1,2,3), p);
        }

        [TestMethod]
        public void InitializePolynomialByAnotherOne()
        {
            var p = new Polynomial(0, 1, 2, 3);
            var p1 = new Polynomial(p);
            Assert.AreEqual(p1, p);
        }

        [TestMethod]
        public void Polynomial_1_2_3_Plus_AnotherOne_0_10_Expected_1_12_3()
        {
            var p = new Polynomial(1, 2, 3);
            var p1 = new Polynomial(0, 10);
            Polynomial res = p + p1;
            Assert.AreEqual(res, new Polynomial(1, 12, 3));
        }
        [TestMethod]
        public void Polynomial_0_1_0_0_Add_AnotherOne_1_10_2_Expected_1_11_2()
        {
            var p = new Polynomial(0, 1, 0, 0);
            var p1 = new Polynomial(1, 10, 2);
            Polynomial res = p1.Add(p);
            Assert.AreEqual(res, new Polynomial(1, 11, 2));
        }

        [TestMethod]
        public void Polynomial_1_2_3_Minus_AnotherOne_0_10_Expected_1_Minus8_3()
        {
            var p = new Polynomial(1, 2, 3);
            var p1 = new Polynomial(0, 10);
            Polynomial res = p - p1;
            Assert.AreEqual(res, new Polynomial(1, -8, 3));
        }
        [TestMethod]
        public void FromPolynomial_0_1_0_0_Subtract_AnotherOne_1_10_2_Expected_Minus1_Minus9_Minus2()
        {
            var p = new Polynomial(0, 1, 0, 0);
            var p1 = new Polynomial(1, 10, 2);
            Polynomial res = p.Subtract(p1);
            Assert.AreEqual(res, new Polynomial(-1, -9, -2));
        }

        [TestMethod]
        public void Number_5_MultiplyBy_Polynomial_0_1_10_0_Expected_0_5_50()
        {
            var p = new Polynomial(0, 1, 10, 0);
            Polynomial res = 5*p;
            Assert.AreEqual(res, new Polynomial(0, 5, 50));
        }

        [TestMethod]
        public void Polynomial_0_1_10_0_MultiplyBy_5_Expected_0_5_50()
        {
            var p = new Polynomial(0, 1, 10, 0);
            Polynomial res = p*5;
            Assert.AreEqual(res, new Polynomial(0, 5, 50));
        }

        [TestMethod]
        public void CountValue_X_IsEquals5_Polynomial_5_2_Expected_15()
        {
            var p = new Polynomial(5, 2);
            double d = p.CountValue(5);
            Assert.AreEqual(d, 15);
        }

        [TestMethod]
        public void PolynomialEquals()
        {
            var p = new Polynomial(5, 2, 0, 0);
            var p1 = new Polynomial(5, 2);
            Assert.AreEqual(p1.Equals(p), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InitializePolynimialByNullArray_ExpectedException()
        {
            var p = new Polynomial(0, 0, 0);
        }

        [TestMethod]
        public void TryToChangePolynom_ExpectedUnchangedValue()
        {
            var p = new Polynomial(5, 2, 0, 0);
            p.Coefficients[0] = 10;
            Assert.AreEqual(new Polynomial(5 ,2), p);
        }

    }
}
