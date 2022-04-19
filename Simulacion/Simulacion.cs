using Numeros_aleatorios.LibreriaSimulacion;
using Numeros_aleatorios.LibreriaSimulacion.GeneradoresAleatorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP7___Comidas_rápidas.Simulacion;

namespace Numeros_aleatorios.Colas
{
    class Simulacion
    {
        double[] probabilidadesTipoComida = new double[] { 0.8, 1 };
        string[] tipoComida = new string[] { "CC", "Sandwich" };


        DataTable resultados;
        private int cantidadClientes;
        private int indice;
        private Linea lineaActual;
        public Truncador truncador = new Truncador(4);

        private List<List<String>> lineas;
        
        public Simulacion()
        {
            resultados = new DataTable();
            crearTabla(resultados);
            this.lineas = new List<List<string>>(100);
        }

        private void crearTabla(DataTable tabla)
        {
            tabla.Columns.Add("i", typeof(string));
            tabla.Columns.Add("evento", typeof(string));
            tabla.Columns.Add("reloj", typeof(string));
            tabla.Columns.Add("RND llegada", typeof(string));
            tabla.Columns.Add("RND demora", typeof(string));
            tabla.Columns.Add("tiempo entre llegadas", typeof(string));
            tabla.Columns.Add("tiempo llegada caja", typeof(string));
            tabla.Columns.Add("llegada cliente", typeof(string));
            tabla.Columns.Add("tiempo atencion caja", typeof(string));
            tabla.Columns.Add("fin atencion caja", typeof(string));
            tabla.Columns.Add("RND tipo comida", typeof(string));
            tabla.Columns.Add("Tipo de comida", typeof(string));
            tabla.Columns.Add("RND Atencion Sand", typeof(string));
            tabla.Columns.Add("tiempo atencion sandwich", typeof(string));
            tabla.Columns.Add("fin atencion sandwich", typeof(string));
            tabla.Columns.Add("RND atencion CC", typeof(string));
            tabla.Columns.Add("tiempo atencion CC", typeof(string));
            tabla.Columns.Add("fin atencion CC (1)", typeof(string));
            tabla.Columns.Add("fin atencion CC (2)", typeof(string));
            tabla.Columns.Add("RND perm", typeof(string));
            tabla.Columns.Add("tiempo perm", typeof(string));
            tabla.Columns.Add("fin perm (1)", typeof(string));
            tabla.Columns.Add("fin perm (2)", typeof(string));
            tabla.Columns.Add("fin perm (3)", typeof(string));
            tabla.Columns.Add("fin perm (4)", typeof(string));
            tabla.Columns.Add("fin perm (5)", typeof(string));
            tabla.Columns.Add("fin perm (6)", typeof(string));
            tabla.Columns.Add("fin perm (7)", typeof(string));
            tabla.Columns.Add("fin perm (8)", typeof(string));
            tabla.Columns.Add("fin perm (9)", typeof(string));
            tabla.Columns.Add("fin perm (10)", typeof(string));
            tabla.Columns.Add("fin perm (11)", typeof(string));
            tabla.Columns.Add("fin perm (12)", typeof(string));
            tabla.Columns.Add("fin perm (13)", typeof(string));
            tabla.Columns.Add("fin perm (14)", typeof(string));
            tabla.Columns.Add("fin perm (15)", typeof(string));
            tabla.Columns.Add("fin perm (16)", typeof(string));
            tabla.Columns.Add("fin perm (17)", typeof(string));
            tabla.Columns.Add("fin perm (18)", typeof(string));
            tabla.Columns.Add("fin perm (19)", typeof(string));
            tabla.Columns.Add("fin perm (20)", typeof(string));
            tabla.Columns.Add("fin perm (21)", typeof(string));
            tabla.Columns.Add("fin perm (22)", typeof(string));
            tabla.Columns.Add("fin perm (23)", typeof(string));
            tabla.Columns.Add("fin perm (24)", typeof(string));
            tabla.Columns.Add("fin perm (25)", typeof(string));
            tabla.Columns.Add("fin perm (26)", typeof(string));
            tabla.Columns.Add("fin perm (27)", typeof(string));
            tabla.Columns.Add("fin perm (28)", typeof(string));
            tabla.Columns.Add("fin perm (29)", typeof(string));
            tabla.Columns.Add("fin perm (30)", typeof(string));
            tabla.Columns.Add("fin perm (31)", typeof(string));
            tabla.Columns.Add("fin perm (32)", typeof(string));
            tabla.Columns.Add("fin perm (33)", typeof(string));
            tabla.Columns.Add("fin perm (34)", typeof(string));
            tabla.Columns.Add("fin perm (35)", typeof(string));
            tabla.Columns.Add("fin perm (36)", typeof(string));
            tabla.Columns.Add("fin perm (37)", typeof(string));
            tabla.Columns.Add("fin perm (38)", typeof(string));
            tabla.Columns.Add("fin perm (39)", typeof(string));
            tabla.Columns.Add("fin perm (40)", typeof(string));
            tabla.Columns.Add("fin perm (41)", typeof(string));
            tabla.Columns.Add("fin perm (42)", typeof(string));
            tabla.Columns.Add("fin perm (43)", typeof(string));
            tabla.Columns.Add("fin perm (44)", typeof(string));
            tabla.Columns.Add("fin perm (45)", typeof(string));
            tabla.Columns.Add("fin perm (46)", typeof(string));
            tabla.Columns.Add("fin perm (47)", typeof(string));
            tabla.Columns.Add("fin perm (48)", typeof(string));
            tabla.Columns.Add("fin perm (49)", typeof(string));
            tabla.Columns.Add("fin perm (50)", typeof(string));
            tabla.Columns.Add("Estado Caja", typeof(string));
            tabla.Columns.Add("Cola Caja", typeof(string));
            tabla.Columns.Add("Estado Atencion Sandwich", typeof(string));
            tabla.Columns.Add("Cola Atencion Sandwich", typeof(string));
            tabla.Columns.Add("Estado Atencion CC (1)", typeof(string));
            tabla.Columns.Add("Estado Atencion CC (2)", typeof(string));
            tabla.Columns.Add("Cola Atencion CC", typeof(string));
            tabla.Columns.Add("Est Mesa (1)", typeof(string));
            tabla.Columns.Add("Est Mesa (2)", typeof(string));
            tabla.Columns.Add("Est Mesa (3)", typeof(string));
            tabla.Columns.Add("Est Mesa (4)", typeof(string));
            tabla.Columns.Add("Est Mesa (5)", typeof(string));
            tabla.Columns.Add("Est Mesa (6)", typeof(string));
            tabla.Columns.Add("Est Mesa (7)", typeof(string));
            tabla.Columns.Add("Est Mesa (8)", typeof(string));
            tabla.Columns.Add("Est Mesa (9)", typeof(string));
            tabla.Columns.Add("Est Mesa (10)", typeof(string));
            tabla.Columns.Add("Est Mesa (11)", typeof(string));
            tabla.Columns.Add("Est Mesa (12)", typeof(string));
            tabla.Columns.Add("Est Mesa (13)", typeof(string));
            tabla.Columns.Add("Est Mesa (14)", typeof(string));
            tabla.Columns.Add("Est Mesa (15)", typeof(string));
            tabla.Columns.Add("Est Mesa (16)", typeof(string));
            tabla.Columns.Add("Est Mesa (17)", typeof(string));
            tabla.Columns.Add("Est Mesa (18)", typeof(string));
            tabla.Columns.Add("Est Mesa (19)", typeof(string));
            tabla.Columns.Add("Est Mesa (20)", typeof(string));
            tabla.Columns.Add("Est Mesa (21)", typeof(string));
            tabla.Columns.Add("Est Mesa (22)", typeof(string));
            tabla.Columns.Add("Est Mesa (23)", typeof(string));
            tabla.Columns.Add("Est Mesa (24)", typeof(string));
            tabla.Columns.Add("Est Mesa (25)", typeof(string));
            tabla.Columns.Add("Est Mesa (26)", typeof(string));
            tabla.Columns.Add("Est Mesa (27)", typeof(string));
            tabla.Columns.Add("Est Mesa (28)", typeof(string));
            tabla.Columns.Add("Est Mesa (29)", typeof(string));
            tabla.Columns.Add("Est Mesa (30)", typeof(string));
            tabla.Columns.Add("Est Mesa (31)", typeof(string));
            tabla.Columns.Add("Est Mesa (32)", typeof(string));
            tabla.Columns.Add("Est Mesa (33)", typeof(string));
            tabla.Columns.Add("Est Mesa (34)", typeof(string));
            tabla.Columns.Add("Est Mesa (35)", typeof(string));
            tabla.Columns.Add("Est Mesa (36)", typeof(string));
            tabla.Columns.Add("Est Mesa (37)", typeof(string));
            tabla.Columns.Add("Est Mesa (38)", typeof(string));
            tabla.Columns.Add("Est Mesa (39)", typeof(string));
            tabla.Columns.Add("Est Mesa (40)", typeof(string));
            tabla.Columns.Add("Est Mesa (41)", typeof(string));
            tabla.Columns.Add("Est Mesa (42)", typeof(string));
            tabla.Columns.Add("Est Mesa (43)", typeof(string));
            tabla.Columns.Add("Est Mesa (44)", typeof(string)); 
            tabla.Columns.Add("Est Mesa (45)", typeof(string));
            tabla.Columns.Add("Est Mesa (46)", typeof(string));
            tabla.Columns.Add("Est Mesa (47)", typeof(string));
            tabla.Columns.Add("Est Mesa (48)", typeof(string));
            tabla.Columns.Add("Est Mesa (49)", typeof(string));
            tabla.Columns.Add("Est Mesa (50)", typeof(string));
            tabla.Columns.Add("Pedidos CC llevados",typeof(string));
            tabla.Columns.Add("Pedidos Sandwich llevados", typeof(string));
            tabla.Columns.Add("Max Cola Atencion CC", typeof(string));
            tabla.Columns.Add("Acum tiempo perm cliente", typeof(string));
            tabla.Columns.Add("Cont clientes atendidos", typeof(string));
            tabla.Columns.Add("Acum tiempo ocup Caja", typeof(string));
            tabla.Columns.Add("Acum tiempo ocioso Mostrador Sandwich", typeof(string));
        }

