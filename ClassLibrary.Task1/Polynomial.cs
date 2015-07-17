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
                if (coefficients != null)
                {
                    var arrayToReturn = new double[coefficients.Length];
                    coefficients.CopyTo(arrayToReturn,0);
                    return arrayToReturn;
                }
                return new double[] { };
            }
        }

        public double this[int i]
        {
            get { return coefficients[i]; }
        }

        public Polynomial(params double[] coefficients)
        {
            CheckParams(ref coefficients);
            this.coefficients = coefficients;
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

        public Polynomial Clone()
        {
            var p = new Polynomial(this);
            return p;
        }

        object ICloneable.Clone()
        {
            var p = new Polynomial(this);
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

        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            return MakeOperation(left, right, (d, d1) => d + d1);
        }

        public Polynomial Add(Polynomial otherPolynom)
        {
            return this + otherPolynom;
        }

        public static Polynomial operator -(Polynomial minuend, Polynomial sub)
        {
            return MakeOperation(minuend, sub, (d, d1) => d - d1);
        }

        public Polynomial Subtract(Polynomial otherPolynom)
        {
            return this - otherPolynom;
        }

        public static Polynomial operator *(double number, Polynomial multPolynom)
        {
            return MakeOperation(number, multPolynom, (d, d1) => d*d1);
        }

        public static Polynomial operator *(Polynomial multPolynom, double number)
        {
            return MakeOperation(number, multPolynom, (d, d1) => d*d1);
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
                res += Coefficients[i]*Math.Pow(x, i);
            }
            return res;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Coefficients.Length; i++)
            {
                sb.Append(Coefficients[i]);
                sb.Append(' ');
            }
            return sb.ToString();
        }

        private static int DefineLen(Polynomial left, Polynomial right)
        {
            return left.Coefficients.Length >= right.Coefficients.Length ? left.Coefficients.Length : right.Coefficients.Length;
        }

        private static Polynomial MakeOperation(Polynomial left, Polynomial right, Func<double, double, double> toDoFunc)
        {
            int maxLen = DefineLen(left, right);
            double[] array = left.Coefficients;
            if (left.Coefficients.Length != maxLen)
                Array.Resize(ref array, maxLen);
            else
                maxLen = right.Coefficients.Length;
            for (int i = 0; i < maxLen; i++)
            {
                array[i] = toDoFunc(array[i], right.Coefficients[i]);
            }
            return new Polynomial(array);
        }

        private static Polynomial MakeOperation(double number, Polynomial polynomial, Func<double, double, double> toDoFunc)
        {
            double[] array = polynomial.Coefficients;
            for (int i = 0; i < polynomial.Coefficients.Length; i++)
            {
                array[i] = toDoFunc(number, polynomial.Coefficients[i]);
            }
            return new Polynomial(array);
        }

        private void CheckParams(ref double[] coeffs)
        {
            if (coeffs == null)
                throw new ArgumentNullException();
            int zeroCount = 0;
            if (coeffs.Length == 0)
                throw new ArgumentException("Too few arguments to initialize");
            for (int i = coeffs.Length - 1; i >= 0; i--)
            {
                if (Math.Abs(coeffs[i]) < TOLERANCE)
                    zeroCount++;
                else
                    break;
            }
            Array.Resize(ref coeffs, coeffs.Length - zeroCount);
            if (coeffs.Length == 0)
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
