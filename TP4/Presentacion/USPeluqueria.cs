using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP4.Logica;

namespace TP4.Presentacion
{
    public partial class USPeluqueria : UserControl
    {
        Parametros parametros = new Parametros();
        GestorSimulacionTP4 gestor;

        public USPeluqueria()
        {
            InitializeComponent();
            cargarValores();
        }

        private void cargarValores()
        {
            txtLleagaA.Text = this.parametros.llegadaA.ToString();
            txtLlegadaB.Text = this.parametros.llegadaB.ToString();
            txtAprendizA.Text = this.parametros.aprendizA.ToString();
            txtAprendizB.Text = this.parametros.aprendizB.ToString();
            txtVeteAA.Text = this.parametros.veteranoAA.ToString();
            txtVeteAB.Text = this.parametros.veteranoAB.ToString();
            txtVeteBA.Text = this.parametros.veteranoBA.ToString();
            txtVeteBB.Text = this.parametros.veteranoBB.ToString();
            txtProbabilidadA.Text = this.parametros.probabilidadAprendiz.ToString();
            txtProbabilidadVA.Text = this.parametros.probabilidadVeteranoA.ToString();
        }

        private void btnComenzar_Click(object sender, EventArgs e)
        {
            gestor = new GestorSimulacionTP4(this);
            if (txtSimulaciones.Text != "" && txtDesde.Text != "" && txtHasta.Text != "")
            {
                int n = Convert.ToInt32(txtSimulaciones.Text);
                int desde = Convert.ToInt32(txtDesde.Text);
                int hasta = Convert.ToInt32(txtHasta.Text);
                dgvEventos.Rows.Clear();
                validarDatos(parametros);
                gestor.iniciarSimulacion(Convert.ToInt32(txtSimulaciones.Text), this.parametros, Convert.ToInt32(txtDesde.Text), Convert.ToInt32(txtHasta.Text));

                dgvEventos.SelectedRows[0].Selected = false;
                dgvEventos.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                //dgvEventos.Rows[(hasta - desde) + 2].DefaultCellStyle.BackColor = Color.Yellow;

            }
            else
            {
                MessageBox.Show("Complete todos los datos");
            }
        }

        private void validarDatos(Parametros parametros)
        {
            if (txtLleagaA.Text != "") { parametros.llegadaA = Convert.ToDouble(txtLleagaA.Text); }
            if (txtLlegadaB.Text != "") { parametros.llegadaB = Convert.ToDouble(txtLlegadaB.Text); }
            if (txtAprendizA.Text != "") { parametros.aprendizA = Convert.ToDouble(txtAprendizA.Text); }
            if (txtAprendizB.Text != "") { parametros.aprendizB = Convert.ToDouble(txtAprendizB.Text); }
            if (txtVeteAA.Text != "") { parametros.veteranoAA = Convert.ToDouble(txtVeteAA.Text); }
            if (txtVeteAB.Text != "") { parametros.veteranoAB = Convert.ToDouble(txtVeteAB.Text); }
            if (txtVeteBA.Text != "") { parametros.veteranoBA = Convert.ToDouble(txtVeteBA.Text); }
            if (txtVeteBB.Text != "") { parametros.veteranoBB = Convert.ToDouble(txtVeteBB.Text); }
            if (txtProbabilidadA.Text != "") { parametros.probabilidadAprendiz = Convert.ToDouble(txtProbabilidadA.Text); }
            if (txtProbabilidadVA.Text != "") { parametros.probabilidadVeteranoA = Convert.ToDouble(txtProbabilidadVA.Text); }
        }

        private void MostrarError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void mostrarFila(Fila fila, List<Cliente> enElSistema, double numeroDia, double numeroSimulacion, string nombreEvento)
        {
            int index = dgvEventos.Rows.Add();
            dgvEventos.Rows[index].Resizable = DataGridViewTriState.False;

            PropertyInfo[] properties = typeof(Fila).GetProperties();

            //Asignamos los estados correspondientes
            foreach (PropertyInfo property in properties)
            {
                string nombreAtributo = property.Name;

                double valor = (double)property.GetValue(fila);
                dgvEventos.Rows[index].Cells["Dia"].Value = numeroDia;
                dgvEventos.Rows[index].Cells["Numero"].Value = numeroSimulacion;
                dgvEventos.Rows[index].Cells["Evento"].Value = nombreEvento;

                if (valor != -1)
                {
                    if (nombreAtributo == "estado_aprendiz" || nombreAtributo == "estado_veteA" || nombreAtributo == "estado_veteB")
                    {
                        string estado = valor == 0 ? "Libre" : "Ocupado";
                        dgvEventos.Rows[index].Cells[nombreAtributo].Value = estado;
                    }
                    else
                    {
                        dgvEventos.Rows[index].Cells[nombreAtributo].Value = valor;
                    }

                    //if (nombreAtributo == "peluquero")
                    //{
                    //    string tipoPeluquero = valor == 1 ? "Aprendiz" : valor == 2 ? "VeteranoA" : "VeteranoB";
                    //    dgvEventos.Rows[index].Cells[nombreAtributo].Value = tipoPeluquero;
                    //}
                    //else
                    //{
                    //    dgvEventos.Rows[index].Cells[nombreAtributo].Value = valor;
                    //}
                }
            }

            //Creamos las columnas de los clientes dinamicamente
            if (enElSistema.Count != 0)
            {
                for (int i = 0; i < enElSistema.Count; i++)
                {
                    if (dgvEventos.Columns["Estado" + i.ToString()] != null)
                    {
                        dgvEventos.Rows[index].Cells["Estado" + i.ToString()].Value = (enElSistema[i].estadoCliente(enElSistema[i].estado) + "(" + enElSistema[i].numero + ")").ToString();
                        dgvEventos.Rows[index].Cells["HLLR" + i.ToString()].Value = Convert.ToDecimal(enElSistema[i].hora_refrigerio.ToString()).ToString("N");
                        dgvEventos.Rows[index].Cells["HIA" + i.ToString()].Value = Convert.ToDecimal(enElSistema[i].hora_atencion.ToString()).ToString("N");
                    }
                    else
                    {
                        int indiceColumna = dgvEventos.Columns.Add("Estado" + i.ToString(), "Estado");
                        dgvEventos.Rows[index].Cells[indiceColumna].Value = (enElSistema[i].estadoCliente(enElSistema[i].estado) + "(" + enElSistema[i].numero + ")").ToString();

                        int indiceColumna2 = dgvEventos.Columns.Add("HLLR" + i.ToString(), "HLLR");
                        dgvEventos.Rows[index].Cells[indiceColumna2].Value = Convert.ToDecimal(enElSistema[i].hora_refrigerio.ToString()).ToString("N");

                        int indiceColumna3 = dgvEventos.Columns.Add("HIA" + i.ToString(), "HIA");
                        dgvEventos.Rows[index].Cells[indiceColumna3].Value = Convert.ToDecimal(enElSistema[i].hora_atencion.ToString()).ToString("N");
                    }
                }
            }
        }
    }
}
