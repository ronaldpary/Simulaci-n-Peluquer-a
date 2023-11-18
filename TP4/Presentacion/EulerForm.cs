using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP4.Logica;
using static TP4.Logica.Euler;

namespace TP4.Presentacion
{
    public partial class EulerForm : Form
    {
        public EulerForm(double h, double t0, double D0, ITipoEuler tipoEuler, double cola, double tiempo)
        {
            InitializeComponent();

            Euler eu = new Euler(h, t0, D0, tipoEuler, this, cola, tiempo);

            eu.calcularEuler();
        }

        public void mostrarFila(double[] fila)
        {
            dgvEuler.Rows.Add(fila[0], fila[1], fila[2], fila[3], fila[4]);
        }
        private void Euler_Load(object sender, EventArgs e)
        {

        }
    }
}
