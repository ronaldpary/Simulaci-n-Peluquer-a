using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Logica
{
    public class Fila
    {
        #region Atributos
        public double Reloj { get; set; }
        public double fin_dia { get; set; } = 480;
        public double rnd_llegada { get; set; } = -1;
        public double tiempo_entre_llegada { get; set; } = -1;
        public double proxima_llegada { get; set; } = -1;
        public double rnd_atencion { get; set; } = -1;
        public double tiempo_atencion { get; set; } = -1;
        public double fin_atencion_aprendiz { get; set; } = -1;
        public double fin_atencion_veteA { get; set; } = -1;
        public double fin_Atencion_veteB { get; set; } = -1;
        public double rnd_peluquero { get; set; } = -1;
        public double peluquero { get; set; } = -1;
        public double estado_aprendiz { get; set; } = -1;
        public double cola_aprendiz { get; set; } = -1;
        public double estado_veteA { get; set; } = -1;
        public double cola_veteA { get; set; } = -1;
        public double estado_veteB { get; set; } = -1;
        public double cola_veteB { get; set; } = -1;
        public double total_recaudacion { get; set; } = 0;
        public double promedio_recaudacion { get; set; } = 0;
        public double clientes_maximos { get; set; } = 0;

        #endregion

        #region Metodos

        public void limpiarColumnasAprendiz()
        {
            this.rnd_atencion = -1;
            this.tiempo_atencion = -1;
            this.fin_atencion_aprendiz = -1;
        }

        public void limpiarColumnasVeteranoA()
        {
            this.rnd_atencion = -1;
            this.tiempo_atencion = -1;
            this.fin_atencion_veteA = -1;
        }

        public void limpiarColumndasVeteranoB()
        {
            this.rnd_atencion = -1;
            this.tiempo_atencion = -1;
            this.fin_Atencion_veteB = -1;
        }

        #endregion
    }
}
