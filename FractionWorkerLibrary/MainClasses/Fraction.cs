using System;
using System.Collections.Generic;
using System.Linq;

namespace FractionWorkerLibrary.MainClasses {

    public class Fraction {

        //constructor for proper fraction
        public Fraction(int numerator, int denumerator) {
            _numerator = numerator;

            //checking for deviding by zero
            if (denumerator != 0) {
                _denumerator = denumerator;
            } else {
                throw new DivideByZeroException("Denumerator is dividing by zero");
            }
        }
        //constructor for fraction with integer part
        public Fraction(int integerPart, int numerator, int denumerator)
        {
            _numerator = numerator;
            _integerPart = integerPart;
            //checking for deviding by zero
            if (denumerator != 0)
            {
                _denumerator = denumerator;
            }
            else
            {
                throw new DivideByZeroException("Denumerator is dividing by zero");
            }
            //transformToImproperFraction(integerPart, numerator, denumerator);
        }

        private void transformToImproperFraction(int integerPart, int numerator, int denumerator) {
            _numerator = integerPart * denumerator + numerator;
            _integerPart = 0;
        }

        //main fields for fraction object
        private int _numerator;
        private int _denumerator;
        private int _integerPart;

        //overrided operators    
        public static Fraction operator +(Fraction f1, Fraction f2) {
            var currentNumerator = (f1._numerator * f2._denumerator) + (f2._numerator * f1._denumerator);
            var currentDenumerator = f1._denumerator * f2._denumerator;
            if (currentNumerator == 0) return new Fraction(0, currentDenumerator);
            //if numerator !=0 transforming a fraction
            Fraction reductedFraction = reduct(currentNumerator, currentDenumerator);
            reductedFraction = transformFractionWithIntegerPart(reductedFraction);
            return reductedFraction;
        }

        public static Fraction operator -(Fraction f1, Fraction f2) {
            var currentNumerator = (f1._numerator * f2._denumerator) - (f2._numerator * f1._denumerator);
            var currentDenumerator = f1._denumerator * f2._denumerator;
            if (currentNumerator == 0) return new Fraction(0, currentDenumerator);
            //if numerator !=0 transforming a fraction
            Fraction reductedFraction = reduct(currentNumerator, currentDenumerator);
            reductedFraction = transformFractionWithIntegerPart(reductedFraction);
            return reductedFraction;
        }

        public static Fraction operator *(Fraction f1, Fraction f2) {
            var currentNumerator = (f1._numerator * f2._numerator);
            var currentDenumerator = f1._denumerator * f2._denumerator;
            if (currentNumerator == 0) return new Fraction(0, currentDenumerator);
            //if numerator !=0 transforming a fraction
            Fraction reductedFraction = reduct(currentNumerator, currentDenumerator);
            reductedFraction = transformFractionWithIntegerPart(reductedFraction);
            return reductedFraction;
        }
        public static Fraction operator /(Fraction f1, Fraction f2) {
            var temp = f2._numerator;
            f2._numerator = f2._denumerator;
            f2._denumerator = temp;
            return f1 * f2;
        }
        //overrided toString for pretty output to console
        public override string ToString() {
            if (_numerator == 0) {
                return "0";
            }
            if (_numerator == _denumerator) {
                return "1";
            }
            if (_denumerator == 1) {
                return _numerator.ToString();
            }
            if (_integerPart == 0) {
                return ("[" + _numerator + "/" + _denumerator + "]");
            }
            return (_integerPart + "[" + _numerator + "/" + _denumerator + "]");
        }

        //method to reduction
        private static Fraction reduct(int currentNumerator, int currentDenumerator) {
            //alg for reduction
            //list for storing simple parts of numbers
            var numeratorParts = new List<int>();
            var denumeratorParts = new List<int>();
            var divider = 2; // minimal divider always is 2
            //counting numerator
            while (currentNumerator != 1 & currentNumerator != -1) {
                if (currentNumerator % divider == 0) {
                    numeratorParts.Add(divider);
                    currentNumerator /= divider;
                    divider = 2;
                } else {
                    divider++;
                }
            }
            divider = 2; //give initial value
            //counting denumerator
            while (currentDenumerator != 1 & currentDenumerator != -1) {
                if (currentDenumerator % divider == 0) {
                    denumeratorParts.Add(divider);
                    currentDenumerator /= divider;
                    divider = 2;
                } else {
                    divider++;
                }
            }
            //making similar values to 1
            for (var i = 0; i < numeratorParts.Count; i++) {
                for (var j = 0; j < denumeratorParts.Count; j++) {
                    if (numeratorParts[i] != denumeratorParts[j]) continue;
                    numeratorParts[i] = 1;
                    denumeratorParts[j] = 1;
                }
            }
            //shaping numerator
            currentNumerator = numeratorParts.Aggregate(currentNumerator, (current, e) => current * e);
            //shaping denumerator
            currentDenumerator = denumeratorParts.Aggregate(currentDenumerator, (current, e) => current * e);
            //checking for doubled "-"
            if (currentNumerator < 0 && currentDenumerator < 0) {
                return new Fraction(Math.Abs(currentNumerator), Math.Abs(currentDenumerator));
            }
            return new Fraction(currentNumerator, currentDenumerator);
        }

        private static Fraction transformFractionWithIntegerPart(Fraction f) {
            //transforming a fraction
            if (f._numerator <= f._denumerator) return f;
            //if denumerator > numerator finding integer part of fraction
            var integerPart = f._numerator / f._denumerator;
            f._integerPart = Math.Abs(integerPart);
            f._numerator = f._numerator % f._denumerator;
            return f;
        }
    }

}