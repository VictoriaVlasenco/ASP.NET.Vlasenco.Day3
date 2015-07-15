using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Task1
{
    public class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        const double TOLERANCE = 0.00001;
        private readonly double[] coefficients;
        public double[] Coefficients 
        { 
            get
            {
                if (coefficients != null) return coefficients;
                return new double[] { };
            }
        }

        public Polynomial(params double[] coeffitients)
        {
            CheckParams(coeffitients);
            this.coefficients = coeffitients;
        }

        public Polynomial(Polynomial other)
        {
            coefficients = other.Coefficients;
        }

        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other)) return false;
                return ArrayEquals(other.Coefficients);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Polynomial);
        }

        public override int GetHashCode()
        {
            return (coefficients.GetHashCode());
        }

        public object Clone()
        {
            var p = (Polynomial)(this);
            return p;
        }

        public static bool operator ==(Polynomial leftOperand, Polynomial rightOperand)
        {
            if (ReferenceEquals(null, leftOperand)) 
                return ReferenceEquals(null, rightOperand);
            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(Polynomial leftOperand, Polynomial rightOperand)
        {
            return !(leftOperand == rightOperand);
        }

        public static Polynomial operator +(Polynomial leftPolynom, Polynomial rightPolynom)
        {
            int minLen;
            Polynomial res;
            DefineLen(leftPolynom, rightPolynom, out minLen, out res);
            
            for (int i = 0; i < minLen; i++)
            {
                res.Coefficients[i] = leftPolynom.Coefficients[i] + rightPolynom.Coefficients[i];
            }
            return res;
        }

        public Polynomial Add(Polynomial otherPolynom)
        {
            return this + otherPolynom;
        }

        public static Polynomial operator -(Polynomial minuendPolynom, Polynomial subPolynom)
        {
            int minLen;
            Polynomial res;
            DefineLen(minuendPolynom, subPolynom, out minLen, out res);
            

            for (int i = 0; i < minLen; i++)
            {
                res.Coefficients[i] = minuendPolynom.Coefficients[i] - subPolynom.Coefficients[i];
            }
            return res;
        }

        public Polynomial Subtract(Polynomial otherPolynom)
        {
            return this - otherPolynom;
        }

        public static Polynomial operator *(double number, Polynomial multPolynom)
        {
            var res = (Polynomial)multPolynom.Clone();
            for (int i = 0; i < multPolynom.Coefficients.Length; i++)
            {
                res.Coefficients[i] = number * multPolynom.Coefficients[i];
            }
            return res;
        }

        public Polynomial Multiply(double number)
        {
            return number * this;
        }

        public double CountValue(double x)
        {
            double res = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                res += coefficients[i]*Math.Pow(x, i);
            }
            return res;
        }

        private static void DefineLen(Polynomial leftOperand, Polynomial rightOperand,out int minLen, out Polynomial p)
        {
            if (leftOperand.Coefficients.Length <= rightOperand.Coefficients.Length)
            {
                minLen = leftOperand.Coefficients.Length;
                p = (Polynomial)rightOperand.Clone();
            }
            else
            {
                minLen = rightOperand.Coefficients.Length;
                p = (Polynomial)leftOperand.Clone();
            }
        }

        private void CheckParams(double[] coeffitients)
        {
            if (coeffitients == null)
                throw new ArgumentNullException();
            int zeroCount = 0;
            if (coeffitients.Length == 0)
                throw new ArgumentException("Too few arguments to initialize");
            for (int i = coeffitients.Length - 1; i >= 0; i--)
            {
                if (Math.Abs(coefficients[i]) < TOLERANCE)
                    zeroCount++;
                else
                    break;
            }
            Array.Copy(coeffitients, zeroCount, coeffitients, 0, coefficients.Length - zeroCount);
            if (coeffitients.Length == 0)
                throw new ArgumentException("Too few arguments to initialize");
        }
        private bool ArrayEquals (double[] a2)
        {
            if (coefficients == null || a2 == null)
                return false;

            if (coefficients.Length != a2.Length)
                return false;

            for (int i = 0; i < coefficients.Length; i++)
            {
                if (Math.Abs(coefficients[i] - a2[i]) > TOLERANCE) return false;
            }
            return true;
        }
    }
}
