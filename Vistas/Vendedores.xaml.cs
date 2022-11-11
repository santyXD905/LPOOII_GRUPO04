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
    /// Interaction logic for Vendedores.xaml
    /// </summary>
    public partial class Vendedores : Window
    {
        
        public Vendedores()
        {
            InitializeComponent();
            habilitarText(false);
            habilitarGuarCanc(false);
        }

        #region Attributes

        ObservableCollection<Vendedor> listaVendedores = new ObservableCollection<Vendedor>(); // para el manejo de filtros
        CollectionViewSource vistaFiltro = new CollectionViewSource();
        char option; // interpola la opcion del boton guardar entre modificar o agregar un nuevo objeto 
        Vendedor actual; //Vendedor seleccionado
        public String mode = "default";

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            habilitarABM(true);
            habilitarGuarCanc(false);
            habilitarText(false);
            ObjectDataProvider odp = this.Resources["LISTA_VENDEDORES"] as ObjectDataProvider;
            listaVendedores = odp.Data as ObservableCollection<Vendedor>;

            vistaFiltro = this.Resources["VISTA_VENDEDORES"] as CollectionViewSource;

            //txtFiltro.Text = String.Empty;

            habilitarText(false);

            if (mode.Equals("venta"))
            {
                btnSeleccionar.Visibility = System.Windows.Visibility.Visible;
                btnSeleccionar.IsEnabled = listaVendedores.Count > 0;
            }
            else
            {
                btnSeleccionar.Visibility = System.Windows.Visibility.Hidden;
            }
            //limpiar();
        }

        #endregion

        #region Nuevo_Modificar_Eliminar
        
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
                txtLegajo.IsEnabled = false;
                btnSeleccionar.IsEnabled = false;
                option = 'u';
            }
            else MessageBox.Show("Seleccione un vendedor primero");
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (actual != null)
            {
                if (TrabajarVentas.BuscarLegajo(actual.Legajo))
                {
                    MessageBox.Show("El vendedor tiene ventas asociadas", "Eliminacion Vendedor");
                    return;
                }
                btnSeleccionar.IsEnabled = false;
                MessageBoxResult result = MessageBox.Show(
                    "Confirme eliminacion",
                    "Eliminacion Vendedor", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    TrabajarVendedor.EliminarVendedor(actual.Legajo);
                    listaVendedores.Remove(actual);
                    if(listaVendedores.Count == 0) limpiar();
                }
            }
            else MessageBox.Show("Seleccione un Vendedor primero");
        }

        #endregion

        #region Guardar_Cancelar

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(stpPadre))
            {
                if (option == 'n')
                    foreach (Vendedor cli in listaVendedores)
                    {
                        if (cli.Legajo == txtLegajo.Text)
                        {
                            MessageBox.Show("Ya hay un Vendedor con el Legajo ingresado");
                            return;
                        }
                    }



                MessageBoxResult result = MessageBox.Show(
                    option == 'n' ? "Guardar el Vendedor?" : "Modificar el Vendedor?",
                    option == 'n' ? "Alta Vendedor" : "Modificacion Vendedor",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);


                if (result == MessageBoxResult.Yes)
                {
                    Vendedor vend = new Vendedor(txtLegajo.Text, txtApellido.Text, txtNombre.Text);

                    if (option == 'n')
                    {
                        TrabajarVendedor.AgregarVendedor(vend);
                        listaVendedores.Add(vend);
                    }
                    else
                    {
                        TrabajarVendedor.ModificarVendedor(vend);
                        listaVendedores = TrabajarVendedor.TraerVendedores();
                        vistaFiltro.Source = listaVendedores;
                    }

                    habilitarText(false);
                    habilitarGuarCanc(false);
                    habilitarABM(true);
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

        #region navegar_listview

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            habilitarText(false);
            actual = listView1.SelectedItem as Vendedor;
            if (actual != null)
            {
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToFirst();
                actual = vistaFiltro.View.CurrentItem as Vendedor;
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
                actual = vistaFiltro.View.CurrentItem as Vendedor;
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
                actual = vistaFiltro.View.CurrentItem as Vendedor;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToLast();
                actual = vistaFiltro.View.CurrentItem as Vendedor;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }

        #endregion

        #region Habilitar_Deshabilitar_Botones

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

        #region Habilitar_Limpiar_Cargar_Textboxes

        public void habilitarText(bool estado)
        {
            txtLegajo.IsEnabled = estado;
            txtNombre.IsEnabled = estado;
            txtApellido.IsEnabled = estado;
        }

        public void setTextBoxes(Vendedor v1)
        {
            txtLegajo.Text = v1.Legajo;
            txtNombre.Text = v1.Nombre;
            txtApellido.Text = v1.Apellido;
            actual = v1;
        }

        public void limpiar()
        {
            txtLegajo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            actual = null;
        }

        #endregion

        #region Filtros y ordenamiento

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaFiltro != null)
            {
                vistaFiltro.Filter += filtroEventHandler;

                //controlamos la opcion de imprimir para que no se pueda imprimir una lista vacia 
                /*
                if (listView1.Items.Count == 0)
                {
                    btnImprimir.IsEnabled = false;

                }
                else btnImprimir.IsEnabled = true;
                 */
            }

        }

        private void filtroEventHandler(object sender, FilterEventArgs e)
        {
            Vendedor curr = e.Item as Vendedor;

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

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            Ventas padre = this.Owner as Ventas;
            padre.vendedor =actual;
            padre.txtVendedor.Text = actual.Legajo + ", " + actual.Apellido + ", " + actual.Nombre;
            padre.btnSelVendedor.Content = "Seleccionado";
            padre.btnSelVendedor.Background = Brushes.Khaki;
            padre.btnSelVendedor.Foreground = Brushes.Black;
            this.Close();
        }

       

        

    }
}
