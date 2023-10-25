using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP4.Presentacion;
using static TP4.Logica.Cliente;

namespace TP4.Logica
{
    public class GestorSimulacionTP4
    {
        // Creamos los empleados y los peluqueros
        public Empleado aprendiz = new Empleado();
        public Empleado veteranoA = new Empleado();
        public Empleado veteranoB = new Empleado();

        public Peluqueria peluqueroAprendiz;
        public Peluqueria peluqueroVeteA;
        public Peluqueria peluqueroVeteB;

        private USPeluqueria interfaz;
        private Eventos eventos;
        private Generadores generador;

        public Random rndLlegada = new Random();
        public Random rndAtencion = new Random();
        public Random rndPeluquero = new Random();

        //Iniciamos los parametros por defecto
        public double llegadaA = 2;
        public double llegadaB = 12;
        public double aprendizA = 20;
        public double aprendizB = 30;
        public double veteranoAA = 11;
        public double veteranoAB = 13;
        public double veteranoBA = 12;
        public double veteranoBB = 18;

        public double probabilidadAp = 15;
        public double probabilidadVA = 45;

        //Variables para los puntos pedidos, acumuladores y contadores
        private int contadorDias;
        private double acumuladorRecaudacion;
        private double promedioRecaudacion;
        private int contadorClientes;
        private int contadorSillas;

        public bool fin = false;

        //Variable para cada fila x
        public Fila fila;

        //Lista de clientes en el sistema
        public List<Cliente> enElSistema;

        public int cantFilasMostradas;

        public GestorSimulacionTP4 (USPeluqueria interfaz) 
        {
            this.interfaz = interfaz;
            this.fila = new Fila();
            this.enElSistema = new List<Cliente>();
            this.peluqueroAprendiz = new Peluqueria(aprendiz);
            this.peluqueroVeteA = new Peluqueria(veteranoA);
            this.peluqueroVeteB = new Peluqueria(veteranoB);
            generador = new Generadores();

        }

        //Metodo que se llama desde la interfaz
        public void iniciarSimulacion(int dias, Parametros parametros, int desde, int hasta)
        {
            eventos = new Eventos(fila, this);
            this.llegadaA = parametros.llegadaA;
            this.llegadaB = parametros.llegadaB;
            this.aprendizA = parametros.aprendizA;
            this.aprendizB = parametros.aprendizB;
            this.veteranoAA = parametros.veteranoAA;
            this.veteranoAB = parametros.veteranoAB;
            this.veteranoBA = parametros.veteranoBA;
            this.veteranoBB = parametros.veteranoBB;
            this.probabilidadAp = parametros.probabilidadAprendiz;
            this.probabilidadVA = parametros.probabilidadVeteranoA;
            //Metodo para iniciar la simulacion
            comenzarSimulacion(dias, desde, hasta);
        }

