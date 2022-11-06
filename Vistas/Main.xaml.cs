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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClasesBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Usuario logged;

        public Window proveedores;
        public Window clientes;
        public Window productos;
        public Window productosGR;
        public Window vendedores;
        public Window listaDeEstados;
        public Window actual;

        public Main()
        {
            InitializeComponent();
            
        }
        public void validar()
        {
           if (logged != null && logged.Rol == "vendedor")
               VendedorItem.Visibility = Visibility.Collapsed;
        }

        //botones para cambiar de gestion 
        private void ProveedoresItem_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            proveedores = new Proveedores();
            actual = proveedores;
            actual.Show();
        }


        private void ClientesItem_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            clientes = new Clientes();
            actual = clientes;
            actual.Show();
        }

        private void VendedorItem_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            vendedores = new Vendedores();
            actual = vendedores;
            actual.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ProductosItem_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            productos = new Productos();
            actual = productos;
            actual.Show();
        }

        private void VentasItem_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(logged != null) lblWelcome.Content += " " + logged.Nombre;
            introSong.Play();
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            About ventana = new About();
            ventana.Show();
        }


    }
}
