using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Numeros_aleatorios.LibreriaSimulacion;

namespace Numeros_aleatorios.Colas
{
	class RungeKutta
	{
		public double limite;
		public double h;
		public double to;
		public double eo;
		public double a;
		public DataTable tabla;
		public double k1;
		public double k2;
		public double k3;
		public double k4;
		public double ti;
		public double ei;

		public double tiempo160;

		public RungeKutta()
		{

		}

		public void crearTabla()
		{
			tabla = new DataTable();
			tabla.Columns.Add("to");
			tabla.Columns.Add("eo");
			tabla.Columns.Add("k1");
			tabla.Columns.Add("k2");
			tabla.Columns.Add("k3");
			tabla.Columns.Add("k4");
			tabla.Columns.Add("ti+1");
			tabla.Columns.Add("ei+1");
        }


		public double calcularRungeKuttaTiempoCompra(double h, double limite1)
		{
			this.h = h;
			this.to = 0;
			this.eo = 0;
			Truncador truncador = new Truncador(6);

			crearTabla();
			DataRow row;
			do
			{
				row = tabla.NewRow();
				row[0] = truncador.truncar(to);
				row[1] = truncador.truncar(eo);
				k1 = h * (62 * eo + 5);
				row[2] = truncador.truncar(k1);
				k2 = h * (62 * (eo + (k1 / 2)) + 5);
				row[3] = truncador.truncar(k2);
				k3 = h * (62 * (eo + (k2 / 2)) + 5);
				row[4] = truncador.truncar(k3);
				k4 = h * (62 * (eo + k3) + 5);
				row[5] = truncador.truncar(k4);
				ti = to + h;
				row[6] = truncador.truncar(ti);
				ei = eo + ((1.0 / 6.0)) * (k1 + 2 * k2 + 2 * k3 + k4);
				row[7] = truncador.truncar(ei);
				to = ti;
				eo = ei;
				tabla.Rows.Add(row);
			}
			while (eo <= limite1);
			this.tiempo160 = to;
			return to;
		}
	}

}