        public void simular(int filaDesde, int filaHasta, int cantSimulaciones, double tiempoLlegadaClienteA, double tiempoLlegadaClienteB,
                            double tiempoLlegadaCajaA, double tiempoLlegadaCajaB, double tiempoCompra,
                            double tiempoAtencionCCA, double tiempoAtencionCCB, double tiempoAtencionSA, double tiempoAtencionSB,
                            double tiempoPermCCA, double tiempoPermCCB, double tiempoPermSA, double tiempoPermSB)
        {

            Linea lineaAnterior = new Linea(50,2, tiempoLlegadaClienteA, tiempoLlegadaClienteB,
                                      tiempoLlegadaCajaA, tiempoLlegadaCajaB, tiempoCompra,
                                      tiempoAtencionCCA, tiempoAtencionCCB, tiempoAtencionSA, tiempoAtencionSB,
                                      tiempoPermCCA, tiempoPermCCB, tiempoPermSA, tiempoPermSB);
       
            int i;
         

            agregarLinea(lineaAnterior, 0);

            for (i = 1; i <= cantSimulaciones; i++)
            {
                lineaActual = new Linea(lineaAnterior, this, filaDesde, filaHasta, i);
                lineaActual.calcularEvento();
                lineaActual.calcularSiguienteLlegada();
                lineaActual.calcularFinCompra();
                lineaActual.calcularTipoComida(probabilidadesTipoComida, tipoComida);
                lineaActual.calcularFinAtencionSandwich();
                lineaActual.calcularFinAtencionCC();
                lineaActual.calcularFinPermanencia();
                lineaActual.calcularEstadisticas();

                lineaAnterior = lineaActual;

                if (i >= filaDesde && i <= filaHasta)
                {
                    agregarLinea(lineaActual, i);
                }
            }

            agregarLinea(lineaActual, lineaActual.idFila);
        }

