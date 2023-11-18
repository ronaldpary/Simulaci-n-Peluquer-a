using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Logica
{
    public class Cliente : IDisposable
    {
        #region Atributos
        public int numero { get; set; }
        public double estado { get; set; }
        public double hora_refrigerio { get; set; } = 0;
        public double peluquero_elegido { get; set; }
        public double tiene_refri { get; set; } = 0;
        public int col { get; set; } = -1;

        public int noMostrar { get; set; } = 0;

        public int bandera_refrigerio { get; set; } = 0;

        //REFRI
        public double hora_inicio_espera { get; set; } = 0;
        #endregion

        #region Constructor
        public Cliente(double peluquero_elegido)
        {
            this.peluquero_elegido = peluquero_elegido;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public enum Estado
        {
            esperando_atencionAp = 1, siendo_atendidoAp = 2,
            esperando_atencionA = 3, siendo_atendidoA = 4,
            esperando_atencionB = 5, siendo_atendidoB = 6,
            destruido = 7
        }
        public enum Peluquero
        {
            A = 1, B = 2, C = 3
        }
        #endregion

        #region Metodos
        public string estadoCliente(double estado)
        {
            string estadoActual = "";
            switch (estado)
            {
                case 1:
                    estadoActual = "EAA";
                    break;
                case 2:
                    estadoActual = "SAA";
                    break;
                case 3:
                    estadoActual = "EAVA";
                    break;
                case 4:
                    estadoActual = "SAVA";
                    break;
                case 5:
                    estadoActual = "EAVB";
                    break;
                case 6:
                    estadoActual = "SAVB";
                    break;
                case 7:
                    estadoActual = "Atendido";
                    break;
                default:
                    break;

            }
            return estadoActual;
        }
        public string peluqueroElegido(double peluquero_elegido) 
        {
            string peluqueroActual = "";
            switch (peluquero_elegido)
            {
                case 1:
                    peluqueroActual = "Aprendiz";
                    break;
                case 2:
                    peluqueroActual = "VeteranoA";
                    break;
                case 3:
                    peluqueroActual = "VeteranoB";
                    break;
                default:
                    break;
            }
            return peluqueroActual;
        }

        public bool isSiendoAtendido()
        {
            //Atenciones de cada peluquero o fue Atendido
            return estado == 2 || estado == 4 || estado == 6 || estado == 7;
        }


        #endregion
    }

    
}
