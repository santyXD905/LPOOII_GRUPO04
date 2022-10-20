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
using System.Data;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        public string mode = "default";
        public Productos()
        {
            InitializeComponent();
            habilitarText(false);
            habilitarGuarCanc(false);
        }

        private int cont = 1;
        private bool bandera = false;
        private Producto oProducto = new Producto();
        #region manejo de bontones
        
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
            bandera = false;
            btnSeleccionar.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(false);
            habilitarGuarCanc(false);
            habilitarABM(true);
        }


        
        #endregion

        #region botones de direcciones

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (cont < TrabajarProducto.DeterminarCantidadProductos())
            {
                cont++;
                Producto p1 = new Producto();
                p1 = TrabajarProducto.TraerActual(cont);
                establecerProducto(p1);
                btnSeleccionar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No hay otro producto adelante");
            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (cont > 1)
            {
                cont--;
                Producto p1 = new Producto();
                p1 = TrabajarProducto.TraerActual(cont);
                establecerProducto(p1);
                btnSeleccionar.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No hay otro producto atras");
            }
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            Producto p1 = new Producto();
            p1 = TrabajarProducto.TraerActual(1);
            establecerProducto(p1);
            cont = 1;
            btnSeleccionar.IsEnabled = true;
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            Producto p1 = new Producto();
            p1 = TrabajarProducto.TraerActual(TrabajarProducto.DeterminarCantidadProductos());
            establecerProducto(p1);
            cont = TrabajarProducto.DeterminarCantidadProductos();
            btnSeleccionar.IsEnabled = true;
        }

        #endregion

        #region alta baja modificacion

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(this))
            {
                if (!bandera)
                {
                    bandera = false;

                    MessageBoxResult result = MessageBox.Show("Guardar el producto?", "Alta Producto", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        if (TrabajarProducto.DeterminarProductoExistente(txtCodigo.Text))
                        {
                            MessageBox.Show("El codigo del producto ya existe, por favor ingrese otro");
                        }
                        else
                        {
                            
                            oProducto.CodProducto = txtCodigo.Text;
                            oProducto.Categoria = txtCategoria.Text;
                            oProducto.Color = txtColor.Text;
                            oProducto.Descripcion = txtDescripcion.Text;
                            oProducto.Precio = decimal.Parse(txtPrecio.Text);
                            MessageBox.Show("Codigo: " + oProducto.CodProducto + "\nCategoria: " + oProducto.Categoria + "\nColor: " + oProducto.Color + "\nDescripcion: " + oProducto.Descripcion + "\nPrecio: " + oProducto.Precio);
                            TrabajarProducto.AgregarProducto(oProducto);
                            habilitarText(false);
                            habilitarGuarCanc(false);
                            habilitarABM(true);
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Modificar el producto?", "Actualizacion de Producto", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Producto oProducto = new Producto();
                        oProducto.CodProducto = txtCodigo.Text;
                        oProducto.Categoria = txtCategoria.Text;
                        oProducto.Color = txtColor.Text;
                        oProducto.Descripcion = txtDescripcion.Text;
                        oProducto.Precio = decimal.Parse(txtPrecio.Text);
                        MessageBox.Show("Codigo: " + oProducto.CodProducto + "\nCategoria: " + oProducto.Categoria + "\nColor: " + oProducto.Color + "\nDescripcion: " + oProducto.Descripcion + "\nPrecio: " + oProducto.Precio);
                        TrabajarProducto.ModificarProducto(oProducto);
                        habilitarText(false);
                        habilitarGuarCanc(false);
                        habilitarABM(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Hay campos incorrectos");
            }

            this.actualizarListProductos();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                habilitarText(true);
                habilitarABM(false);
                habilitarGuarCanc(true);
                txtCodigo.IsEnabled = false;
                bandera = true;
                btnSeleccionar.IsEnabled = false ;
            }
            else
            {
                MessageBox.Show("No hay elemento seleccionado");
            }     

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                btnSeleccionar.IsEnabled = true;
                MessageBoxResult result = MessageBox.Show("Eliminar el producto?", "Borrado Producto", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    TrabajarProducto.BorrarProducto(txtCodigo.Text);
                }
            }
            else
            {
                MessageBox.Show("No hay elemento seleccionado");
            }

            this.actualizarListProductos();

        }

        /*creamos un nuevo binding porque el objetDataProvider no se actualizaba y
         no podiamos enlazar los datos con la propiedad DataContext del lisview*/
        public void actualizarListProductos()
        {
            Binding actualizador = new Binding();
            actualizador.Source = TrabajarProducto.TraerProductos();

            listView1.SetBinding(ListView.ItemsSourceProperty, actualizador);
        }


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

        #endregion

        #region manejo de formulario

        public void establecerProducto(Producto p1)
        {
            oProducto = p1;
            txtCodigo.Text = p1.CodProducto;
            txtCategoria.Text = p1.Categoria;
            txtColor.Text = p1.Color;
            txtDescripcion.Text = p1.Descripcion;
            txtPrecio.Text = p1.Precio.ToString();
        }

        public void limpiar()
        {
            oProducto = null;
            txtCodigo.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dt = listView1.SelectedValue as DataRowView;
            Producto seleccionado = new Producto();
            if (listView1.SelectedIndex != -1)
            {
                cont = listView1.SelectedIndex + 1;
                seleccionado = TrabajarProducto.TraerActual(cont);
                this.establecerProducto(seleccionado);
                btnSeleccionar.IsEnabled = true;
            }
            else
            {
                cont = 1;
                seleccionado = TrabajarProducto.TraerActual(cont);
                this.establecerProducto(seleccionado);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
            if (mode.Equals("venta"))
            {
                btnSeleccionar.Visibility = System.Windows.Visibility.Visible;
                btnSeleccionar.IsEnabled = false;
            }
            else
            {
                btnSeleccionar.Visibility = System.Windows.Visibility.Hidden;
            }
            limpiar();
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            Ventas padre = this.Owner as Ventas;
            padre.producto = oProducto;
            padre.btnSelProd.Content = "Seleccionado";
            padre.btnSelProd.Background = Brushes.Khaki;
            padre.btnSelProd.Foreground = Brushes.Black;
            this.Close();
        }
        
       
    }
}