        public DataTable getResultados()
        {
            return this.resultados;
        }

        public void calcularEstadisticas(GestorSimulacion gestor)
        {
            int tamañoTabla = resultados.Rows.Count-1;

            string longitudMaxColaCC = resultados.Rows[tamañoTabla][130].ToString();
            
            string tiempoPermanencia = resultados.Rows[tamañoTabla][131].ToString();
            string cantidadClientesAtendidos = resultados.Rows[tamañoTabla][132].ToString();
            double tiempoPromedioPermanenciaEnBar = cantidadClientesAtendidos != "0" ?
                                                    double.Parse(tiempoPermanencia) / double.Parse(cantidadClientesAtendidos) : 0;

            string tiempoOcupacionCaja = resultados.Rows[tamañoTabla][133].ToString();
            string tiempoTotalSimulado = resultados.Rows[tamañoTabla][2].ToString();
            double porcentajeTiempoOcupacionInformes = tiempoTotalSimulado != "0"?
                                                (double.Parse(tiempoOcupacionCaja) * 100) / double.Parse(tiempoTotalSimulado) : 0;

            string tiempoOciosoMostradorSandwich = resultados.Rows[tamañoTabla][134].ToString();
            double porcentajeTiempoOciosoSandwich = tiempoTotalSimulado != "0" ?
                                                (double.Parse(tiempoOciosoMostradorSandwich) * 100) / double.Parse(tiempoTotalSimulado) : 0;

            string cantidadClientesCCIdos = resultados.Rows[tamañoTabla][128].ToString();
            string cantidadClientesCCSandwich = resultados.Rows[tamañoTabla][129].ToString();
            double cantidadClientesTotales = double.Parse(cantidadClientesCCIdos) + 
                                             double.Parse(cantidadClientesCCSandwich) + 
                                             double.Parse(cantidadClientesAtendidos);
            double porcentajeClientesAtendidos = cantidadClientesTotales != 0 ?
                                                 (double.Parse(cantidadClientesAtendidos) * 100) / cantidadClientesTotales : 0;

            gestor.mostrarEstadisticas( int.Parse(longitudMaxColaCC), tiempoPromedioPermanenciaEnBar, porcentajeTiempoOcupacionInformes,
                                        porcentajeTiempoOciosoSandwich, porcentajeClientesAtendidos);
        }

