using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.AnimatorNS;
using static TP4.Logica.Cliente;
using static TP4.Logica.Empleado;

namespace TP4.Logica
{
    public class Eventos
    {
        public GestorSimulacionTP4 gestor;
        private Fila fila;
        private int id;

        public int maximoSillas;
 
        public Eventos(Fila fila, GestorSimulacionTP4 gestor)
        {
            this.fila = fila;
            this.id = 0;
            this.gestor = gestor;
        }

        public Cliente proximaLlegada()
        {
            id++;
            gestor.generarTiempoProximaLlegada();

            fila.rnd_peluquero = Math.Round(gestor.rndPeluquero.NextDouble(), 2);

            Cliente cliente = primerCliente(fila.rnd_peluquero);
            fila.peluquero = cliente.peluquero_elegido;
            cliente.numero = id;

            gestor.enElSistema.Add(cliente);

            return cliente;

        }

        private Cliente primerCliente(double rnd_peluquero)
        {

            Cliente cliente;
            if (rnd_peluquero <= gestor.probabilidadAp)
            {
                cliente = new Cliente((double)Peluquero.A);
                consultarColaAprendiz(cliente);
            }
            else if (rnd_peluquero <= gestor.probabilidadAp + gestor.probabilidadVA)
            {
                cliente = new Cliente((double)Peluquero.B);
                consultarColaVeteranoA(cliente);
            }
            else
            {
                cliente = new Cliente((double)Peluquero.C);
                consultarColaVeteranoB(cliente);
            }

            return cliente;

        }

        private void consultarColaVeteranoB(Cliente cliente)
        {
            //Preguntamos si el veteranoA esta libre
            if (gestor.peluqueroVeteB.empleado.estado == (double)EstadosEmpleados.libre)
            {
                gestor.peluqueroVeteB.empleado.estado = (double)EstadosEmpleados.ocupado;

                gestor.peluqueroVeteB.empleado.cliente = cliente;

                gestor.generarTiempoAtencionVeteB();

                cliente.estado = (double)Estado.siendo_atendidoB; //Cliente en atencion
            }
            else
            {
                gestor.peluqueroVeteB.cola.Enqueue(cliente);

                cliente.estado = (double)Estado.esperando_atencionB; //Cliente esperando
                cliente.hora_refrigerio = fila.Reloj + 30;
            }
        }

        private void consultarColaVeteranoA(Cliente cliente)
        {
            //Preguntamos si el veteranoA esta libre
            if (gestor.peluqueroVeteA.empleado.estado == (double)EstadosEmpleados.libre)
            {
                gestor.peluqueroVeteA.empleado.estado = (double)EstadosEmpleados.ocupado;

                gestor.peluqueroVeteA.empleado.cliente = cliente;

                gestor.generarTiempoAtencionVeteA();

                cliente.estado = (double)Estado.siendo_atendidoA; //Cliente en atencion
            }
            else
            {
                gestor.peluqueroVeteA.cola.Enqueue(cliente);

                cliente.estado = (double)Estado.esperando_atencionA; //Cliente esperando
                cliente.hora_refrigerio = fila.Reloj + 30;
            }
        }

        private void consultarColaAprendiz(Cliente cliente)
        {
            //Preguntamos si el aprendiz esta libre
            if (gestor.peluqueroAprendiz.empleado.estado == (double)EstadosEmpleados.libre)
            {
                gestor.peluqueroAprendiz.empleado.estado = (double)EstadosEmpleados.ocupado;

                gestor.peluqueroAprendiz.empleado.cliente = cliente;

                gestor.generarTiempoAtencionAP();

                cliente.estado = (double)Estado.siendo_atendidoAp; //Cliente en atencion
            }
            else
            {
                gestor.peluqueroAprendiz.cola.Enqueue(cliente);

                cliente.estado = (double)Estado.esperando_atencionAp; //Cliente esperando
                cliente.hora_refrigerio = fila.Reloj + 30;
            }
        }

        public void finAtencionAprendiz(Cliente clienteFin)
        {
            //Acumulador para la recaudacion
            fila.total_recaudacion = fila.total_recaudacion + 1800;

            //Acumulador para el promedio
            fila.promedio_recaudacion = fila.total_recaudacion / gestor.numeroDia;

            clienteAtendido(clienteFin); //Borramos los clientes atendidos

            //Ponemos en libre el aprendiz
            gestor.peluqueroAprendiz.empleado.estado = (double)EstadosEmpleados.libre;
            fila.limpiarColumnasAprendiz();

            //Preguntamos si tiene clientes en cola
            if (gestor.peluqueroAprendiz.cola.Count != 0)
            {
                Cliente siguienteCliente = gestor.peluqueroAprendiz.cola.Dequeue();
                consultarColaAprendiz(siguienteCliente);
            }
       
        }

        public void finAtencionVeteA(Cliente clienteFin)
        {
            //Acumulador para la recaudacion
            fila.total_recaudacion = fila.total_recaudacion + 3500;

            //Acumulador para el promedio
            fila.promedio_recaudacion = fila.total_recaudacion / gestor.numeroDia;

            clienteAtendido(clienteFin); //Borramos los clientes atendidos

            //Ponemos en libre veteranoA
            gestor.peluqueroVeteA.empleado.estado = (double)EstadosEmpleados.libre;
            fila.limpiarColumnasVeteranoA();

            //Preguntamos si tiene clientes en cola
            if (gestor.peluqueroVeteA.cola.Count != 0)
            {
                Cliente siguienteCliente = gestor.peluqueroVeteA.cola.Dequeue();
                consultarColaVeteranoA(siguienteCliente);
            }
  
        }

        public void finAtencionVeteB(Cliente clienteFin)
        {
            //Acumulador para la recaudacion
            fila.total_recaudacion = fila.total_recaudacion + 3500;

            //Acumulador para el promedio
            fila.promedio_recaudacion = fila.total_recaudacion / gestor.numeroDia;

            clienteAtendido(clienteFin); //Borramos los clientes atendidos

            //Ponemos en libre veteranoB
            gestor.peluqueroVeteB.empleado.estado = (double)EstadosEmpleados.libre;
            fila.limpiarColumndasVeteranoB();

            //Preguntamos si tiene clientes en cola
            if (gestor.peluqueroVeteB.cola.Count != 0)
            {
                Cliente siguienteCliente = gestor.peluqueroVeteB.cola.Dequeue();
                consultarColaVeteranoB(siguienteCliente);
            }
 
        }

        public void clienteAtendido(Cliente cliente)
        {
            cliente.estado = (double)Estado.destruido;
            cliente.Dispose();
        }

        public void finDia()
        {
            //fila.proxima_llegada = -1;
            fila.Reloj = 0;
            fila.fin_dia = 480;
            gestor.generarTiempoProximaLlegada();
        }

        public int cantidadMaximaSillas()
        {
            int cantidadActualClientesEsperando = gestor.peluqueroAprendiz.cola.Count + gestor.peluqueroVeteA.cola.Count + gestor.peluqueroVeteB.cola.Count;

            gestor.peluqueroAprendiz.cola.Count();
            gestor.peluqueroVeteA.cola.Count();
            gestor.peluqueroVeteB.cola.Count();

            return cantidadActualClientesEsperando;
        }
    }
}
