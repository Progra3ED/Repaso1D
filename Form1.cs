using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repaso1D
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();
        List<Sueldo> sueldos = new List<Sueldo>();

        public Form1()
        {
            InitializeComponent();
        }

        void CargarEmpleado()
        {
            FileStream stream = new FileStream("Empleado.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            
            while (reader.Peek() > -1)
            {
                Empleado empleado = new Empleado();
                empleado.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                empleado.Nombre = reader.ReadLine();
                empleado.SueldoHora = Convert.ToDecimal(reader.ReadLine());

                empleados.Add(empleado);

                
            }
           
            reader.Close();
        }
        void CargarAsistencia()
        {
            FileStream stream = new FileStream("Asistencia.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Asistencia asistencia = new Asistencia();
                asistencia.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                asistencia.HorasMes = Convert.ToInt16(reader.ReadLine());
                asistencia.Mes = reader.ReadLine();

                asistencias.Add(asistencia);

            }
            
            reader.Close();
        }

        void CargarGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = empleados;
            dataGridView1.Refresh();

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = asistencias;
            dataGridView2.Refresh();

        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            CargarAsistencia();
            CargarEmpleado();
            CargarGrid();

        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < empleados.Count; i++)
            {
                for (int j = 0; j < asistencias.Count; j++)
                {
                    if (empleados[i].NoEmpleado == asistencias[j].NoEmpleado)
                    {
                        Sueldo sueldo = new Sueldo();
                        sueldo.NoEmpleado = empleados[i].NoEmpleado;
                        sueldo.Nombre = empleados[i].Nombre;
                        sueldo.SueldoMes = empleados[i].SueldoHora * asistencias[j].HorasMes;
                        sueldo.Mes = asistencias[j].Mes;

                        sueldos.Add(sueldo);
                    }

                }

            }
            dataGridView3.DataSource = sueldos;
            dataGridView3.Refresh();
        }
    }
}
