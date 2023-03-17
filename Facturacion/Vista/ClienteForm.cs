using Datos;
using Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Vista
{
    public partial class ClienteForm : Form
    {
        public ClienteForm()
        {
            InitializeComponent();
        }

        string tipoOperacion;

        DataTable dt = new DataTable();
        ClienteDB ClienteDB = new ClienteDB();
        Cliente cliente = new Cliente();

        private void HabilitarControles()
        {
            IdentidadTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            TelefonoTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            DireccionTextBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;

            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
        }

        private void DeshabilitarControles()
        {
            IdentidadTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            TelefonoTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            DireccionTextBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;

            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            IdentidadTextBox.Clear();
            NombreTextBox.Clear();
            TelefonoTextBox.Clear();
            CorreoTextBox.Clear();
            DireccionTextBox.Clear();
            EstaActivoCheckBox.Checked = false;

        }

        

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            IdentidadTextBox.Focus();
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(IdentidadTextBox.Text))
                {
                    errorProvider1.SetError(IdentidadTextBox, "Ingrese el numero de Identidad");
                    IdentidadTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese un nombre");
                    NombreTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                cliente.Identidad = IdentidadTextBox.Text;
                cliente.Nombre = NombreTextBox.Text;
                cliente.Telefono = TelefonoTextBox.Text;
                cliente.Correo = CorreoTextBox.Text;
                cliente.Direccion = DireccionTextBox.Text;
                cliente.EstaActivo = EstaActivoCheckBox.Checked;

                bool inserto = ClienteDB.Insertar(cliente);

                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerCliente();
                    MessageBox.Show("Registro Guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro");
                }

            }
            else if (tipoOperacion == "Modificar")
            {
                cliente.Identidad = IdentidadTextBox.Text;
                cliente.Nombre = NombreTextBox.Text;
                cliente.Telefono = TelefonoTextBox.Text;
                cliente.Correo = CorreoTextBox.Text;
                cliente.Direccion = DireccionTextBox.Text;
                cliente.EstaActivo = EstaActivoCheckBox.Checked;

                bool modifico = ClienteDB.Editar(cliente);
                if (modifico)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerCliente();
                    MessageBox.Show("Registro actualizado correctamente");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }
            }

        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            tipoOperacion = "Modificar";
            if (ClienteDataGridView.SelectedRows.Count > 0)
            {
                IdentidadTextBox.Text = ClienteDataGridView.CurrentRow.Cells["Identidad"].Value.ToString();
                NombreTextBox.Text = ClienteDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                TelefonoTextBox.Text = ClienteDataGridView.CurrentRow.Cells["Telefono"].Value.ToString();
                CorreoTextBox.Text = ClienteDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                DireccionTextBox.Text = ClienteDataGridView.CurrentRow.Cells["Direccion"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(ClienteDataGridView.CurrentRow.Cells["EstaActivo"].Value);

                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            TraerCliente();
        }

        private void TraerCliente()
        {
            dt = ClienteDB.DevolverClientes();

            ClienteDataGridView.DataSource = dt;

        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (ClienteDataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    bool elimino = ClienteDB.Eliminar(ClienteDataGridView.CurrentRow.Cells["Identidad"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        TraerCliente();
                        MessageBox.Show("Registro eliminado");
                    }
                    else
                    { MessageBox.Show("No se pudo eliminar el registro"); }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }
    }
}

    
    

