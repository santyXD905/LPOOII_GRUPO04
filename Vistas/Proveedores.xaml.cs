using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Proveedores.xaml
    /// </summary>
    public partial class Proveedores : Window
    {
        public Proveedores()
        {
            InitializeComponent();
        }

        public void limpiar()
        {
            txtCuit.Text = string.Empty;
            txtDomicilio.Text = string.Empty;
            txtRazon.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

        public void habilitarText(bool estado)
        {
            txtCuit.IsEnabled = estado;
            txtDomicilio.IsEnabled = estado;
            txtRazon.IsEnabled = estado;
            txtTelefono.IsEnabled = estado;
        }

        public void habilitarGuarCanc(bool estado)
        {
            btnGuardar.IsEnabled = estado;
            btnCancelar.IsEnabled = estado;
        }

        public void habilitarABM(bool estado)
        {
            btnNuevo.IsEnabled = estado;
            btnModificar.IsEnabled = estado;
            btnEliminar.IsEnabled = estado;
            btnAnterior.IsEnabled = estado;
            btnSiguiente.IsEnabled = estado;
            btnPrimero.IsEnabled = estado;
            btnUltimo.IsEnabled = estado;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(true);
            habilitarGuarCanc(true);
            habilitarABM(false);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Guardar el Proveedor?", "Alta Proveedor", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Proveedor oPreveedor = new Proveedor();
                oPreveedor.CUIT = txtCuit.Text;
                oPreveedor.RazonSocial = txtRazon.Text;
                oPreveedor.Domicilio = txtDomicilio.Text;
                oPreveedor.Telefono = txtTelefono.Text;
                MessageBox.Show("CUIT: " + oPreveedor.CUIT + "\nRazon Social: " + oPreveedor.RazonSocial + "\nDomicilio: " + oPreveedor.Domicilio + "\nTelefono: " + oPreveedor.Telefono);
                habilitarText(false);
                habilitarGuarCanc(false);
                habilitarABM(true);

            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(false);
            habilitarGuarCanc(false);
            habilitarABM(true);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
