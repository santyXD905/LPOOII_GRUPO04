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
    /// Interaction logic for Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        #region Atributos
        private Venta venta;
        public Cliente cliente;
        public Vendedor vendedor;
        public Producto producto;
        #endregion

        public Ventas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            habilitarBotones(false);
            habilitarBotones2(false);
            
        }

        #region Botones
        
        private void btnSelCliente_Click(object sender, RoutedEventArgs e)
        {
            Clientes ventana = new Clientes();
            ventana.mode = "venta";
            ventana.Owner = this;
            ventana.Show();
            

        }

        private void btnSelProd_Click(object sender, RoutedEventArgs e)
        {
            Productos ventana = new Productos();
            ventana.mode = "venta";
            ventana.Owner = this;
            ventana.Show();
               
        }

        private void btnSelVendedor_Click(object sender, RoutedEventArgs e)
        {
            Vendedores ventana = new Vendedores();
            ventana.mode = "venta";
            ventana.Owner = this;
            ventana.Show();
            
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Ultimos cambios validacion
            if (miniValidacion() == true)
            {
                venta = new Venta();
                venta.Dni = cliente.Dni;
                venta.Legajo = vendedor.Legajo;
                venta.CodProducto = producto.CodProducto;
                venta.Precio = (decimal)producto.Precio ;
                venta.Cantidad = Int32.Parse(txtCantidad.Text);
                venta.Importe = venta.Precio * venta.Cantidad;
                if (datePicker1.SelectedDate != null) venta.FechaFactura = datePicker1.SelectedDate.Value;

                TrabajarVentas.GuardarVenta(venta);
                venta.NroFactura = TrabajarVentas.GetCurrentIndex();
            
                VistaPreviaVenta ventana = new VistaPreviaVenta();
                ventana.Owner = this;
                ventana.venta = venta;
                ventana.Show();
            }
            else {
                MessageBox.Show("Faltan Campos para guardar la venta", "Atencion", MessageBoxButton.OK, MessageBoxImage.Information);

            }

           
           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarBotones(false);
        }

        

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarBotones(true);

        }
        #endregion

        #region Botones Anashei
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion

        #region Validacion y Habilitado de botones

        private void habilitarBotones (bool a){
            datePicker1.IsEnabled = a;
            btnSelCliente.IsEnabled = a;
            btnSelProd.IsEnabled = a;
            btnSelVendedor.IsEnabled = a;
            btnGuardar.IsEnabled = a;
        }

        private void habilitarBotones2 (bool a){
            /*btnUltimo.Visibility = System.Windows.Visibility.Hidden;
            btnPrimero.Visibility = System.Windows.Visibility.Hidden;
            btnSiguiente.Visibility = System.Windows.Visibility.Hidden;
            btnModificar.Visibility = System.Windows.Visibility.Hidden;
            btnAnterior.Visibility = System.Windows.Visibility.Hidden;
            btnEliminar.Visibility = System.Windows.Visibility.Hidden;*/
            btnUltimo.IsEnabled = a;
            btnPrimero.IsEnabled = a;
            btnSiguiente.IsEnabled = a;
            btnModificar.IsEnabled = a;
            btnAnterior.IsEnabled = a;
            btnEliminar.IsEnabled = a;
            
        }
        private bool miniValidacion() {

            string a = "Seleccionar";
            if (txtCantidad.Text != string.Empty && (string)btnSelCliente.Content != a && datePicker1.SelectedDate != null && btnSelCliente.Content.ToString() != a &&
            btnSelProd.Content.ToString() != a && btnSelVendedor.Content.ToString() != a) {
                return true;
            }
            return false;

        }

        private void limpiar()
        {
            venta = new Venta();
            cliente = null;
            producto = null;
            vendedor = null;

            txtCantidad.Text = string.Empty;
            datePicker1.SelectedDate = null;
            btnSelCliente.Content = "Seleccionar";
            btnSelProd.Content = "Seleccionar";
            btnSelVendedor.Content = "Seleccionar";

            btnSelVendedor.Background = Brushes.Green;
            btnSelProd.Background = Brushes.Green;
            btnSelCliente.Background = Brushes.Green;

            btnSelCliente.Foreground = Brushes.White;
            btnSelProd.Foreground = Brushes.White;
            btnSelVendedor.Foreground = Brushes.White;

        }
        #endregion
    }
}
