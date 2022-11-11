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
using Microsoft.Win32;

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

        #region atributes

        ObservableCollection<Producto> listaProductos = new ObservableCollection<Producto>(); // para el manejo de filtros
        CollectionViewSource vistaFiltro = new CollectionViewSource();
        char option; // interpola la opcion del boton guardar entre modificar o agregar un nuevo objeto 
        Producto actual = null; //Producto seleccionado
        public string mode = "default";

        #endregion

        #region manejo de ventana

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
            ObjectDataProvider odp = this.Resources["LISTA_PRODUCTOS"] as ObjectDataProvider;
            listaProductos = odp.Data as ObservableCollection<Producto>;

            vistaFiltro = this.Resources["VISTA_PRODUCTOS"] as CollectionViewSource;

            //txtFiltro.Text = String.Empty;
            habilitarText(false);

            if (mode.Equals("venta"))
            {
                btnSeleccionar.Visibility = System.Windows.Visibility.Visible;
                btnSeleccionar.IsEnabled = listaProductos.Count > 0;
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
                txtCodigo.IsEnabled = false;
                btnSeleccionar.IsEnabled = false;
                option = 'u';
            }
            else MessageBox.Show("Seleccione un Producto primero");

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (actual != null)
            {
                if(TrabajarVentas.BuscarCodProducto(actual.CodProducto))
                {
                    MessageBox.Show("El producto tiene ventas asociadas","Eliminacion Producto");
                    return;
                }
                btnSeleccionar.IsEnabled = false;
                MessageBoxResult result = MessageBox.Show(
                    "Confirme eliminacion",
                    "Eliminacion Producto", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                   TrabajarProducto.EliminarProducto(actual.CodProducto);
                    listaProductos.Remove(actual);
                    if(listaProductos.Count == 0) limpiar();
                }
            }
            else MessageBox.Show("Seleccione un Producto primero");
        }


        #endregion

        #region Guardar_Cancelar

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //se validan los datos del formulario
            if (IsValid(stpPadre) && IsValid(txtImagen))
            {
                //se verifica la unicidad de la clave primaria 
                if (option == 'n')

                    foreach (Producto prd in listaProductos)
                    {
                        if (prd.CodProducto == txtCodigo.Text)
                        {
                            MessageBox.Show("Ya hay un producto ingresado con ese codigo");
                            return;
                        }
                    }

                //se solicita confimacion de la accion a realizar
                MessageBoxResult result = MessageBox.Show(
                option == 'n' ? "Guardar el producto?" : "Modificar el producto?",
                option == 'n' ? "Alta Producto" : "Modificacion Producto",
                MessageBoxButton.YesNo, MessageBoxImage.Question);


                if (result == MessageBoxResult.Yes)
                {
                    Producto prod = new Producto(txtCodigo.Text, txtCategoria.Text, txtColor.Text, txtDescripcion.Text, Convert.ToDecimal(txtPrecio.Text), imgDynamic.Source.ToString());
                    //se realiza la accion y se actualiza las vistas
                    if (option == 'n')
                    {
                        TrabajarProducto.AgregarProducto(prod);
                        listaProductos.Add(prod);
                    }
                    else
                    {
                        TrabajarProducto.ModificarProducto(prod);
                        listaProductos = TrabajarProducto.TraerProductos();
                        vistaFiltro.Source = listaProductos;
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

        #region navegar list_view

        //este metodo maneja la seleccion de modo objeto/detalle 
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            habilitarText(false);
            actual = listView1.SelectedItem as Producto;
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
                actual = vistaFiltro.View.CurrentItem as Producto;
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
                actual = vistaFiltro.View.CurrentItem as Producto;
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
                actual = vistaFiltro.View.CurrentItem as Producto;
                setTextBoxes(actual);
                btnSeleccionar.IsEnabled = true;
            }
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            if (vistaFiltro.View != null)
            {
                vistaFiltro.View.MoveCurrentToLast();
                actual = vistaFiltro.View.CurrentItem as Producto;
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

        public void setTextBoxes(Producto p1)
        {
            if (p1 != null)
            {
                txtCodigo.Text = p1.CodProducto;
                txtCategoria.Text = p1.Categoria;
                txtColor.Text = p1.Color;
                txtDescripcion.Text = p1.Descripcion;
                txtPrecio.Text = p1.Precio.ToString();
                if (!String.IsNullOrEmpty(p1.Imagen))
                {
                    try
                    {
                        imgDynamic.Source = new ImageSourceConverter().ConvertFromString(p1.Imagen) as ImageSource;
                        txtImagen.Text = p1.Imagen;
                    }catch(Exception e)
                    {
                        imgDynamic.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/no-image.png"));
                        txtImagen.Text = "Imagen no encontrada";
                    }
                }
                else
                {
                    imgDynamic.Source = null;
                    txtImagen.Text = "No se proveyo una imagen";
                }
                
            }
           
        }

        public void habilitarText(bool estado)
        {
            txtCodigo.IsEnabled = estado;
            txtCategoria.IsEnabled = estado;
            txtColor.IsEnabled = estado;
            txtDescripcion.IsEnabled = estado;
            txtPrecio.IsEnabled = estado;
            btnImage.IsEnabled = estado;
        }

        public void limpiar()
        {
            
            txtCodigo.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtImagen.Text = string.Empty;
            imgDynamic.Source = null;
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
            Producto curr = e.Item as Producto;

            if (txtFiltro != null)
            {
                //filta por el codigo de producto
                if (curr.CodProducto.StartsWith(txtFiltro.Text, StringComparison.CurrentCultureIgnoreCase)) e.Accepted = true;
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

       
        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            Ventas padre = this.Owner as Ventas;
            padre.producto = actual;
            padre.txtProd.Text = actual.CodProducto + ", " + actual.Descripcion;
            padre.btnSelProd.Content = "Seleccionado";
            padre.btnSelProd.Background = Brushes.Khaki;
            padre.btnSelProd.Foreground = Brushes.Black;
            this.Close();
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileImg = new OpenFileDialog();
            fileImg.Filter = "Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*";
            string pathImg;
            if (fileImg.ShowDialog() == true)
            {
                pathImg = fileImg.FileName;
                imgDynamic.Source = new ImageSourceConverter().ConvertFromString(pathImg) as ImageSource;
                txtImagen.Text = imgDynamic.Source.ToString();
            }
        }
        
       
    }
}
