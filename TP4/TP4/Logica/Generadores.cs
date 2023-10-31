using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Logica
{
    public class Generadores
    {
        public double GeneradorExponencial(double media, double rnd)
        {
            double exp = -media * Math.Log(1 - rnd);
            return exp;
        }

        public double GeneradorUniforme(double a, double b, double rnd)
        {
            double unif = a + rnd * (b - a);
            return unif;
        }

        public double GeneradorNormal(double desviacion, double media, double rnd, double rnd2)
        {
            double normal = (Math.Sqrt(-2 * Math.Log(rnd)) * Math.Cos(2 * Math.PI * rnd2)) * desviacion + media;
            return normal;
        }
    }
}
