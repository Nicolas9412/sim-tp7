using Numeros_aleatorios.LibreriaSimulacion;
using Numeros_aleatorios.LibreriaSimulacion.GeneradoresAleatorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP7___Comidas_rápidas.Simulacion;

namespace Numeros_aleatorios.Colas
{
    class Linea
    {
        public string INICIALIZACION = "inicializacion";
        public string LLEGADA_CLIENTE = "llegada de cliente";
        public string FIN_COMPRA = "fin de compra";
        public string FIN_ATENCION_SANDWICH = "fin atencion sandwich";
        public string FIN_ATENCION_CC = "fin atencion comida caliente";
        public string FIN_PERMANENCIA = "fin permanencia";


        public IGenerador aleatorios;
        public Truncador truncador;
        public IGenerador uniformeLlegadaCliente;
        public IGenerador uniformeLlegadaCaja;
        public IGenerador uniformeAtencionCC;
        public IGenerador uniformeAtencionS;
        public IGenerador uniformePermCC;
        public IGenerador uniformePermS;
        public string evento;
        public double reloj;
        public double rndTiempoEntreLlegada;
        public double rndTiempoLlegadaCaja;
        public double tiempoEntreLlegada;
        public double tiempoLlegadaCaja;
        public double llegadaCliente;
        public double tiempoAtencionCaja;
        public double rndTipoComida;
        public string tipoComida;
        public double rndTiempoAtencionSandwich;
        public double tiempoAtencionSandwich;
        public double rndTiempoAtencionCC;
        public double tiempoAtencionCC;
        public List<MostradorCC> mostradoresCC;
        public double rndTiempoPermanencia;
        public double tiempoPermanencia;
        public List<Mesa> mesas;
        

        public Caja caja;
        public MostradorSandwich mostradorSandwich;
        public Linea lineaAnterior;
        public MostradorCC mostradorCCFinAtencionCC;
        public Mesa mesaFinPermanencia;
        public List<Cliente> clientes;
        public Simulacion colas;
        public List<Cliente> clientesLibre;

        public int maxLongitudColaAtencionCC;
        public double acumuladorTiempoPermanenciaCliente;
        public int contadorClientesAtendidos;
        

        public int idFila;
        private int filaDesde;
        private int filaHasta;
        private double tiempoCompra;

        public int contadorClienteConCCIdos;
        public int contadorClienteConSandwichIdos;
        public double mostrador1FinAtencion;
        public double mostrador2FinAtencion;
        public Cliente mostrador1Cliente;
        public Cliente mostrador2Cliente;
        public Cliente mostradorSandwichCliente;

        public double acumuladorTiempoOcupacionCaja;
        public double acumuladorTiempoOciosoMostradorSandwich;