        public void agregarColumna()
        {
            cantidadClientes++;
            this.resultados.Columns.Add("estado " + cantidadClientes, typeof(string));
            this.resultados.Columns.Add("hora llegadaBar " + cantidadClientes, typeof(string));
        }

        private void agregarLinea(Linea linea, int i)
        {
            DataRow row = resultados.NewRow();
            row[0] = i.ToString();
            row[1] = linea.evento.ToString();
            row[2] = truncador.truncar(linea.reloj).ToString();
            row[3] = linea.rndTiempoEntreLlegada.ToString() != "-1" ? truncador.truncar(linea.rndTiempoEntreLlegada).ToString() : "".ToString();
            row[4] = linea.rndTiempoLlegadaCaja.ToString() != "-1" ? truncador.truncar(linea.rndTiempoLlegadaCaja).ToString() : "".ToString();
            row[5] = linea.tiempoEntreLlegada.ToString() != "-1" ? truncador.truncar(linea.tiempoEntreLlegada).ToString() : "".ToString();
            row[6] = linea.tiempoLlegadaCaja.ToString() != "-1" ? truncador.truncar(linea.tiempoLlegadaCaja).ToString() : "".ToString();
            row[7] = truncador.truncar(linea.llegadaCliente).ToString();
            row[8] = linea.tiempoAtencionCaja.ToString() != "-1" ? truncador.truncar(linea.tiempoAtencionCaja).ToString() : "".ToString();
            row[9] = linea.caja.finCompra.ToString() != "-1" ? truncador.truncar(linea.caja.finCompra).ToString() : "".ToString();
            row[10] = linea.rndTipoComida.ToString() != "-1" ? truncador.truncar(linea.rndTipoComida).ToString() : "".ToString();
            row[11] = linea.tipoComida.ToString();
            row[12] = linea.rndTiempoAtencionSandwich.ToString() != "-1" ? truncador.truncar(linea.rndTiempoAtencionSandwich).ToString() : "".ToString();
            row[13] = linea.tiempoAtencionSandwich.ToString() != "-1" ? truncador.truncar(linea.tiempoAtencionSandwich).ToString() : "".ToString();
            row[14] = linea.mostradorSandwich.finAtencionSandwich.ToString() != "-1" ? truncador.truncar(linea.mostradorSandwich.finAtencionSandwich).ToString() : "".ToString();
            row[15] = linea.rndTiempoAtencionCC.ToString() != "-1" ? truncador.truncar(linea.rndTiempoAtencionCC).ToString() : "".ToString();
            row[16] = linea.tiempoAtencionCC.ToString() != "-1" ? truncador.truncar(linea.tiempoAtencionCC).ToString() : "".ToString();
            row[17] = linea.mostradoresCC[0].finAtencionCC.ToString() != "-1" ? truncador.truncar(linea.mostradoresCC[0].finAtencionCC).ToString() : "".ToString();
            row[18] = linea.mostradoresCC[1].finAtencionCC.ToString() != "-1" ? truncador.truncar(linea.mostradoresCC[1].finAtencionCC).ToString() : "".ToString();
            row[19] = linea.rndTiempoPermanencia.ToString() != "-1" ? truncador.truncar(linea.rndTiempoPermanencia).ToString() : "".ToString();
            row[20] = linea.tiempoPermanencia.ToString() != "-1" ? truncador.truncar(linea.tiempoPermanencia).ToString() : "".ToString();
            row[21] = linea.mesas[0].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[0].finPermanencia).ToString() : "".ToString();
            row[22] = linea.mesas[1].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[1].finPermanencia).ToString() : "".ToString();
            row[23] = linea.mesas[2].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[2].finPermanencia).ToString() : "".ToString();
            row[24] = linea.mesas[3].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[3].finPermanencia).ToString() : "".ToString();
            row[25] = linea.mesas[4].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[4].finPermanencia).ToString() : "".ToString();
            row[26] = linea.mesas[5].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[5].finPermanencia).ToString() : "".ToString();
            row[27] = linea.mesas[6].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[6].finPermanencia).ToString() : "".ToString();
            row[28] = linea.mesas[7].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[7].finPermanencia).ToString() : "".ToString();
            row[29] = linea.mesas[8].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[8].finPermanencia).ToString() : "".ToString();
            row[30] = linea.mesas[9].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[9].finPermanencia).ToString() : "".ToString();
            row[31] = linea.mesas[10].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[10].finPermanencia).ToString() : "".ToString();
            row[32] = linea.mesas[11].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[11].finPermanencia).ToString() : "".ToString();
            row[33] = linea.mesas[12].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[12].finPermanencia).ToString() : "".ToString();
            row[34] = linea.mesas[13].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[13].finPermanencia).ToString() : "".ToString();
            row[35] = linea.mesas[14].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[14].finPermanencia).ToString() : "".ToString();
            row[36] = linea.mesas[15].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[15].finPermanencia).ToString() : "".ToString();
            row[37] = linea.mesas[16].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[16].finPermanencia).ToString() : "".ToString();
            row[38] = linea.mesas[17].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[17].finPermanencia).ToString() : "".ToString();
            row[39] = linea.mesas[18].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[18].finPermanencia).ToString() : "".ToString();
            row[40] = linea.mesas[19].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[19].finPermanencia).ToString() : "".ToString();
            row[41] = linea.mesas[20].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[20].finPermanencia).ToString() : "".ToString();
            row[42] = linea.mesas[21].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[21].finPermanencia).ToString() : "".ToString();
            row[43] = linea.mesas[22].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[22].finPermanencia).ToString() : "".ToString();
            row[44] = linea.mesas[23].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[23].finPermanencia).ToString() : "".ToString();
            row[45] = linea.mesas[24].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[24].finPermanencia).ToString() : "".ToString();
            row[46] = linea.mesas[25].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[25].finPermanencia).ToString() : "".ToString();
            row[47] = linea.mesas[26].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[26].finPermanencia).ToString() : "".ToString();
            row[48] = linea.mesas[27].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[27].finPermanencia).ToString() : "".ToString();
            row[49] = linea.mesas[28].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[28].finPermanencia).ToString() : "".ToString();
            row[50] = linea.mesas[29].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[29].finPermanencia).ToString() : "".ToString();
            row[51] = linea.mesas[30].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[30].finPermanencia).ToString() : "".ToString();
            row[52] = linea.mesas[31].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[31].finPermanencia).ToString() : "".ToString();
            row[53] = linea.mesas[32].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[32].finPermanencia).ToString() : "".ToString();
            row[54] = linea.mesas[33].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[33].finPermanencia).ToString() : "".ToString();
            row[55] = linea.mesas[34].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[34].finPermanencia).ToString() : "".ToString();
            row[56] = linea.mesas[35].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[35].finPermanencia).ToString() : "".ToString();
            row[57] = linea.mesas[36].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[36].finPermanencia).ToString() : "".ToString();
            row[58] = linea.mesas[37].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[37].finPermanencia).ToString() : "".ToString();
            row[59] = linea.mesas[38].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[38].finPermanencia).ToString() : "".ToString();
            row[60] = linea.mesas[39].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[39].finPermanencia).ToString() : "".ToString();
            row[61] = linea.mesas[40].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[40].finPermanencia).ToString() : "".ToString();
            row[62] = linea.mesas[41].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[41].finPermanencia).ToString() : "".ToString();
            row[63] = linea.mesas[42].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[42].finPermanencia).ToString() : "".ToString();
            row[64] = linea.mesas[43].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[43].finPermanencia).ToString() : "".ToString();
            row[65] = linea.mesas[44].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[44].finPermanencia).ToString() : "".ToString();
            row[66] = linea.mesas[45].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[45].finPermanencia).ToString() : "".ToString();
            row[67] = linea.mesas[46].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[46].finPermanencia).ToString() : "".ToString();
            row[68] = linea.mesas[47].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[47].finPermanencia).ToString() : "".ToString();
            row[69] = linea.mesas[48].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[48].finPermanencia).ToString() : "".ToString();
            row[70] = linea.mesas[49].finPermanencia.ToString() != "-1" ? truncador.truncar(linea.mesas[49].finPermanencia).ToString() : "".ToString();
            row[71] = linea.caja.estado;
            row[72] = linea.caja.cola.Count.ToString();
            row[73] = linea.mostradorSandwich.estado;
            row[74] = linea.mostradorSandwich.cola.Count.ToString();
            row[75] = linea.mostradoresCC[0].estado.ToString();
            row[76] = linea.mostradoresCC[1].estado.ToString();
            row[77] = MostradorCC.cola.Count.ToString();
            row[78] = linea.mesas[0].estado.ToString();
            row[79] = linea.mesas[1].estado.ToString();
            row[80] = linea.mesas[2].estado.ToString();
            row[81] = linea.mesas[3].estado.ToString();
            row[82] = linea.mesas[4].estado.ToString();
            row[83] = linea.mesas[5].estado.ToString();
            row[84] = linea.mesas[6].estado.ToString();
            row[85] = linea.mesas[7].estado.ToString();
            row[86] = linea.mesas[8].estado.ToString();
            row[87] = linea.mesas[9].estado.ToString();
            row[88] = linea.mesas[10].estado.ToString();
            row[89] = linea.mesas[11].estado.ToString();
            row[90] = linea.mesas[12].estado.ToString();
            row[91] = linea.mesas[13].estado.ToString();
            row[92] = linea.mesas[14].estado.ToString();
            row[93] = linea.mesas[15].estado.ToString();
            row[94] = linea.mesas[16].estado.ToString();
            row[95] = linea.mesas[17].estado.ToString();
            row[96] = linea.mesas[18].estado.ToString();
            row[97] = linea.mesas[19].estado.ToString();
            row[98] = linea.mesas[20].estado.ToString();
            row[99] = linea.mesas[21].estado.ToString();
            row[100] = linea.mesas[22].estado.ToString();
            row[101] = linea.mesas[23].estado.ToString();
            row[102] = linea.mesas[24].estado.ToString();
            row[103] = linea.mesas[25].estado.ToString();
            row[104] = linea.mesas[26].estado.ToString();
            row[105] = linea.mesas[27].estado.ToString();
            row[106] = linea.mesas[28].estado.ToString();
            row[107] = linea.mesas[29].estado.ToString();
            row[108] = linea.mesas[30].estado.ToString();
            row[109] = linea.mesas[31].estado.ToString();
            row[110] = linea.mesas[32].estado.ToString();
            row[111] = linea.mesas[33].estado.ToString();
            row[112] = linea.mesas[34].estado.ToString();
            row[113] = linea.mesas[35].estado.ToString();
            row[114] = linea.mesas[36].estado.ToString();
            row[115] = linea.mesas[37].estado.ToString();
            row[116] = linea.mesas[38].estado.ToString();
            row[117] = linea.mesas[39].estado.ToString();
            row[118] = linea.mesas[40].estado.ToString();
            row[119] = linea.mesas[41].estado.ToString();
            row[120] = linea.mesas[42].estado.ToString();
            row[121] = linea.mesas[43].estado.ToString();
            row[122] = linea.mesas[44].estado.ToString();
            row[123] = linea.mesas[45].estado.ToString();
            row[124] = linea.mesas[46].estado.ToString();
            row[125] = linea.mesas[47].estado.ToString();
            row[126] = linea.mesas[48].estado.ToString();
            row[127] = linea.mesas[49].estado.ToString();
            row[128] = linea.contadorClienteConCCIdos.ToString();
            row[129] = linea.contadorClienteConSandwichIdos.ToString();
            row[130] = linea.maxLongitudColaAtencionCC.ToString();
            row[131] = truncador.truncar(linea.acumuladorTiempoPermanenciaCliente).ToString();
            row[132] = linea.contadorClientesAtendidos.ToString();
            row[133] = truncador.truncar(linea.acumuladorTiempoOcupacionCaja).ToString();
            row[134] = truncador.truncar(linea.acumuladorTiempoOciosoMostradorSandwich).ToString();

            indice = 134;
                for (int j = 0; j < cantidadClientes; j++)
                {
                    indice += 1;
                    row[indice] = linea.clientes[j].estado.ToString();
                    indice += 1;
                    row[indice] = linea.clientes[j].horaLlegadaABar.ToString() != "-1" ? truncador.truncar(linea.clientes[j].horaLlegadaABar).ToString() : "".ToString(); ;
                }
            
            resultados.Rows.Add(row);
        }

    }
}
