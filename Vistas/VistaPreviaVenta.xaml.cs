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
    /// Interaction logic for VistaPreviaVenta.xaml
    /// </summary>
    public partial class VistaPreviaVenta : Window
    {

        public Venta venta;

        public VistaPreviaVenta()
        {
            InitializeComponent();
        }


        #region Ventana

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //company
            lblCompany.Content = "EL ROBLE";
            lblCalle.Content = "Av. Exodo 345";
            lblCiudad.Content = "San Salvador de Jujuy";
            lblCuit.Content = "30-12345678-6";
            lblIBB.Content = "A-999";

            //venta
            lblNroVenta.Content = "Numero: " + venta.NroFactura.ToString();
            lblFecha.Content = "Fecha: " + venta.FechaFactura.ToString("dd/MM/yyyy ");

            //cliente
            Cliente cliente = TrabajarCliente.getByDni(venta.Dni);
            lblDni.Content = "DNI: " + cliente.Dni;
            lblNombre.Content = "Nombre: " + cliente.Nombre;
            lblApellido.Content = "Apellido: " + cliente.Apellido;

            //vendedor
            Vendedor vend = TrabajarVendedor.getByLegajo(venta.Legajo);
            lblVendedor.Content = "Vendedor: " + vend.Apellido + " " + vend.Nombre;

            //producto
            Producto prod = TrabajarProducto.getByCod(venta.CodProducto);
            lblCant.Content = "Cantidad: " + venta.Cantidad;
            lblPrecio.Content = "Precio: " + venta.Precio;
            lblDesc.Content = "Descripcion: " + prod.Descripcion;
            lblColor.Content = "Color:" + prod.Color;

            //total

            lblTotal.Content = "TOTAL: " + venta.Importe.ToString();

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion


    }
}
