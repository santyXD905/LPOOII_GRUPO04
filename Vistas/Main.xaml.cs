﻿using System;
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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public struct usuarioLog
        { 
            public string tipoUsuer;
        }
        public usuarioLog userLog;

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
           if (userLog.tipoUsuer == "vendedor")
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

        private void ProductosAmb_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            productos = new Productos();
            actual = productos;
            actual.Show();
        }

        private void ProductosGrilla_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            productosGR = new ProductosGrilla();
            actual = productosGR;
            actual.Show();
        }

        private void VendedorItem_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            vendedores = new Vendedores();
            actual = vendedores;
            actual.Show();
        }

        private void FacturaItem_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null) actual.Close();
            listaDeEstados = new ListaDeEstados();
            actual = listaDeEstados;
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


    }
}