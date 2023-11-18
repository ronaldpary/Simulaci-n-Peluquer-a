using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Logica
{
    class Euler
    {
        double h;
        double D;
        double t;
        ITipoEuler tipoEuler;
        Presentacion.EulerForm form;
        double Do;
        double cola;
        double tiempo;

        public Euler(double h, double to, double Do, ITipoEuler tipoEuler, Presentacion.EulerForm form, double cola, double tiempo)
        {
            this.h = h;
            this.t = to;
            this.Do = Do;
            this.tipoEuler = tipoEuler;
            this.form = form;
            this.cola = cola;
            this.tiempo = tiempo;
        }

        public double calcularEuler()
        {
            double[] filaInicial = new double[5] { 0, 0, 0, this.t, Do };
            if (form != null)
                mostrarFila(filaInicial);

            double[] filaAux = calcularFilaEuler(filaInicial);
            if (form != null)
                mostrarFila(filaAux);

            while (tipoEuler.ValidarCondicion(filaAux[1], ValidarParametro(filaInicial), tiempo))
            {
                filaInicial = filaAux;
                filaAux = calcularFilaEuler(filaAux);
                if (form != null)
                    mostrarFila(filaAux);
            }

            return filaAux[0];
        }

        public double ValidarParametro(double[] filaAnterior)
        {
            if (this.tipoEuler.GetType().Name == "FinAtencionAprendiz")
                return filaAnterior[1];
            if (this.tipoEuler.GetType().Name == "FinAtencionVeteranoA" || this.tipoEuler.GetType().Name == "FinAtencionVeteranoB")
                return filaAnterior[1];

            return 0;
        }

        public double[] calcularFilaEuler(double[] fila)
        {
            double[] filaAuxiliar = new double[5];
            filaAuxiliar[0] = fila[3]; //t
            filaAuxiliar[1] = fila[4]; //D
            filaAuxiliar[2] = tipoEuler.Diferencial(filaAuxiliar[1], filaAuxiliar[0], cola, tiempo);
            filaAuxiliar[3] = filaAuxiliar[0] + h; //t(i+1)
            filaAuxiliar[4] = tipoEuler.Ysiguiente(filaAuxiliar[1], h, filaAuxiliar[2]); //D(i+1)

            return filaAuxiliar;
        }
        
        public void mostrarFila(double[] fila)
        {
            form.mostrarFila(fila);
        }

    }

    public interface ITipoEuler
    {

        bool ValidarCondicion(double Y_iteracion, double Do, double tiempo);
        double Diferencial(double D, double t, double cola, double tiempo);

        double Ysiguiente(double D, double h, double Diferencial);

    }

    class FinAtencionAprendiz : ITipoEuler
    {
        public bool ValidarCondicion(double D_iteracion, double D_anterior, double tiempo)
        {
            if (D_iteracion >= tiempo)
                return false;
            return true;
            
        }

        public double Diferencial(double D, double t, double cola, double tiempo)
        {
            return cola + 0.2 * tiempo + Math.Pow(t, 2);
        }

        public double Ysiguiente(double D, double h, double Diferencial)
        {
            return D + h * Diferencial;
        }
    }

    class FinAtencionVeteranoA : ITipoEuler

    {
        public bool ValidarCondicion(double D_iteracion, double D_anterior, double tiempo)
        {
            if (D_iteracion >= tiempo)
                return false;
            return true;
        }

        public double Diferencial(double D, double t, double cola, double tiempo)
        {
            return cola + 0.2 * tiempo + Math.Pow(t, 2);
        }

        public double Ysiguiente(double D, double h, double Diferencial)
        {
            return D + h * Diferencial;
        }
    }

    class FinAtencionVeteranoB : ITipoEuler
    {
        public bool ValidarCondicion(double D_iteracion, double D_anterior, double tiempo)
        {
            if (D_iteracion >= tiempo)
                return false;
            return true;
        }

        public double Diferencial(double D, double t, double cola, double tiempo)
        {
            return cola + 0.2 * tiempo + Math.Pow(t, 2);
        }

        public double Ysiguiente(double D, double h, double Diferencial)
        {
            return D + h * Diferencial;
        }
    }
}
