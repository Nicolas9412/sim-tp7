using Numeros_aleatorios.LibreriaSimulacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP7___Comidas_rápidas;

namespace Numeros_aleatorios.Colas
{
    class GestorSimulacion
    {
        PantallaResultados pantalla;
        Simulacion simulacion;
        public double tiempoCompra;
        Truncador truncador = new Truncador(4);



        public GestorSimulacion(PantallaResultados pantalla)
        {
            this.pantalla = pantalla;
        }

        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, double tiempoLlegadaClienteA, double tiempoLlegadaClienteB,
                            double tiempoLlegadaCajaA, double tiempoLlegadaCajaB, double tiempoAtencionCCA,
                            double tiempoAtencionCCB, double tiempoAtencionSA, double tiempoAtencionSB,
                            double tiempoPermCCA, double tiempoPermCCB, double tiempoPermSA, double tiempoPermSB, double corteC, double paso)
        {
            calcularTiempo(paso, corteC);
            ejecutar(filaDesde, filaHasta, cantSimulaciones, tiempoLlegadaClienteA, tiempoLlegadaClienteB,
                    tiempoLlegadaCajaA, tiempoLlegadaCajaB, truncador.truncar(this.tiempoCompra),
                    tiempoAtencionCCA, tiempoAtencionCCB, tiempoAtencionSA, tiempoAtencionSB,tiempoPermCCA, 
                    tiempoPermCCB, tiempoPermSA, tiempoPermSB) ;
            calcularEstadisticas();
        }

        private void calcularTiempo(double paso, double corteC)
        {
            RungeKutta rungeKutta = new RungeKutta();
            RungeKuttaResultados pantallaRungeKutta = new RungeKuttaResultados();

            rungeKutta.calcularRungeKuttaTiempoCompra(paso,corteC);

            this.tiempoCompra = rungeKutta.tiempo160;
            
            Truncador truncador = new Truncador(4);

            pantallaRungeKutta.mostrarResultados(rungeKutta.tabla,"Tiempo de compra: " + truncador.truncar(tiempoCompra).ToString() + " minutos");
        }

        private void ejecutar( int filaDesde, int filaHasta,  int cantSimulaciones, double tiempoLlegadaClienteA, double tiempoLlegadaClienteB,
                               double tiempoLlegadaCajaA, double tiempoLlegadaCajaB, double tiempoCompra,
                               double tiempoAtencionCCA, double tiempoAtencionCCB, double tiempoAtencionSA, double tiempoAtencionSB, 
                               double tiempoPermCCA, double tiempoPermCCB, double tiempoPermSA, double tiempoPermSB)
        {
            simulacion = new Simulacion();
            simulacion.simular(filaDesde, filaHasta, cantSimulaciones, tiempoLlegadaClienteA, tiempoLlegadaClienteB,
                               tiempoLlegadaCajaA, tiempoLlegadaCajaB, tiempoCompra,
                               tiempoAtencionCCA, tiempoAtencionCCB, tiempoAtencionSA, tiempoAtencionSB,
                               tiempoPermCCA, tiempoPermCCB, tiempoPermSA, tiempoPermSB);
            pantalla.mostrarResultados(simulacion.getResultados());
            pantalla.Show();
        }

        private void calcularEstadisticas()
        {
            simulacion.calcularEstadisticas(this);
        }

        
        public void mostrarEstadisticas(int longitudMaxColaCC, Double tiempoPromedioPermanenciaEnBar, Double porcentajeTiempoOcupacionInformes,
                                        Double porcentajeTiempoOciosoSandwich, Double porcentajeClientesAtendidos)
        {
            pantalla.mostrarEstadisticas(longitudMaxColaCC, tiempoPromedioPermanenciaEnBar, porcentajeTiempoOcupacionInformes,
                                         porcentajeTiempoOciosoSandwich, porcentajeClientesAtendidos);
        }
        
    }
}
