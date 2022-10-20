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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for Clientes.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        public String mode="default";
        #region Attributes
        ObservableCollection<Cliente> listaClientes;
        CollectionViewSource vistaFiltro;
        char option;
        Cliente actual;
        #endregion
        public Clientes()
        {
            InitializeComponent();
            
        }
        #region Ventana
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = this.Resources["LISTA_CLIENTES"] as ObjectDataProvider;
            listaClientes = odp.Data as ObservableCollection<Cliente>;

            vistaFiltro = this.Resources["VISTA_CLIENTES"] as CollectionViewSource;

            txtFiltro.Text = String.Empty;
            habilitarText(false);

            if (mode.Equals("venta"))
            {
                btnSeleccionar.Visibility = System.Windows.Visibility.Visible;
                btnSeleccionar.IsEnabled = false;
            }
            else {
                btnSeleccionar.Visibility = System.Windows.Visibility.Hidden;
            }
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

        //metodos para habilitar y deshabilitar elementos
        #region Utilidades
        public void limpiar()
        {
            txtDni.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        public void habilitarText(bool estado)
        {
            txtDni.IsEnabled = estado;
            txtNombre.IsEnabled = estado;
            txtApellido.IsEnabled = estado;
            txtDireccion.IsEnabled = estado;
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

        public void setTextBoxes(Cliente cli)
        {
            if (cli != null)
            {
                txtApellido.Text = cli.Apellido;
                txtDireccion.Text = cli.Direccion;
                txtDni.Text = cli.Dni;
                txtNombre.Text = cli.Nombre;
            }
            
        }
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            habilitarText(false);
            actual = listView1.SelectedItem as Cliente;
            if (actual != null) { setTextBoxes(actual);
            btnSeleccionar.IsEnabled = true;
            } 


        }
        #endregion



        //metodos para manejar los botones
        #region ABM
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(true);
            habilitarGuarCanc(true);
            habilitarABM(false);
            btnSeleccionar.IsEnabled = false;
            option = 'n';
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null)
            {

                habilitarGuarCanc(true);
                habilitarText(true);
                txtDni.IsEnabled = false;
                btnSeleccionar.IsEnabled = false;
                option = 'u';
            }
            else MessageBox.Show("Seleccione un cliente primero");



        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (actual != null)
            {
                btnSeleccionar.IsEnabled = false;
                MessageBoxResult result = MessageBox.Show(
                    "Confirme eliminacion",
                    "Eliminacion Cliente", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    TrabajarCliente.EliminarCliente(actual.Dni);
                    listaClientes.Remove(actual);
                }
            }
            else MessageBox.Show("Seleccione un cliente primero");
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(option == 'n')
                foreach (Cliente cli in listaClientes)
                {
                    if (cli.Dni == txtDni.Text)
                    {
                        MessageBox.Show("Ya hay un cliente con el DNI ingresado");
                        return;
                    }
                }
            
            
            
            MessageBoxResult result = MessageBox.Show(
                option == 'n'? "Guardar el cliente?":"Modificar el cliente?", 
                option == 'n'? "Alta Cliente":"Modificacion Cliente", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                if (actual == null) actual = new Cliente();
                actual.Dni = txtDni.Text;
                actual.Nombre = txtNombre.Text;
                actual.Apellido = txtApellido.Text;
                actual.Direccion = txtDireccion.Text;

                if (option == 'n')
                {
                    TrabajarCliente.GuardarCliente(actual);
                    listaClientes.Add(actual);
                }
                else
                {
                    TrabajarCliente.ModificarCliente(actual);
                    listaClientes = TrabajarCliente.TraerClientes();
                    vistaFiltro.Source = listaClientes;
                }

                habilitarText(false);
                habilitarGuarCanc(false);
                habilitarABM(true);
                actual = null;
                limpiar();

            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(false);
            habilitarGuarCanc(false);
            habilitarABM(true);
            actual = null;
        }
        #endregion


        #region Barra navegacion

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {

            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToFirst();
                actual = vistaFiltro.View.CurrentItem as Cliente;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }

        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {

            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToPrevious();
                if (vistaFiltro.View.IsCurrentBeforeFirst) vistaFiltro.View.MoveCurrentToLast();
                actual = vistaFiltro.View.CurrentItem as Cliente;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToNext();
                if (vistaFiltro.View.IsCurrentAfterLast) vistaFiltro.View.MoveCurrentToFirst();
                actual = vistaFiltro.View.CurrentItem as Cliente;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToLast();
                actual = vistaFiltro.View.CurrentItem as Cliente;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }
        #endregion


        #region Filtros y ordenamiento

        private void filtroEventHandler(object sender, FilterEventArgs e)
        {
            Cliente curr = e.Item as Cliente;

            if (txtFiltro != null)
            {
                if (curr.Apellido.StartsWith(txtFiltro.Text, StringComparison.CurrentCultureIgnoreCase)) e.Accepted = true;
                else e.Accepted = false;
            }
        }
        private void listSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vistaFiltro != null)
            {
                vistaFiltro.SortDescriptions.Clear();
                string campo = ((ListBoxItem)listSort.Items[listSort.SelectedIndex]).Content as string;
                //MessageBox.Show(campo);
                vistaFiltro.SortDescriptions.Add(new SortDescription(campo, ListSortDirection.Ascending));
            }
        }

        #endregion 

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaFiltro != null)
            {
                vistaFiltro.Filter += filtroEventHandler;
                if (listView1.Items.Count == 0)
                {
                    btnImprimir.IsEnabled = false;

                }
                else  btnImprimir.IsEnabled = true;
            }

        }

       
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            VistaPrevia vistaPrevia = new VistaPrevia();
            ItemCollection ic = listView1.Items;
            ObservableCollection<Cliente> lista = new ObservableCollection<Cliente>();
            foreach (Cliente x in ic)
            {
                lista.Add(x);
            }

            if (lista != null)
            {
                vistaPrevia.listaClientes = lista;
                vistaPrevia.Show();
            }
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
           
            Ventas padre = this.Owner as Ventas;
            padre.btnSelCliente.Content = "Seleccionado";
            padre.btnSelCliente.Background = Brushes.Khaki;
            padre.btnSelCliente.Foreground = Brushes.Black;
            padre.cliente = actual;
            
            this.Close();
        }

        
        


    }
}