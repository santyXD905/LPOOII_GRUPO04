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
    /// Interaction logic for Proveedores.xaml
    /// </summary>
    public partial class Proveedores : Window
    {
        public Proveedores()
        {
            InitializeComponent();
        }


        #region Attributes

        ObservableCollection<Proveedor> listaProveedores = new ObservableCollection<Proveedor>(); // para el manejo de filtros
        CollectionViewSource vistaFiltro = new CollectionViewSource();
        char option; // interpola la opcion del boton guardar entre modificar o agregar un nuevo objeto 
        Proveedor actual; //Proveedor seleccionado

        #endregion

        #region manejar_ventana

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //recuperamos los objetos y usamos una arreglo para manejar las actualizaciones y eliminaciones
            ObjectDataProvider odp = this.Resources["LISTA_PROVEEDORES"] as ObjectDataProvider;
            listaProveedores = odp.Data as ObservableCollection<Proveedor>;

            //recuperamos la vista de los recursos y la usamos para manejar navegar por los objetos
            vistaFiltro = this.Resources["VISTA_PROVEEDORES"] as CollectionViewSource;

            habilitarText(false);
        }

        #endregion
        
        #region Nuevo_Moficar_Eliminar

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(true);
            habilitarGuarCanc(true);
            habilitarABM(false);
            option = 'n';
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null)
            {

                habilitarGuarCanc(true);
                habilitarText(true);
                txtCuit.IsEnabled = false;
                option = 'u';
            }
            else MessageBox.Show("Seleccione un Proveedor primero");
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (actual != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Confirme eliminacion",
                    "Eliminacion Proveedor", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show(txtCuit.Text);
                    TrabajarProveedor.EliminarProveedor(txtCuit.Text.ToString());
                    listaProveedores.Remove(actual);
                }
            }
            else MessageBox.Show("Seleccione un Proveedor primero");
        }


        #endregion

        #region Guardar_Cancelar

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(stpPadre))
            {
                //verificamos en caso de guardar un nuevo proveedor que no se repita el CUIT
                if (option == 'n')
                    foreach (Proveedor prov in listaProveedores)
                    {
                        if (prov.CUIT == txtCuit.Text)
                        {
                            MessageBox.Show("Ya hay un Proveedor con el CUIT ingresado");
                            return;
                        }
                    }

                //solicitamos una confirmacion de parte del usuario
                MessageBoxResult result = MessageBox.Show(
                    option == 'n' ? "Guardar el Proveedor?" : "Modificar el Proveedor?",
                    option == 'n' ? "Alta Proveedor" : "Modificacion Proveedor",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);


                if (result == MessageBoxResult.Yes)
                {
                    actual = new Proveedor(txtCuit.Text, txtRazon.Text, txtDomicilio.Text, txtTelefono.Text);
                    if (option == 'n')
                    {

                        TrabajarProveedor.GuardarProveedor(actual);
                        listaProveedores.Add(actual);
                    }
                    else
                    {
                        MessageBox.Show(txtRazon.Text);
                        TrabajarProveedor.ModificarProveedor(actual);
                        //actualizamos la UI
                        listaProveedores = TrabajarProveedor.TraerProveedores();
                        vistaFiltro.Source = listaProveedores;
                    }

                    habilitarText(false);
                    habilitarGuarCanc(false);
                    habilitarABM(true);
                    actual = null;
                    limpiar();

                }
            }
            else
            {
                MessageBox.Show("Hay campos incorrectos");
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

        #region navegar_listView

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            habilitarText(false);
            actual = listView1.SelectedItem as Proveedor;
            if (actual != null) setTextBoxes(actual);
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {

            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToFirst();
                actual = vistaFiltro.View.CurrentItem as Proveedor;
                setTextBoxes(actual);
            }

        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {

            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToPrevious();
                if (vistaFiltro.View.IsCurrentBeforeFirst) vistaFiltro.View.MoveCurrentToLast();
                actual = vistaFiltro.View.CurrentItem as Proveedor;
                setTextBoxes(actual);
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToNext();
                if (vistaFiltro.View.IsCurrentAfterLast) vistaFiltro.View.MoveCurrentToFirst();
                actual = vistaFiltro.View.CurrentItem as Proveedor;
                setTextBoxes(actual);
            }
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToLast();
                actual = vistaFiltro.View.CurrentItem as Proveedor;
                setTextBoxes(actual);
            }
        }

        #endregion

        #region habilitar_deshabilitar_botones

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

        #region habilitar_limpiar_cargar_texboxes

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

        public void setTextBoxes(Proveedor prov)
        {
            if (prov != null)
            {
                txtCuit.Text = prov.CUIT;
                txtRazon.Text= prov.RazonSocial;
                txtDomicilio.Text= prov.Domicilio;
                txtTelefono.Text = prov.Telefono;
            }
        }

        #endregion

        #region filtrado y ordenacion

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaFiltro != null)
            {
                vistaFiltro.Filter += filtroEventHandler;
            }

        }

        private void filtroEventHandler(object sender, FilterEventArgs e)
        {
            Proveedor curr = e.Item as Proveedor;

            if (txtFiltro != null)
            {
                if (curr.Domicilio.StartsWith(txtFiltro.Text, StringComparison.CurrentCultureIgnoreCase)) e.Accepted = true;
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

        //metodo para realizar las validaciones de datos
        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child)) { return false; }
            }

            return true;
        }
    }
}
