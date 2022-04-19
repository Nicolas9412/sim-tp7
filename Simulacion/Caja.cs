using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7___Comidas_rápidas.Simulacion
{
    class Caja: ICloneable
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";

        public string estado;
        public double finCompra;

        public Queue<Cliente> cola;

        public Cliente clienteActual;

        public Caja()
        {
            this.estado = LIBRE;
            this.finCompra = -1;
            this.cola = new Queue<Cliente>();
        }

        public void agregarFinCompra(double fin)
        {
            this.estado = OCUPADO;
            finCompra = fin;
            return;
        }
        public Boolean estaOcupada()
        {
            return this.estado.Equals(OCUPADO);
        }
        public Boolean tieneCola()
        {
            return cola.Count > 0;
        }
        public Boolean tieneFinCompra()
        {
            return finCompra != -1;
        }
        public void liberar()
        {
            this.estado = LIBRE;
            this.finCompra = -1;
        }
        public object Clone()
        {
            Caja res = new Caja();
            res.estado = this.estado;
            res.finCompra = this.finCompra;
            res.clienteActual = this.clienteActual;
            Cliente[] temp = new Cliente[cola.Count];
            cola.CopyTo(temp, 0);
            res.cola = new Queue<Cliente>(temp);
            return res;
        }

        public Cliente siguienteCliente()
        {
            return this.cola.Dequeue();
        }

        public Cliente getClienteActual()
        {
            return this.clienteActual;
        }

        public void agregarACola(Cliente cliente)
        {
            cola.Enqueue(cliente);
        }
    }
}
