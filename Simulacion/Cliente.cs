using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7___Comidas_rápidas.Simulacion
{
    class Cliente
    {
        String ESPERANDO_ATENCION_CAJA = "EAC";
        String SIENDO_ATENDIDO_CAJA = "SAC";
        String ESPERANDO_ATENCION_SANDWICH = "EAS";
        String SIENDO_ATENDIDO_SANDWICH = "SAS";
        String ESPERANDO_ATENCION_COMIDA_CALIENTE = "EACC";
        String SIENDO_ATENDIDO_COMIDA_CALIENTE = "SACC";
        String ESPERANDO_MESA_COMIDA_CALIENTE = "EMCC";
        String SENTADO_MESA_COMIDA_CALIENTE = "SMCC";
        String ESPERANDO_MESA_SANDWICH = "EMS";
        String SENTADO_MESA_SANDWICH = "SMS";

        public String estado;
        public double horaLlegadaABar;
        public double tiempoPermanenciaBar;


        public Cliente()
        {
            this.estado = "";
            this.horaLlegadaABar = 0;
            this.tiempoPermanenciaBar = 0;
        }

        public void limpiar()
        {
            estado = "";
            horaLlegadaABar = -1;
            tiempoPermanenciaBar = 0;
        }

        public Boolean estaLibre()
        {
            return this.estado == "";
        }

        public void esperarCaja()
        {
            this.estado = ESPERANDO_ATENCION_CAJA;
        }

        public void esperarAtencionSandwich()
        {
            this.estado = ESPERANDO_ATENCION_SANDWICH;
        }

        public void esperarAtencionComidaCaliente()
        {
            this.estado = ESPERANDO_ATENCION_COMIDA_CALIENTE;
        }

        public void atenderCaja()
        {
            this.estado = SIENDO_ATENDIDO_CAJA;
        }

        public void atenderSandwich()
        {
            this.estado = SIENDO_ATENDIDO_SANDWICH;
        }

        public void atenderComidaCaliente(int numero)
        {
            this.estado = SIENDO_ATENDIDO_COMIDA_CALIENTE + " " + numero;
        }
        
        public void esperandoMesaComidaCaliente() 
        {
            this.estado = ESPERANDO_MESA_COMIDA_CALIENTE;
        }

        public void esperandoMesaSandwich()
        {
            this.estado = ESPERANDO_MESA_SANDWICH;
        }

        public void sentadoMesaConComidaCaliente(int numero) 
        {
            this.estado = SENTADO_MESA_COMIDA_CALIENTE + " " + numero; 
        }
        
        public void sentadoMesaConSandwich(int numero)
        {
            this.estado = SENTADO_MESA_SANDWICH + " " + numero;
        }
    }
}
