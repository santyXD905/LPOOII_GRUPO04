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
namespace Vistas
{
    /// <summary>
    /// Interoption logic for Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        #region Atributos
        public Venta actual;
        public Cliente cliente;
        public Vendedor vendedor;
        public Producto producto;
        public char option = 'n';
        private ObservableCollection<Venta> lista;
        private CollectionViewSource vista;
        #endregion

        public Ventas()
        {
            InitializeComponent();
        }


        #region Manejo ventana
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnVistaPrevia.Visibility = System.Windows.Visibility.Hidden;
            habilitarForm(false);
            habilitarABM(true);
            habilitarGuarCanc(false);

            ObjectDataProvider odp = this.Resources["LISTA_VENTAS"] as ObjectDataProvider;

            lista = odp.Data as ObservableCollection<Venta>;

            vista = this.Resources["VISTA_VENTAS"] as CollectionViewSource;

            //actual = null;
            //listVentas.SelectedItem = null;
            cmbEstado.SelectedValue = "PENDIENTE";
            //limpiar();
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

        
        #region Botones ABM
        

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Ultimos cambios validacion
            if (miniValidacion() == true)
            {

                MessageBoxResult result = MessageBox.Show(
                   option == 'n' ? "Guardar la venta?" : "Modificar la venta?",
                   option == 'n' ? "Alta Venta" : "Modificacion Venta",
                   MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Venta venta = new Venta();
                    venta.Dni = cliente.Dni.ToString();
                    venta.Estado = cmbEstado.SelectedValue.ToString();
                    venta.Legajo = vendedor.Legajo;
                    venta.CodProducto = producto.CodProducto;
                    venta.Precio = (decimal)producto.Precio;
                    venta.Cantidad = Int32.Parse(txtCantidad.Text);
                    venta.Importe = venta.Precio * venta.Cantidad;
                    if (datePicker1.SelectedDate != null) venta.FechaFactura = datePicker1.SelectedDate.Value;
                    if (option == 'n')
                    {
                        TrabajarVentas.GuardarVenta(venta);
                        venta.NroFactura = TrabajarVentas.GetCurrentIndex();
                        lista.Add(venta);
                    }
                    else
                    {
                        venta.NroFactura = actual.NroFactura;
                        TrabajarVentas.ModificarVenta(venta);
                        lista = TrabajarVentas.TraerVentas();
                        vista.Source = lista;
                    }

                    habilitarABM(true);
                    habilitarForm(false);
                    habilitarGuarCanc(false);
                    abrirVistaPrevia();
                    limpiar();
                }

                
            
                
            }
            else {
                MessageBox.Show("Faltan Campos para guardar la venta", "Atencion", MessageBoxButton.OK, MessageBoxImage.Information);

            }

           
           
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null)
            {
                option = 'u';
                habilitarABM(false);
                habilitarForm(true);
                habilitarGuarCanc(true);
            }
            else MessageBox.Show("Primero seleccione una venta");
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null)
            {
                MessageBoxResult result = MessageBox.Show(
                   "Confirme eliminacion",
                   "Eliminacion Venta", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    TrabajarVentas.EliminarVenta(actual.NroFactura);
                    lista.Remove(actual);
                    if (lista.Count == 0) limpiar();
                }
            }
            else MessageBox.Show("Primero seleccione una venta");
        }

        #endregion

        #region Botones Nuevo y Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarForm(false);
            habilitarABM(true);
            habilitarGuarCanc(false);

            //disabledColors();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            option = 'n';
            //defaultColors();

            btnVistaPrevia.Visibility = System.Windows.Visibility.Hidden;
            listVentas.SelectedItem = null;

            habilitarABM(false);
            habilitarGuarCanc(true);
            habilitarForm(true);
        }
        #endregion

        
        #region Menu Principal
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
        #endregion


        #region Botones seleccionar
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
        #endregion


        #region Navegacion Lista
        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            if (vista.View != null)
            {
                vista.View.MoveCurrentToFirst();
                actual = vista.View.CurrentItem as Venta;
                setTextBoxes(actual);
                btnVistaPrevia.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (vista.View != null)
            {
                vista.View.MoveCurrentToPrevious();
                if (vista.View.IsCurrentBeforeFirst) vista.View.MoveCurrentToLast();
                actual = vista.View.CurrentItem as Venta;
                setTextBoxes(actual);
                btnVistaPrevia.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (vista.View != null)
            {
                vista.View.MoveCurrentToNext();
                if (vista.View.IsCurrentAfterLast) vista.View.MoveCurrentToFirst();
                actual = vista.View.CurrentItem as Venta;
                setTextBoxes(actual);
                btnVistaPrevia.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            if (vista.View != null)
            {
                vista.View.MoveCurrentToLast();
                actual = vista.View.CurrentItem as Venta;
                setTextBoxes(actual);
                btnVistaPrevia.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion


        #region Manejo Form

        private void defaultColors()
        {

            btnSelCliente.Background = Brushes.Green;
            btnSelCliente.Foreground = Brushes.White;

            btnSelProd.Background = Brushes.Green;
            btnSelProd.Foreground = Brushes.White;

            btnSelVendedor.Background = Brushes.Green;
            btnSelVendedor.Foreground = Brushes.White;
        }
        private void disabledColors()
        {
            btnSelCliente.Background = Brushes.White;
            btnSelCliente.Foreground = Brushes.Gray;

            btnSelProd.Background = Brushes.White;
            btnSelProd.Foreground = Brushes.Gray;

            btnSelVendedor.Background = Brushes.White;
            btnSelVendedor.Foreground = Brushes.Gray;
        }

        private void habilitarForm(bool a){
            datePicker1.IsEnabled = a;
            btnSelCliente.IsEnabled = a;
            txtCantidad.IsEnabled = a;
            btnSelProd.IsEnabled = a;
            btnSelVendedor.IsEnabled = a;
            btnGuardar.IsEnabled = a;
            //txtDni.IsEnabled = a;
            //txtProd.IsEnabled = a;
            //txtVendedor.IsEnabled = a;
            cmbEstado.IsEnabled = a;
        }
        private bool miniValidacion() {

            if (txtCantidad.Text != string.Empty && txtDni.Text != "No seleccionado" && datePicker1.SelectedDate != null && 
                txtProd.Text != "No seleccionado" && txtVendedor.Text != "No seleccionado") {
                return true;
            }
            return false;

        }

        private void limpiar()
        {
            actual = new Venta();
            cliente = null;
            producto = null;
            vendedor = null;

            txtCantidad.Text = string.Empty;
            datePicker1.SelectedDate = null;
            btnSelCliente.Content = "Seleccionar";
            btnSelProd.Content = "Seleccionar";
            btnSelVendedor.Content = "Seleccionar";
            txtDni.Text = "No seleccionado";
            txtProd.Text = "No seleccionado";
            txtVendedor.Text = "No seleccionado";

           

        }

        public void setTextBoxes(Venta venta)
        {
            if (venta != null)
            {
                datePicker1.SelectedDate = venta.FechaFactura;
                txtCantidad.Text = venta.Cantidad.ToString();
                cliente = TrabajarCliente.getByDni(Int32.Parse(venta.Dni));
                producto = TrabajarProducto.getByCod(venta.CodProducto);
                vendedor = TrabajarVendedor.getByLegajo(venta.Legajo);

                txtDni.Text = cliente.Dni + ", " + cliente.Apellido + ", " + cliente.Nombre;
                txtProd.Text = producto.CodProducto + ", " + producto.Descripcion;
                txtVendedor.Text = vendedor.Legajo + ", " + vendedor.Apellido + ", " + vendedor.Nombre;
                cmbEstado.SelectedValue = venta.Estado;
            }

        }
        #endregion


        #region Impresion y vista previa
        private void abrirVistaPrevia()
        {
            VistaPreviaVenta ventana = new VistaPreviaVenta();
            ventana.Owner = this;
            ventana.venta = actual;
            ventana.Show();
        }
        private void listVentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            habilitarForm(false);
            habilitarGuarCanc(false);
            habilitarABM(true);
            limpiar();
            actual = listVentas.SelectedItem as Venta;
            if (actual != null)
            {
                setTextBoxes(actual);
                btnVistaPrevia.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            abrirVistaPrevia();
        }
        #endregion


        #region Filtro y Ordenamiento
        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vista != null)
            {
                vista.Filter += filtroEventHandler;
            }
        }

        private void filtroEventHandler(object sender, FilterEventArgs e)
        {
            Venta curr = e.Item as Venta;

            if (txtFiltro != null && !String.IsNullOrEmpty(txtFiltro.Text))
            {
                if (curr.Dni.ToString().StartsWith(txtFiltro.Text, StringComparison.CurrentCultureIgnoreCase)) e.Accepted = true;
                else e.Accepted = false;
            }
        }
        
        private void listSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vista != null)
            {
                vista.SortDescriptions.Clear();
                
                string campo = ((ListBoxItem)listSort.Items[listSort.SelectedIndex]).Content as string;
                //MessageBox.Show(campo);

                if (campo == "Nro") campo = "NroFactura";
                else if (campo == "Fecha") campo = "FechaFactura";
                else if (campo == "Total") campo = "Importe";
                vista.SortDescriptions.Add(new SortDescription(campo, ListSortDirection.Ascending));
            }
        }
        #endregion







    }
}
