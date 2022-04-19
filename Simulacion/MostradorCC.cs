using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7___Comidas_rápidas.Simulacion
{
    class MostradorCC : ICloneable
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";

        public string estado { get; set; }
        public double finAtencionCC { get; set; }

        public int id;

        public static Queue<Cliente> cola;

        public Cliente clienteActual;

        public MostradorCC(int id)
        {
            this.estado = LIBRE;
            this.id = id;
            cola = new Queue<Cliente>();
            this.finAtencionCC = -1;
        }

        public Boolean estaLibre()
        {
            return this.estado.Equals(LIBRE);
        }

        public void agregarFinAtencionCC(double fin)
        {
            this.finAtencionCC = fin;
            this.estado = OCUPADO;
        }

        public void liberar()
        {
            this.finAtencionCC = -1;
            this.estado = LIBRE;
            this.clienteActual = null;
        }

        public object Clone()
        {
            MostradorCC res = new MostradorCC(this.id);
            res.estado = this.estado;
            res.id = this.id;
            res.finAtencionCC = this.finAtencionCC;
            res.clienteActual = this.clienteActual;
            Cliente[] temp = new Cliente[cola.Count];
            cola.CopyTo(temp, 0);
            cola = new Queue<Cliente>(temp);
            return res;
        }

        public Cliente getClienteActual()
        {
            return this.clienteActual;
        }

        public Boolean tieneFinAtencionCC()
        {
            return this.finAtencionCC > 0;
        }

        public static Boolean tieneCola()
        {
            return cola.Count > 0;
        }
        public static int getCola()
        {
            return cola.Count;
        }

        public static void agregarACola(Cliente cliente)
        {
            cola.Enqueue(cliente);
        }

        public static Cliente siguienteCliente()
        {
            return cola.Dequeue();
        }
    }
}
