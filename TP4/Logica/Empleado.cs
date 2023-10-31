using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Logica
{
    public class Empleado
    {
        #region Atributos
        public double estado { get; set; }
        public Cliente cliente { get; set; }
        #endregion

        #region Constructor
        public Empleado() { }
        public enum EstadosEmpleados
        {
            libre = 0, ocupado = 1
        }
        #endregion

        #region Metodos
        #endregion
    }
}
