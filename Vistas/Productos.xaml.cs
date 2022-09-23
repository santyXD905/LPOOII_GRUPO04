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
    /// Interaction logic for Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
        }

        public void limpiar()
        {
            txtCodigo.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
        }

        public void habilitarText(bool estado)
        {
            txtCodigo.IsEnabled = estado;
            txtCategoria.IsEnabled = estado;
            txtColor.IsEnabled = estado;
            txtDescripcion.IsEnabled = estado;
            txtPrecio.IsEnabled = estado;
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
            MessageBoxResult result = MessageBox.Show("Guardar el producto?", "Alta Producto", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Producto oProducto = new Producto();
                oProducto.CodProducto = txtCodigo.Text;
                oProducto.Categoria = txtCategoria.Text;
                oProducto.Color = txtColor.Text;
                oProducto.Descripcion = txtDescripcion.Text;
                decimal number;
                if (Decimal.TryParse(txtPrecio.Text, out number))
                    oProducto.Precio = decimal.Parse(txtPrecio.Text);
                else
                    oProducto.Precio = 0.0M;
                MessageBox.Show("Codigo: " + oProducto.CodProducto + "\nCategoria: " + oProducto.Categoria + "\nColor: " + oProducto.Color + "\nDescripcion: " + oProducto.Descripcion + "\nPrecio: " + oProducto.Precio);
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