        private void comenzarSimulacion(int dias, int desde, int hasta)
        {
            generarTiempoProximaLlegada();
            actualizarEstados();
            actualizarColas();

            double numeroDia = 0;
            double numeroSimulacion = 0;
            string nombreEvento = "Inicio";

            //Mostramos la fila inicio
            interfaz.mostrarFila(fila, enElSistema, numeroDia, numeroSimulacion, nombreEvento);

            //Para cada dia recorremos
            for (int i = 0; i < dias; i++)
            {
                fin = false;

                //Mientras haya tiempo para atender a un cliente
                while (fin == false) //El veteranoA es el que tiene el menor tiempo de atencion//cambiar
                {
                    numeroDia = i + 1;
                    double siguienteTiempo = definirSiguienteTiempo(fila);
                    double relojAnterior = fila.Reloj;
                    fila.Reloj = siguienteTiempo;
                    numeroSimulacion = numeroSimulacion + 1;

                    fila.fin_dia = fila.fin_dia - (fila.Reloj - relojAnterior);

                    if (Math.Truncate(siguienteTiempo * 10000) / 10000 == Math.Truncate(fila.proxima_llegada * 10000) / 10000)
                    {
                        // Ver si es necesario tener un contador para los clientes que ingresen al sistema
                        Cliente clienteCreado = eventos.proximaLlegada();
                        nombreEvento = "Llegada de cliente " + "(" + clienteCreado.numero.ToString() + ")";

                        if (fila.proxima_llegada > 480)
                        {
                            fila.proxima_llegada = -1;
                        }

                    }
                    else if (Math.Truncate(siguienteTiempo * 10000) / 10000 == Math.Truncate(fila.fin_atencion_aprendiz * 10000) / 10000)
                    {
                        for (int j = 0; j < enElSistema.Count; j++)
                        {
                            if (enElSistema[j].estado == (double)Estado.siendo_atendidoAp)
                            {

                                eventos.finAtencionAprendiz(enElSistema[j]);
                                nombreEvento = "Fin atencion Ap" + "(" + enElSistema[j].numero.ToString() + ")";

                                break;
                            }
                        }
                    }
                    else if (Math.Truncate(siguienteTiempo * 10000) / 10000 == Math.Truncate(fila.fin_atencion_veteA * 10000) / 10000)
                    {

                        for (int j = 0; j < enElSistema.Count; j++)
                        {
                            if (enElSistema[j].estado == (double)Estado.siendo_atendidoA)
                            {

                                eventos.finAtencionVeteA(enElSistema[j]);
                                nombreEvento = "Fin atencion VA" + "(" + enElSistema[j].numero.ToString() + ")";

                                break;
                            }
                        }
                    }
                    else if (Math.Truncate(siguienteTiempo * 10000) / 10000 == Math.Truncate(fila.fin_Atencion_veteB * 10000) / 10000)
                    {
                        for (int j = 0; j < enElSistema.Count; j++)
                        {
                            if (enElSistema[j].estado == (double)Estado.siendo_atendidoB)
                            {

                                eventos.finAtencionVeteB(enElSistema[j]);
                                nombreEvento = "Fin atencion VB" + "(" + enElSistema[j].numero.ToString() + ")";

                                break;
                            }
                        }                        
                    }
                    

                    actualizarColas();
                    actualizarEstados();

                    if(numeroSimulacion >= desde && numeroSimulacion <= hasta)
                    {
                        interfaz.mostrarFila(fila, enElSistema, numeroDia, numeroSimulacion, nombreEvento);
                        cantFilasMostradas++;
                    }

                    //Eliminamos los clientes que hayan sido atendidos
                    eliminarClientesAtendidos();

                    //if (enElSistema.Count == 0 && fila.fin_atencion_aprendiz == -1 && fila.fin_atencion_veteA == -1 && fila.fin_Atencion_veteB == -1 && fila.Reloj > 480)
                    if (enElSistema.Count == 0)
                    {

                        numeroSimulacion = numeroSimulacion + 1;

                        eventos.finDia();
                        nombreEvento = "Fin dia" + "(" + numeroDia.ToString() + ")";

                        if (numeroSimulacion >= desde && numeroSimulacion <= hasta)
                        {
                            interfaz.mostrarFila(fila, enElSistema, numeroDia, numeroSimulacion, nombreEvento);
                            cantFilasMostradas++;
                        }

                        fin = true;

                    }

                }

            }

            //Mostramos la ultima fila
            interfaz.mostrarFila(fila, enElSistema, numeroDia, numeroSimulacion, nombreEvento);

        }

        private double definirSiguienteTiempo(Fila fila)
        {
            List<double> listaEventos = new Double[] {fila.proxima_llegada, fila.fin_atencion_aprendiz, fila.fin_atencion_veteA, fila.fin_Atencion_veteB}.ToList();
            listaEventos.RemoveAll(x => x == -1);
            double minimo = listaEventos.Min();
            return minimo;
        }

        private void eliminarClientesAtendidos()
        {
            enElSistema.RemoveAll(clienteAtendido);
        }

        private bool clienteAtendido(Cliente obj)
        {
            return obj.estado == (double)Estado.destruido;
        }

        private void actualizarColas()
        {
            fila.cola_aprendiz = peluqueroAprendiz.cola.Count;
            fila.cola_veteA = peluqueroVeteA.cola.Count;
            fila.cola_veteB = peluqueroVeteB.cola.Count;
        }

        private void actualizarEstados()
        {
            fila.estado_aprendiz = peluqueroAprendiz.empleado.estado;
            fila.estado_veteA = peluqueroVeteA.empleado.estado;
            fila.estado_veteB = peluqueroVeteB.empleado.estado;
        }

        public void generarTiempoProximaLlegada()
        {
            fila.rnd_llegada = rndLlegada.NextDouble();
            fila.tiempo_entre_llegada = generador.GeneradorUniforme(llegadaA, llegadaB, fila.rnd_llegada);
            fila.proxima_llegada = fila.Reloj + fila.tiempo_entre_llegada;
        }

        public void generarTiempoAtencionVeteB()
        {
            fila.rnd_atencion = rndAtencion.NextDouble();
            fila.tiempo_atencion = generador.GeneradorUniforme(veteranoBA, veteranoBB, fila.rnd_atencion);
            fila.fin_Atencion_veteB = fila.Reloj + fila.tiempo_atencion;
        }

        public void generarTiempoAtencionVeteA()
        {
            fila.rnd_atencion = rndAtencion.NextDouble();
            fila.tiempo_atencion = generador.GeneradorUniforme(veteranoAA, veteranoAB, fila.rnd_atencion);
            fila.fin_atencion_veteA = fila.Reloj + fila.tiempo_atencion;
        }

        public void generarTiempoAtencionAP()
        {
            fila.rnd_atencion = rndAtencion.NextDouble();
            fila.tiempo_atencion = generador.GeneradorUniforme(aprendizA, aprendizB, fila.rnd_atencion);
            fila.fin_atencion_aprendiz = fila.Reloj + fila.tiempo_atencion;
        }
    }
}
