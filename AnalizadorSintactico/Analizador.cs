using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AnalizadorSintactico
{
    public partial class Analizador : Form
    {
        List<char> num = new List<char>(new char[] { '0', '1',
        '2', '3', '4', '5', '6', '7', '8', '9'});

        List<char> ope = new List<char>(new char[] { '+', '-',
        '*', '/'});

        List<char> delim = new List<char>(new char[] { '(', ')' });

        DataTable lexToken = new DataTable();

        public Analizador()
        {
            InitializeComponent();
        }

        private void Analizador_Load(object sender, EventArgs e)
        {
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            lexToken.Columns.Add("Elemento", typeof(char));
            lexToken.Columns.Add("Tipo", typeof(string));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (funcionBienEscrita(txtFuncion.Text))
            {
                label2.ForeColor = Color.Green;
                label2.Text = "Validacion: Correcta";
                lexToken.Clear();

                List<char> elementos = txtFuncion.Text.Replace(" ", "").ToCharArray().ToList();

                if (elementos.Count > 0)
                {
                    DataRow fila;

                    foreach (char elemento in elementos)
                    {
                        fila = lexToken.NewRow();

                        if (num.Contains(elemento))
                        {
                            fila["Elemento"] = elemento;
                            fila["Tipo"] = "Numerico";
                        }
                        else if (ope.Contains(elemento))
                        {
                            fila["Elemento"] = elemento;
                            fila["Tipo"] = "Operador";
                        }
                        else if (delim.Contains(elemento))
                        {
                            fila["Elemento"] = elemento;
                            fila["Tipo"] = "Delimitador";
                        }

                        lexToken.Rows.Add(fila);
                    }
                    dgvSalida.DataSource = lexToken;
                    dgvSalida.Refresh();
                }
            
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = "Validacion: Incorrecta";
                dgvSalida.DataSource = null;
                dgvSalida.Refresh();
            }
        }

        private void txtFuncion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private Boolean funcionBienEscrita(String funcion)
        {
            String expresion;
            expresion = "[0-9()|]{1,}(?: {0,}[+\\-\\*\\/] {0,}[0-9()|]{1,}){0,}$";

            if (Regex.IsMatch(funcion, expresion))
            {
                if (Regex.Replace(funcion, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFuncion.Clear();
            lexToken.Clear();
            dgvSalida.DataSource = null;
            dgvSalida.Refresh();
            label2.ResetText();
        }
    }
}
