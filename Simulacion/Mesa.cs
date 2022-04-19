using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7___Comidas_rápidas.Simulacion
{
    class Mesa
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";

        public string estado { get; set; }
        public double finPermanencia { get; set; }

        public int id;

        public static Queue<Cliente> colaS;

        public static Queue<Cliente> colaCC;

        public Cliente clienteActual;

        public Mesa(int id)
        {
            this.estado = LIBRE;
            this.id = id;
            colaS = new Queue<Cliente>();
            colaCC = new Queue<Cliente>();
            this.finPermanencia = -1;
        }

        public Boolean estaLibre()
        {
            return this.estado.Equals(LIBRE);
        }

        public void agregarFinPermanencia(double fin)
        {
            this.finPermanencia = fin;
            this.estado = OCUPADO;
        }

        public void liberar()
        {
            this.finPermanencia = -1;
            this.estado = LIBRE;
            this.clienteActual = null;
        }


        public Cliente getClienteActual()
        {
            return this.clienteActual;
        }

        public static Boolean tieneColaSandwich()
        {
            return colaS.Count > 0;
        }

        public static Boolean tieneColaCC()
        {
            return colaCC.Count > 0;
        }

        public Boolean tieneFinPermanencia()
        {
            return this.finPermanencia > 0;
        }

        public static void agregarAColaSandwich(Cliente cliente)
        {
            colaS.Enqueue(cliente);
        }

        public static void agregarAColaComidaCaliente(Cliente cliente)
        {
            colaCC.Enqueue(cliente);
        }

        public static Cliente siguienteClienteSandwich()
        {
            return colaS.Dequeue();
        }

        public static Cliente siguienteClienteComidaCaliente()
        {
            return colaCC.Dequeue();
        }
    }
}
