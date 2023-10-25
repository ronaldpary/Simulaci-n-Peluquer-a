using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Logica
{
    public class Peluqueria
    {
        #region Atributos
        public Queue<Cliente> cola { get; set; }
        public Empleado empleado;
        #endregion

        #region Constructor
        public Peluqueria(Empleado empleado)
        {
            this.empleado = empleado;
            this.cola = new Queue<Cliente>();
        }
        #endregion
    }
}
