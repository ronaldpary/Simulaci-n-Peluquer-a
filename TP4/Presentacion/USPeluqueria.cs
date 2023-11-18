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
            txtProbabilidadVB.Text = this.parametros.probabilidadVeteranoB.ToString();
            txtH.Text = this.parametros.h.ToString();
            txtTAprendiz.Text = this.parametros.TAprendiz.ToString();
            txtTVeterano.Text = this.parametros.TVeterano.ToString();
        }

        private void btnComenzar_Click(object sender, EventArgs e)
        {
            
            gestor = new GestorSimulacionTP4(this);
            if (txtSimulaciones.Text != "" && txtDesde.Text != "" && txtHasta.Text != "")
            {
                double probabilidadAp = Convert.ToDouble(txtProbabilidadA.Text);
                double probabilidadVA = Convert.ToDouble(txtProbabilidadVA.Text);
                double probabilidadVB = 1 - (probabilidadAp + probabilidadVA);

                if (probabilidadVB < 0)
                {
                    MessageBox.Show("Las probabilidades no deben superar 1");
                } else if (Convert.ToDouble(txtAprendizA.Text) > Convert.ToDouble(txtAprendizB.Text)) {
                    MessageBox.Show("Aprendiz: B no debe ser mayor a A");
                }
                else if (Convert.ToDouble(txtVeteAA.Text) > Convert.ToDouble(txtVeteAB.Text))
                {
                    MessageBox.Show("Veterano A: B no debe ser mayor a A");
                }
                else if (Convert.ToDouble(txtVeteBA.Text) > Convert.ToDouble(txtVeteBB.Text))
                {
                    MessageBox.Show("Veterano B: B no debe ser mayor a A");
                }
                else if (Convert.ToDouble(txtLleagaA.Text) > Convert.ToDouble(txtLlegadaB.Text))
                {
                    MessageBox.Show("Llegada: B no debe ser mayor a A");
                }
                else if (Convert.ToDouble(txtDesde.Text) > Convert.ToDouble(txtHasta.Text))
                {
                    MessageBox.Show("Desde no puede ser mayor a Hasta.");
                }
                else if (Convert.ToDouble(txtProbabilidadA.Text) < 0 || Convert.ToDouble(txtProbabilidadA.Text) > 1)
                {
                    MessageBox.Show("La probabilidad debe estar entre 0 y 1.");
                }
                else if (Convert.ToDouble(txtProbabilidadVA.Text) < 0 || Convert.ToDouble(txtProbabilidadVA.Text) > 1 )
                {
                    MessageBox.Show("La probabilidad debe estar entre 0 y 1.");
                }
                else
                {
                   
                    dgvEventos.Rows.Clear();
                    
                    validarDatos(parametros);
                    txtProbabilidadVB.Text = Convert.ToString(probabilidadVB);

                    gestor.iniciarSimulacion(Convert.ToInt32(txtSimulaciones.Text), this.parametros, Convert.ToInt32(txtDesde.Text), Convert.ToInt32(txtHasta.Text));

                    dgvEventos.SelectedRows[0].Selected = false;
                    dgvEventos.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                    //dgvEventos.Rows[(hasta - desde) + 2].DefaultCellStyle.BackColor = Color.Yellow;
                }

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
            if (txtProbabilidadVB.Text != "") { parametros.probabilidadVeteranoB = 1 - (Convert.ToDouble(txtProbabilidadA.Text) + Convert.ToDouble(txtProbabilidadVA.Text)); }
            if (txtH.Text != "") { parametros.h = Convert.ToDouble(txtH.Text); }
            if (txtTAprendiz.Text != "") { parametros.TAprendiz = Convert.ToDouble(txtTAprendiz.Text); }
            if (txtTVeterano.Text != "") { parametros.TVeterano = Convert.ToDouble(txtTVeterano.Text); }
        }

        private void MostrarError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void mostrarFila(Fila fila, List<Cliente> enElSistema, double numeroDia, double numeroSimulacion, string nombreEvento)
        {

            try {
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


                    }
                }

                //Creamos las columnas de los clientes dinamicamente //VERSION CON FALLO DE EXCESO DE COLUMNAS, DESCOMENTAR ESTO Y COMENTAR EL METODO DE ELIMINAR CLIENTESELIMINADOS QUE ESTA EN EL GESTOR


                if (enElSistema.Count != 0)
                {
                    if (int.Parse(txtDesde.Text) == numeroSimulacion)
                    {
                        //if (enElSistema.All(Cliente => Cliente.estado == 7))
                        //{
                        //  enElSistema.RemoveAt(i);
                        //}
                        enElSistema.RemoveAll(Cliente => Cliente.estado == 7);
                    }

                    for (int i = 0; i < enElSistema.Count; i++)
                    {

                        


                        if (dgvEventos.Columns["Estado" + i.ToString()] != null)
                        {

                            if (enElSistema[i].noMostrar == 1)
                            {
                                dgvEventos.Rows[index].Cells["Estado" + i.ToString()].Value = "";
                                dgvEventos.Rows[index].Cells["HLLR" + i.ToString()].Value = "";
                            }
                            else
                            {
                                dgvEventos.Rows[index].Cells["Estado" + i.ToString()].Value = (enElSistema[i].estadoCliente(enElSistema[i].estado) + "(" + enElSistema[i].numero + ")").ToString();

                                if (enElSistema[i].bandera_refrigerio == 1)
                                {
                                    dgvEventos.Rows[index].Cells["HLLR" + i.ToString()].Value = 0;
                                }
                                else
                                {
                                    dgvEventos.Rows[index].Cells["HLLR" + i.ToString()].Value = Convert.ToDecimal(enElSistema[i].hora_refrigerio.ToString()).ToString("N");
                                }

                                if (enElSistema[i].estado == 7)
                                {
                                    enElSistema[i].noMostrar = 1;
                                }
                            }



                        }
                        else
                        {
                            int indiceColumna = dgvEventos.Columns.Add("Estado" + i.ToString(), "Estado");
                            dgvEventos.Rows[index].Cells[indiceColumna].Value = (enElSistema[i].estadoCliente(enElSistema[i].estado) + "(" + enElSistema[i].numero + ")").ToString();

                            int indiceColumna2 = dgvEventos.Columns.Add("HLLR" + i.ToString(), "HLLR");

                            if (enElSistema[i].bandera_refrigerio == 1)
                            {
                                dgvEventos.Rows[index].Cells[indiceColumna2].Value = 0;
                            }
                            else
                            {
                                dgvEventos.Rows[index].Cells[indiceColumna2].Value = Convert.ToDecimal(enElSistema[i].hora_refrigerio.ToString()).ToString("N");
                            }



                        }
                    }
                }

                //VERSION CON PERDIDA DE ALGUNAS CLIENTES

                //if (enElSistema.Count != 0)
                //{
                //    for (int i = 0; i < enElSistema.Count; i++)
                //    {

                //        if (dgvEventos.Columns["Estado" + i.ToString()] != null)
                //        {
                //            //Verifica si todavia no tiene columna asiganada o si hay alguna vacio (cehquear lo ultimo)
                //            if (enElSistema[i].col == -1 || (enElSistema[i].col == -1 && dgvEventos.Rows[index].Cells["Estado" + enElSistema[i].col.ToString()].Value == null))
                //            {
                //                enElSistema[i].col = i;
                //            }

                //            dgvEventos.Rows[index].Cells["Estado" + enElSistema[i].col.ToString()].Value = (enElSistema[i].estadoCliente(enElSistema[i].estado) + "(" + enElSistema[i].numero + ")").ToString() + enElSistema[i].col.ToString();

                //            // Si está siendo atendido, lo setea en cero
                //            if (enElSistema[i].isSiendoAtendido())
                //            {
                //                dgvEventos.Rows[index].Cells["HLLR" + enElSistema[i].col.ToString()].Value = 0.00d;
                //            }
                //            else
                //            {
                //                dgvEventos.Rows[index].Cells["HLLR" + enElSistema[i].col.ToString()].Value = Convert.ToDecimal(enElSistema[i].hora_refrigerio.ToString()).ToString("N");
                //            }


                //        }
                //        else
                //        {
                //            if (enElSistema[i].col == -1)
                //            {
                //                enElSistema[i].col = i;
                //            }
                //            int indiceColumna = dgvEventos.Columns.Add("Estado" + i.ToString(), "Estado");

                //            dgvEventos.Rows[index].Cells[indiceColumna].Value = (enElSistema[i].estadoCliente(enElSistema[i].estado) + "(" + enElSistema[i].numero + ")").ToString() + enElSistema[i].col.ToString();
                //            int indiceColumna2 = dgvEventos.Columns.Add("HLLR" + enElSistema[i].col.ToString(), "HLLR");
                //            // Si está siendo atendido, lo setea en cero
                //            if (enElSistema[i].isSiendoAtendido())
                //            {
                //                dgvEventos.Rows[index].Cells[indiceColumna2].Value = 0.00d;
                //            }
                //            else
                //            {
                //                dgvEventos.Rows[index].Cells[indiceColumna2].Value = Convert.ToDecimal(enElSistema[i].hora_refrigerio.ToString()).ToString("N");
                //            }




                //        }
                //    }
                //}

            }
            catch { }
            

        }

        private void gpDescripcionRespuesta_Click(object sender, EventArgs e)
        {

        }

        private void CeldaClickeada(object sender, DataGridViewCellEventArgs e)
        {
            int indexColum = e.ColumnIndex;
            int index = e.RowIndex;
            string nombreColumna = dgvEventos.Columns[indexColum].Name.ToString();

            if (nombreColumna == "fin_atencion_aprendiz")
            {
                double colaAprendiz = Convert.ToDouble(dgvEventos.Rows[index].Cells["cola_aprendiz"].Value.ToString());
                ITipoEuler tipoEuler = new FinAtencionAprendiz();
                EulerForm form = new EulerForm(parametros.h, 0, 0, tipoEuler, colaAprendiz, parametros.TAprendiz);
                form.Show();
            }
            else if (nombreColumna == "fin_atencion_veteA")
            {
                double colaVeteA = Convert.ToDouble(dgvEventos.Rows[index].Cells["cola_veteA"].Value.ToString());
                ITipoEuler tipoEuler = new FinAtencionVeteranoA();
                EulerForm form = new EulerForm(parametros.h, 0, 0, tipoEuler, colaVeteA, parametros.TVeterano);
                form.Show();
            }
            else if (nombreColumna == "fin_atencion_veteB")
            {
                double colaVeteB = Convert.ToDouble(dgvEventos.Rows[index].Cells["cola_veteB"].Value.ToString());
                ITipoEuler tipoEuler = new FinAtencionVeteranoB();
                EulerForm form = new EulerForm(parametros.h, 0, 0, tipoEuler, colaVeteB, parametros.TVeterano);
                form.Show();
            }
        }

        private void dgvEventos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
