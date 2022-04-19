using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Numeros_aleatorios.Colas;
using Numeros_aleatorios.LibreriaSimulacion;

namespace TP7___Comidas_rápidas
{
    public partial class PantallaResultados : Form
    {
        int cantSimulaciones;
        int desde;
        int hasta;
        double tiempoLlegadaClienteA;
        double tiempoLlegadaClienteB;
        double tiempoLlegadaCajaA;
        double tiempoLlegadaCajaB;
        double tiempoAtencionCCA;
        double tiempoAtencionCCB;
        double tiempoAtencionSA;
        double tiempoAtencionSB;
        double tiempoPermCCA;
        double tiempoPermCCB;
        double tiempoPermSA;
        double tiempoPermSB;
        double corteC;
        double paso;
        Truncador truncador = new Truncador(4);

        public PantallaResultados()
        {
            InitializeComponent();
        }

        private void PantallaResultados_Load(object sender, EventArgs e)
        {
            txtCantSimulaciones.Text = "200";
            txtLlegadaClienteA.Text = "1,5";
            txtLlegadaClienteB.Text = "2,5";
            txtLlegadaCajaA.Text = "0,25";
            txtLlegadaCajaB.Text = "0,4166";
            txtAtencionCCA.Text = "2";
            txtAtencionCCB.Text = "6";
            txtAtencionSA.Text = "1,5";
            txtAtencionSB.Text = "3,5";
            txtPermCCA.Text = "10";
            txtPermCCB.Text = "30";
            txtPermSA.Text = "5";
            txtPermSB.Text = "15";
            txtDesde.Text = "0";
            txtHasta.Text = "100";
            txtCorteC.Text = "160";
            txtPaso.Text = "0,001";
        }

        public void mostrarResultados(DataTable resultados)
        {
            this.grdRangoResultados.DoubleBuffered(true);
            grdRangoResultados.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdRangoResultados.DataSource = resultados;
        }

        private void grdRangoResultados_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1;
        }

        private void grdRangoResultados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            grdRangoResultados.ClearSelection();
        }

        private void cerrarVentanas()
        {
            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name != "PantallaResultados")
                    f.Close();
            }
        }

        public void mostrarEstadisticas(int longitudMaxColaCC,
                                        double tiempoPromedioPermanenciaEnBar,
                                        double porcentajeTiempoOcupacionCaja,
                                        double porcentajeTiempoOciosoMostradorSandwich,
                                        double porcentajeClientesAtendidos)
        {
            txtTiempoPromedioPermanenciaEnBar.Text = truncador.truncar(tiempoPromedioPermanenciaEnBar).ToString();
            txtLongitudMaxColaCC.Text = longitudMaxColaCC.ToString();
            txtPorcentajeTiempoOcupacionCaja.Text = truncador.truncar(porcentajeTiempoOcupacionCaja).ToString();
            txtPorcentajeTiempoOciosoMostradorSandwich.Text = truncador.truncar(porcentajeTiempoOciosoMostradorSandwich).ToString();
            txtPorcentajeClientesAtendidos.Text = truncador.truncar(porcentajeClientesAtendidos).ToString();
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            cerrarVentanas();

            grdRangoResultados.DataSource = null;
            cantSimulaciones = int.Parse(txtCantSimulaciones.Text);
            desde = int.Parse(txtDesde.Text);
            hasta = int.Parse(txtHasta.Text);
            tiempoLlegadaClienteA = double.Parse(txtLlegadaClienteA.Text);
            tiempoLlegadaClienteB = double.Parse(txtLlegadaClienteB.Text);
            tiempoLlegadaCajaA = double.Parse(txtLlegadaCajaA.Text);
            tiempoLlegadaCajaB = double.Parse(txtLlegadaCajaB.Text);
            tiempoAtencionCCA = double.Parse(txtAtencionCCA.Text);
            tiempoAtencionCCB = double.Parse(txtAtencionCCB.Text);
            tiempoAtencionSA = double.Parse(txtAtencionSA.Text);
            tiempoAtencionSB = double.Parse(txtAtencionSB.Text);
            tiempoPermCCA = double.Parse(txtPermCCA.Text);
            tiempoPermCCB = double.Parse(txtPermCCB.Text);
            tiempoPermSA = double.Parse(txtPermSA.Text);
            tiempoPermSB = double.Parse(txtPermSB.Text);
            corteC = double.Parse(txtCorteC.Text);
            paso = double.Parse(txtPaso.Text);

            GestorSimulacion gestor = new GestorSimulacion(this);
            if (hasta - desde <= 500)
            {
                gestor.simular(desde, hasta, cantSimulaciones, tiempoLlegadaClienteA, tiempoLlegadaClienteB, tiempoLlegadaCajaA, tiempoLlegadaCajaB,
                               tiempoAtencionCCA, tiempoAtencionCCB, tiempoAtencionSA, tiempoAtencionSB, tiempoPermCCA, tiempoPermCCB, 
                               tiempoPermSA, tiempoPermSB, corteC, paso);
            }
            else
            {
                MessageBox.Show("El rango puede ser hasta 500 filas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