        public Linea(int cantidadMesas, int cantidadMostradoresCC, double tiempoLlegadaClienteA, double tiempoLlegadaClienteB,
                     double tiempoLlegadaCajaA, double tiempoLlegadaCajaB, double tiempoCompra,
                     double tiempoAtencionCCA, double tiempoAtencionCCB, double tiempoAtencionSA, double tiempoAtencionSB,
                     double tiempoPermCCA, double tiempoPermCCB, double tiempoPermSA, double tiempoPermSB)
        {
            this.contadorClienteConCCIdos = 0;
            this.contadorClienteConSandwichIdos = 0;
            this.tiempoCompra = tiempoCompra;
            this.truncador = new Truncador(4);
            this.aleatorios = new GeneradorUniformeLenguaje(truncador);
            this.caja = new Caja();
            this.mostradorSandwich = new MostradorSandwich();
            this.uniformeLlegadaCliente = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, tiempoLlegadaClienteA, tiempoLlegadaClienteB);
            this.uniformeLlegadaCaja = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, tiempoLlegadaCajaA, tiempoLlegadaCajaB);
            this.uniformeAtencionCC = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, tiempoAtencionCCA, tiempoAtencionCCB);
            this.uniformeAtencionS = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, tiempoAtencionSA, tiempoAtencionSB);
            this.uniformePermCC = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, tiempoPermCCA, tiempoPermCCB);
            this.uniformePermS = new GeneradorUniformeAB((GeneradorUniformeLenguaje)aleatorios, truncador, tiempoPermSA, tiempoPermSB);

            this.mostradoresCC = new List<MostradorCC>();
            cargarMostradoresCC(cantidadMostradoresCC);
            
            this.mesas = new List<Mesa>();
            cargarMesas(cantidadMesas);
            
            
            this.clientes = new List<Cliente>();
            this.clientesLibre = new List<Cliente>();

            this.tiempoEntreLlegada = uniformeLlegadaCliente.siguienteAleatorio();
            this.tiempoLlegadaCaja = uniformeLlegadaCaja.siguienteAleatorio();
            this.rndTiempoEntreLlegada = ((GeneradorUniformeAB)uniformeLlegadaCliente).getAleatorio();
            this.rndTiempoLlegadaCaja = ((GeneradorUniformeAB)uniformeLlegadaCaja).getAleatorio();
            this.llegadaCliente = tiempoEntreLlegada + tiempoLlegadaCaja;
            this.rndTipoComida = -1;
            this.rndTiempoAtencionCC = -1;
            this.rndTiempoAtencionSandwich = -1;
            this.rndTiempoPermanencia = -1;

            this.tiempoAtencionCaja = -1;
            this.tiempoAtencionCC = -1;
            this.tiempoAtencionSandwich = -1;
            this.tiempoPermanencia = -1;
            
            this.tipoComida = "";

            this.evento = INICIALIZACION;
        }

        public Linea(Linea anterior, Simulacion colas, int filaDesde, int filaHasta, int idFila)
        {
            this.tiempoCompra = anterior.tiempoCompra;
            this.lineaAnterior = anterior;
            this.truncador = anterior.truncador;
            this.aleatorios = anterior.aleatorios;
            this.uniformeLlegadaCliente = anterior.uniformeLlegadaCliente;
            this.uniformeLlegadaCaja = anterior.uniformeLlegadaCaja;
            this.uniformeAtencionCC = anterior.uniformeAtencionCC;
            this.uniformeAtencionS = anterior.uniformeAtencionS;
            this.uniformePermCC = anterior.uniformePermCC;
            this.uniformePermS = anterior.uniformePermS;

            this.caja = anterior.caja;
            this.mostradorSandwich = anterior.mostradorSandwich;

            this.mesas = anterior.mesas;
            this.mostradoresCC = anterior.mostradoresCC;
            this.mostrador1FinAtencion = anterior.mostradoresCC[0].finAtencionCC;
            this.mostrador2FinAtencion = anterior.mostradoresCC[1].finAtencionCC;
            this.mostrador1Cliente = anterior.mostradoresCC[0].getClienteActual();
            this.mostrador2Cliente = anterior.mostradoresCC[1].getClienteActual();
            this.mostradorSandwichCliente = anterior.mostradorSandwich.getClienteActual();
            
            this.clientes = anterior.clientes;

            this.colas = colas;
            this.clientesLibre = anterior.clientesLibre;

            this.filaDesde = filaDesde;
            this.filaHasta = filaHasta;
            this.idFila = idFila;

            this.rndTiempoEntreLlegada = -1;
            this.rndTiempoLlegadaCaja = -1;
            this.rndTipoComida = -1;
            this.rndTiempoAtencionCC = -1;
            this.rndTiempoAtencionSandwich = -1;
            this.rndTiempoPermanencia = -1;

            this.tiempoEntreLlegada = -1;
            this.tiempoLlegadaCaja = -1;
            this.tiempoAtencionCaja = -1;
            this.tiempoAtencionCC = -1;
            this.tiempoAtencionSandwich = -1;
            this.tiempoPermanencia = -1;

            this.acumuladorTiempoPermanenciaCliente = anterior.acumuladorTiempoPermanenciaCliente;
            this.contadorClientesAtendidos = anterior.contadorClientesAtendidos;
            this.contadorClienteConCCIdos = anterior.contadorClienteConCCIdos;
            this.contadorClienteConSandwichIdos = anterior.contadorClienteConSandwichIdos;
            this.maxLongitudColaAtencionCC = anterior.maxLongitudColaAtencionCC;
            this.acumuladorTiempoOcupacionCaja = anterior.acumuladorTiempoOcupacionCaja;
            this.acumuladorTiempoOciosoMostradorSandwich = anterior.acumuladorTiempoOciosoMostradorSandwich;

        }


        private void cargarMesas(int cantidadMesas)
        {
            for (int i = 1; i <= cantidadMesas; i++)
            {
                mesas.Add(new Mesa(i));
            }
        }

        private void cargarMostradoresCC(int cantidadMostradoresCC)
        {
            for (int i = 1; i <= cantidadMostradoresCC; i++)
            {
                mostradoresCC.Add(new MostradorCC(i));
                
            }
        }

        public void calcularEvento()
        {

            this.reloj = lineaAnterior.llegadaCliente;
            this.evento = LLEGADA_CLIENTE;

            if (lineaAnterior.caja.finCompra > 0 && lineaAnterior.caja.finCompra < reloj) {
                reloj = lineaAnterior.caja.finCompra;
                evento = FIN_COMPRA;
            }

            if (lineaAnterior.mostradorSandwich.finAtencionSandwich > 0 && lineaAnterior.mostradorSandwich.finAtencionSandwich < reloj)
            {
                reloj = lineaAnterior.mostradorSandwich.finAtencionSandwich;
                evento = FIN_ATENCION_SANDWICH;
            }

            foreach (var mostradorCC in lineaAnterior.mostradoresCC)
            {
                if (mostradorCC.finAtencionCC < reloj && mostradorCC.finAtencionCC > 0) {
                    reloj = mostradorCC.finAtencionCC;
                    mostradorCCFinAtencionCC = mostradorCC;
                    evento = FIN_ATENCION_CC;
                }
            }

            foreach (var mesa in lineaAnterior.mesas)
            {
                if (mesa.finPermanencia < reloj && mesa.finPermanencia > 0) {
                    reloj = mesa.finPermanencia;
                    mesaFinPermanencia = mesa;
                    evento = FIN_PERMANENCIA;
                }
            }
        }

        public void calcularSiguienteLlegada() {
            if (this.evento.Equals(LLEGADA_CLIENTE))
            {
                this.tiempoEntreLlegada = uniformeLlegadaCliente.siguienteAleatorio();
                this.tiempoLlegadaCaja = uniformeLlegadaCaja.siguienteAleatorio();
                this.rndTiempoEntreLlegada = ((GeneradorUniformeAB)uniformeLlegadaCliente).getAleatorio();
                this.rndTiempoLlegadaCaja = ((GeneradorUniformeAB)uniformeLlegadaCaja).getAleatorio();

                this.llegadaCliente = reloj + tiempoEntreLlegada + tiempoLlegadaCaja;
                return;
            }
            this.llegadaCliente = lineaAnterior.llegadaCliente;
        }


        public void calcularTipoComida(double[] probabilidadesTipoComida, string[] tipoComida)
        {
            rndTipoComida = -1;
            if (this.evento.Equals(FIN_COMPRA))
            {
                rndTipoComida = aleatorios.siguienteAleatorio();
                this.tipoComida = buscarProbabilidadEnVector(probabilidadesTipoComida, tipoComida, rndTipoComida);
                return;
            }
            this.tipoComida = "";
        }

        private string buscarProbabilidadEnVector(double[] probAcum, string[] vector, double random)
        {

            for (int i = 0; i < probAcum.Length; i++)
            {
                if (random < probAcum[i])
                {
                    return vector[i];
                }
            }
            return "";
        }

        
        private void calcularTiempoOcupacionCaja()
        {
            if (lineaAnterior.caja.estaOcupada())
            {
                this.acumuladorTiempoOcupacionCaja = this.lineaAnterior.acumuladorTiempoOcupacionCaja + (reloj - lineaAnterior.reloj);
            }
        }

        private void calcularTiempoOciosoMostradorSandwich()
        {
            if (!lineaAnterior.mostradorSandwich.estaOcupado())
            {
                this.acumuladorTiempoOciosoMostradorSandwich = this.lineaAnterior.acumuladorTiempoOciosoMostradorSandwich + (reloj - lineaAnterior.reloj);
            }
        }


        public void calcularFinCompra()
        {
            calcularTiempoOcupacionCaja();
            calcularFinCompraEventoLlegadaCliente();
            calcularFinCompraEventoFinCompra();
            calcularFinCompraEventoFinAtencionCC();
            calcularFinCompraEventoFinAtencionSandwich();
            calcularFinCompraEventoFinPermanencia();
        }

        public void calcularFinAtencionSandwich()
        {
            calcularTiempoOciosoMostradorSandwich();
            calcularFinAtencionSandwichEventoLlegadaCliente();
            calcularFinAtencionSandwichEventoFinCompra();
            calcularFinAtencionSandwichEventoFinAtencionCC();
            calcularFinAtencionSandwichEventoFinAtencionSandwich();
            calcularFinAtencionSanwichEventoFinPermanencia();
        }

        public void calcularFinAtencionCC()
        {
            calcularFinAtencionCCEventoLlegadaCliente();
            calcularFinAtencionCCEventoFinCompra();
            calcularFinAtencionCCEventoFinAtencionCC();
            calcularFinAtencionCCEventoFinAtencionSandwich();
            calcularFinAtencionCCEventoFinPermanencia();
        }

        public void calcularFinPermanencia() 
        {
            calcularFinPermanenciaEventoLlegadaCliente();
            calcularFinPermanenciaEventoFinCompra();
            calcularFinPermanenciaEventoFinAtencionCC();
            calcularFinPermanenciaEventoFinAtencionSandwich();
            calcularFinPermanenciaEventoFinPermanencia();
        }

        public void calcularEstadisticas()
        {
            calcularEstadisticaLongitudMaximaEnColaCC();
        }

        private void calcularFinCompraEventoFinCompra()
        {
            if (this.evento.Equals(FIN_COMPRA))
            {
                if (lineaAnterior.tieneColaCaja())
                {
                    this.tiempoAtencionCaja = this.tiempoCompra;
                    Cliente clienteActual = caja.siguienteCliente();
                    atenderCaja(clienteActual, tiempoAtencionCaja);
                }
                else
                {
                    caja.liberar();
                    tiempoAtencionCaja = -1;
                }
                return;
            }
        }

        private void calcularFinCompraEventoFinAtencionCC()
        {
            if (this.evento.Equals(FIN_ATENCION_CC) && lineaAnterior.tieneFinCompra())
            {
                caja.agregarFinCompra(lineaAnterior.obtenerFinCompra());
            }
        }

        private void calcularFinCompraEventoFinAtencionSandwich()
        {
            if (this.evento.Equals(FIN_ATENCION_SANDWICH) && lineaAnterior.tieneFinCompra())
            {
                caja.agregarFinCompra(lineaAnterior.obtenerFinCompra());
            }
        }

        private void calcularFinCompraEventoFinPermanencia()
        {
            if (this.evento.Equals(FIN_PERMANENCIA) && lineaAnterior.tieneFinCompra())
            {
                caja.agregarFinCompra(lineaAnterior.obtenerFinCompra());
            }
        }

        private void calcularFinCompraEventoLlegadaCliente()
        {
            if (this.evento.Equals(LLEGADA_CLIENTE)) 
            {
            Cliente clienteActual = buscarClienteLibre();
                if (lineaAnterior.tieneCajaOcupada())
                {
                    esperarCaja(clienteActual);
                }
                else
                {
                    clienteActual.horaLlegadaABar = this.reloj;
                    this.tiempoAtencionCaja = this.tiempoCompra;
                    atenderCaja(clienteActual, tiempoAtencionCaja);
                }
            }
            return;
        }

        public void calcularFinAtencionSandwichEventoLlegadaCliente()
        {
            if (this.evento.Equals(LLEGADA_CLIENTE) && lineaAnterior.tieneFinAtencionSandwich())
            {
                mostradorSandwich.agregarFinAtencionSandwich(lineaAnterior.obtenerFinAtencionSandwich());
            }
        }

        public void calcularFinAtencionSandwichEventoFinCompra()
        {
            if (this.evento.Equals(FIN_COMPRA) && tipoComida == "Sandwich") 
            {
                Cliente clienteActual = lineaAnterior.caja.getClienteActual();
                if (lineaAnterior.tieneMostradorSandwichOcupado())
                {
                    esperarMostradorSandwich(clienteActual);
                }
                else
                {
                    this.tiempoAtencionSandwich = uniformeAtencionS.siguienteAleatorio();
                    this.rndTiempoAtencionSandwich = ((GeneradorUniformeAB)uniformeAtencionS).getAleatorio();
                    atenderMostradorSandwich(clienteActual, tiempoAtencionSandwich);
                }
                return;
            }
        }

        public void calcularFinAtencionSandwichEventoFinAtencionCC() 
        {
            if (this.evento.Equals(FIN_ATENCION_CC) && lineaAnterior.tieneFinAtencionSandwich())
            {
                mostradorSandwich.agregarFinAtencionSandwich(lineaAnterior.obtenerFinAtencionSandwich());
            }
        }
        
        public void calcularFinAtencionSandwichEventoFinAtencionSandwich()
        {
            if (this.evento.Equals(FIN_ATENCION_SANDWICH))
            {
                if (lineaAnterior.tieneColaMostradorSandwich())
                {
                    this.tiempoAtencionSandwich = uniformeAtencionS.siguienteAleatorio();
                    this.rndTiempoAtencionSandwich = ((GeneradorUniformeAB)uniformeAtencionS).getAleatorio();
                    Cliente clienteActual = mostradorSandwich.siguienteCliente();
                    atenderMostradorSandwich(clienteActual, tiempoAtencionSandwich);
                }
                else
                {
                    mostradorSandwich.liberar();
                }
                return;
            }
        }

        public void calcularFinAtencionSanwichEventoFinPermanencia()
        {
            if(this.evento.Equals(FIN_PERMANENCIA) && lineaAnterior.tieneFinAtencionSandwich())
            {
                mostradorSandwich.agregarFinAtencionSandwich(lineaAnterior.obtenerFinAtencionSandwich());
            }
        }

        public void calcularFinPermanenciaEventoLlegadaCliente()
        {
            if (this.evento.Equals(LLEGADA_CLIENTE) && lineaAnterior.tieneFinPermanencia())
            {
                foreach (var mesa in mesas)
                {
                    if (!mesa.estaLibre())
                    {
                        mesa.agregarFinPermanencia(lineaAnterior.obtenerFinPermanencia(mesa.id));
                    }
                }
            }
        }

        public void calcularFinPermanenciaEventoFinCompra()
        {
            if (this.evento.Equals(FIN_COMPRA) && lineaAnterior.tieneFinPermanencia())
            {
                foreach (var mesa in mesas)
                {
                    if (!mesa.estaLibre())
                    {
                        mesa.agregarFinPermanencia(lineaAnterior.obtenerFinPermanencia(mesa.id));
                    }
                }
            }
        }

        public void calcularFinPermanenciaEventoFinAtencionCC()
        {
            if (this.evento.Equals(FIN_ATENCION_CC))
            {
                Cliente clienteActual = null;
                this.tiempoPermanencia = uniformePermCC.siguienteAleatorio();
                this.rndTiempoPermanencia = ((GeneradorUniformeAB)uniformePermCC).getAleatorio();

                if (truncador.truncar(this.reloj) == truncador.truncar(mostrador1FinAtencion))
                {
                    clienteActual = mostrador1Cliente;
                }
                if (truncador.truncar(this.reloj) == truncador.truncar(mostrador2FinAtencion))
                {
                    clienteActual = mostrador2Cliente;
                }
                

                Mesa mesaLibre = buscarMesaLibre();

                if (clienteActual != null)
                {
                    if (mesaLibre == null)
                    {
                        this.contadorClienteConCCIdos = this.lineaAnterior.contadorClienteConCCIdos + 1;
                    }
                    else
                    {
                        sentarseEnMesaConCC(clienteActual, mesaLibre, tiempoPermanencia);
                    }
                }
            }
            
        }

        public void calcularFinPermanenciaEventoFinAtencionSandwich()
        {
            if(this.evento.Equals(FIN_ATENCION_SANDWICH))
            {
                this.tiempoPermanencia = uniformePermS.siguienteAleatorio();
                this.rndTiempoPermanencia = ((GeneradorUniformeAB)uniformePermS).getAleatorio();
                Cliente clienteActual = mostradorSandwichCliente;
                Mesa mesaLibre = buscarMesaLibre();

                if (mesaLibre == null)
                {
                    this.contadorClienteConSandwichIdos = this.contadorClienteConSandwichIdos + 1;
                }
                else
                {
                    sentarseEnMesaConSandwich(clienteActual, mesaLibre, tiempoPermanencia);
                }
            }
        }

        public void calcularFinPermanenciaEventoFinPermanencia() 
        {
            if(this.evento.Equals(FIN_PERMANENCIA))
            {
                for(int i = 0; i < mesas.Count();i++)
                {
                    if(this.reloj == lineaAnterior.mesas[i].finPermanencia)
                    {
                        Cliente clienteViejo = mesas[i].clienteActual;
                        clienteViejo.tiempoPermanenciaBar = this.reloj - clienteViejo.horaLlegadaABar;
                        this.acumuladorTiempoPermanenciaCliente = this.lineaAnterior.acumuladorTiempoPermanenciaCliente + clienteViejo.tiempoPermanenciaBar;
                        this.contadorClientesAtendidos = this.lineaAnterior.contadorClientesAtendidos + 1;
                        clienteViejo.limpiar();
                        clientesLibre.Add(clienteViejo);
                        mesas[i].liberar();
                        
                    }
                }
            }
            
        }
        
        public void calcularEstadisticaLongitudMaximaEnColaCC()
        {
            int colaAnterior = lineaAnterior.maxLongitudColaAtencionCC;
            if(this.obtenerColaMostradoresCC() > colaAnterior)
            {
                this.maxLongitudColaAtencionCC = this.obtenerColaMostradoresCC();
            }
        }


        private void esperarCaja(Cliente clienteActual)
        {
 
            caja.agregarFinCompra(lineaAnterior.obtenerFinCompra());
            caja.agregarACola(clienteActual);
            clienteActual.esperarCaja();
            clienteActual.horaLlegadaABar = this.reloj;
        }

        private void esperarMostradorCC(Cliente nuevoCliente)
        {
            MostradorCC.agregarACola(nuevoCliente);
            nuevoCliente.esperarAtencionComidaCaliente();
        }

        private void esperarMostradorSandwich(Cliente clienteActual)
        {
            mostradorSandwich.agregarFinAtencionSandwich(lineaAnterior.obtenerFinAtencionSandwich());
            mostradorSandwich.agregarACola(clienteActual);
            clienteActual.esperarAtencionSandwich();
        }

        private void atenderCaja(Cliente clienteActual, double tiempo)
        {
            clienteActual.atenderCaja();
            caja.agregarFinCompra(this.reloj + tiempo);
            caja.clienteActual = clienteActual;
        }

        private void atenderMostradorSandwich(Cliente clienteActual, double tiempo)
        {
            clienteActual.atenderSandwich();
            mostradorSandwich.agregarFinAtencionSandwich(this.reloj + tiempo);
            mostradorSandwich.clienteActual = clienteActual;
        }

        private Cliente buscarClienteLibre()
        {
            if (clientesLibre.Count > 0)
            {
                Cliente libre = clientesLibre[clientesLibre.Count - 1];
                clientesLibre.RemoveAt(clientesLibre.Count - 1);
                return libre; 
            }

            Cliente res = new Cliente();
            this.clientes.Add(res);
            if(idFila <= filaHasta)
            {
                colas.agregarColumna();
            }
         
            return res;
        }

        private Boolean tieneCajaOcupada()
        {
            return this.caja.estaOcupada();
        }

        private Boolean tieneColaCaja()
        {
            return this.caja.tieneCola();
        }
        private Boolean tieneColaMostradorSandwich()
        {
            return this.mostradorSandwich.tieneCola();
        }

        private Boolean tieneFinCompra()
        {
            return this.caja.tieneFinCompra();
        }
        private Boolean tieneFinAtencionSandwich()
        {
            return this.mostradorSandwich.tieneFinAtencionSandwich();
        }

        private Boolean tieneFinAtencionCC()
        {
            foreach(var mostradorCC in mostradoresCC)
            {
                if(mostradorCC.tieneFinAtencionCC())
                {
                    return mostradorCC.tieneFinAtencionCC();
                }
            }
            return false;
        }

        private Boolean tieneFinPermanencia()
        {
            foreach (var mesa in mesas)
            {
                if (mesa.tieneFinPermanencia())
                {
                    return mesa.tieneFinPermanencia();
                }
            }
            return false;
        }


        private double obtenerFinCompra()
        {
            return this.caja.finCompra;
        }

        private double obtenerFinAtencionSandwich()
        {
            return this.mostradorSandwich.finAtencionSandwich;
        }

        private double obtenerFinAtencionCC(int id)
        {
            MostradorCC mostradorCC = mostradoresCC.ElementAt(id-1);
            return mostradorCC.finAtencionCC;
        }
        private double obtenerFinPermanencia(int id)
        {
            Mesa mesa = mesas.ElementAt(id - 1);
            return mesa.finPermanencia;
        }

        private Boolean tieneMostradorSandwichOcupado()
        {
            return this.mostradorSandwich.estaOcupado();
        }

        private int obtenerColaMostradoresCC()
        {
            return MostradorCC.getCola();
        }
        private void atenderMostradorCC(Cliente nuevoCliente, MostradorCC mostradorCCLibre, double tiempo)
        {
            mostradorCCLibre.clienteActual = nuevoCliente;
            nuevoCliente.atenderComidaCaliente(mostradorCCLibre.id);
            mostradorCCLibre.agregarFinAtencionCC(reloj + tiempo);
        }

        public void sentarseEnMesaConSandwich(Cliente nuevoCliente, Mesa mesaLibre, double tiempoPermanencia)
        {
            mesaLibre.clienteActual = nuevoCliente;
            nuevoCliente.sentadoMesaConSandwich(mesaLibre.id);
            mesaLibre.agregarFinPermanencia(this.reloj + tiempoPermanencia);
        }

        public void sentarseEnMesaConCC(Cliente nuevoCliente, Mesa mesaLibre, double tiempoPermanencia) 
        {
            mesaLibre.clienteActual = nuevoCliente;
            nuevoCliente.sentadoMesaConComidaCaliente(mesaLibre.id);
            mesaLibre.agregarFinPermanencia(this.reloj + tiempoPermanencia);
        }
        public void calcularFinAtencionCCEventoLlegadaCliente()
        {
            if (this.evento.Equals(LLEGADA_CLIENTE) && lineaAnterior.tieneFinAtencionCC())
            {
                foreach (var mostradorCC in mostradoresCC)
                {
                    if (!mostradorCC.estaLibre())
                    {
                        mostradorCC.agregarFinAtencionCC(lineaAnterior.obtenerFinAtencionCC(mostradorCC.id));
                    }
                }
            }
        }
        public void calcularFinAtencionCCEventoFinCompra()
        {
            if (this.evento.Equals(FIN_COMPRA) && tipoComida == "CC")
            {
                this.tiempoAtencionCC = uniformeAtencionCC.siguienteAleatorio();
                this.rndTiempoAtencionCC = ((GeneradorUniformeAB)uniformeAtencionCC).getAleatorio();
                Cliente clienteActual = lineaAnterior.caja.getClienteActual();
                MostradorCC mostradorCCLibre = buscarMostradorCCLibre();

                if (mostradorCCLibre == null)
                {
                    esperarMostradorCC(clienteActual);
                }
                else
                {
                    atenderMostradorCC(clienteActual, mostradorCCLibre, tiempoAtencionCC);
                }
            }
        }

        public void calcularFinAtencionCCEventoFinAtencionSandwich()
        {
            if (this.evento.Equals(FIN_ATENCION_SANDWICH) && lineaAnterior.tieneFinAtencionCC())
            {
                foreach (var mostradorCC in mostradoresCC)
                {
                    if (!mostradorCC.estaLibre())
                    {
                        mostradorCC.agregarFinAtencionCC(lineaAnterior.obtenerFinAtencionCC(mostradorCC.id));
                    }
                }
            }
        }

        public void calcularFinAtencionCCEventoFinAtencionCC()
        {

            if (this.evento.Equals(FIN_ATENCION_CC))
            {
                this.tiempoAtencionCC = uniformeAtencionCC.siguienteAleatorio();
                this.rndTiempoAtencionCC = ((GeneradorUniformeAB)uniformeAtencionCC).getAleatorio();
                mostradorCCFinAtencionCC.liberar();

                if (lineaAnterior.tieneColaMostradorCC())
                {
                    Cliente clienteActual = MostradorCC.siguienteCliente();
                    atenderMostradorCC(clienteActual, mostradorCCFinAtencionCC, tiempoAtencionCC);
                }
                return;
            }
        }

        public void calcularFinAtencionCCEventoFinPermanencia()
        {
            if (this.evento.Equals(FIN_PERMANENCIA) && lineaAnterior.tieneFinAtencionCC())
            {
                foreach (var mostradorCC in mostradoresCC)
                {
                    if (!mostradorCC.estaLibre())
                    {
                        mostradorCC.agregarFinAtencionCC(lineaAnterior.obtenerFinAtencionCC(mostradorCC.id));
                    }
                }
            }
        }
        private Boolean tieneColaMostradorCC()
        {
            return MostradorCC.tieneCola();
        }

        private MostradorCC buscarMostradorCCLibre()
        {
            foreach (var mostradorCC in mostradoresCC)
            {
                if (mostradorCC.estaLibre()) return mostradorCC;
            }
            return null;
        }

        private Mesa buscarMesaLibre()
        {
            foreach (var mesa in mesas)
            {
                if (mesa.estaLibre()) return mesa;
            }
            return null;
        }
    }
}
