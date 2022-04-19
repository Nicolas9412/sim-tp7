using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7___Comidas_rápidas.Simulacion
{
    class MostradorSandwich: ICloneable
    {
        string LIBRE = "libre";
        string OCUPADO = "ocupado";
        public string estado { get; set; }
        public double finAtencionSandwich { get; set; }

        public Queue<Cliente> cola;

        public Cliente clienteActual;

        public MostradorSandwich()
        {
            this.estado = LIBRE;
            this.finAtencionSandwich = -1;
            this.cola = new Queue<Cliente>();
        }

        public Boolean tieneCola()
        {
            return cola.Count > 0;
        }

        public void agregarFinAtencionSandwich(double tiempo)
        {
            this.finAtencionSandwich = tiempo;
            this.estado = OCUPADO;

        }

        public Boolean tieneFinAtencionSandwich()
        {
            return this.finAtencionSandwich > 0;
        }

        public double obtenerFinAtencionSandwich()
        {
            return finAtencionSandwich;
        }

        public void liberar()
        {
            this.estado = LIBRE;
            this.finAtencionSandwich = -1;
        }


        public Boolean estaOcupado()
        {
            return this.estado.Equals(OCUPADO);
        }

        public object Clone()
        {
            MostradorSandwich res = new MostradorSandwich();
            res.estado = this.estado;
            res.finAtencionSandwich = this.finAtencionSandwich;
            res.clienteActual = this.clienteActual;
            Cliente[] temp = new Cliente[cola.Count];
            cola.CopyTo(temp, 0);
            res.cola = new Queue<Cliente>(temp);
            return res;
        }

        public Cliente siguienteCliente()
        {
            return cola.Dequeue();
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
