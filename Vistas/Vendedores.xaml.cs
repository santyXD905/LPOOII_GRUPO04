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

        private int cont = 1;
        private bool bandera = false;

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

        #region manejo de botones

        public void habilitarText(bool estado)
        {
            txtLegajo.IsEnabled = estado;
            txtNombre.IsEnabled = estado;
            txtApellido.IsEnabled = estado;
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
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            habilitarText(false);
            habilitarGuarCanc(false);
            habilitarABM(true);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLegajo.Text))
            {
                habilitarText(true);
                habilitarABM(false);
                habilitarGuarCanc(true);
                txtLegajo.IsEnabled = false;
                bandera = true;
            }
            else
            {
                MessageBox.Show("No hay elemento seleccionado");
            }
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

        public void establecerVendedor(Vendedor v1)
        {
            txtLegajo.Text = v1.Legajo;
            txtNombre.Text = v1.Nombre;
            txtApellido.Text = v1.Apellido;
        }

        public void limpiar()
        {
            txtLegajo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
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

                    MessageBoxResult result = MessageBox.Show("Guardar el vendedor?", "Alta Vendedor", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        if (TrabajarVendedor.DeterminarVendedorExistente(txtLegajo.Text))
                        {
                            MessageBox.Show("El legajo del vendedor ya existe, por favor ingrese otro");
                        }
                        else
                        {
                            Vendedor oVendedor = new Vendedor();
                            oVendedor.Apellido = txtApellido.Text;
                            oVendedor.Nombre = txtNombre.Text;
                            oVendedor.Legajo = txtLegajo.Text;
                            MessageBox.Show("Legajo: " + oVendedor.Legajo + "\nApellido: " + oVendedor.Apellido + "\nNombre: " + oVendedor.Nombre);
                            TrabajarVendedor.AgregarVendedor(oVendedor);
                            habilitarText(false);
                            habilitarGuarCanc(false);
                            habilitarABM(true);
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Modificar el vendedor?", "Actualizacion de Vendedor", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Vendedor oVendedor = new Vendedor();
                        oVendedor.Apellido = txtApellido.Text;
                        oVendedor.Nombre = txtNombre.Text;
                        oVendedor.Legajo = txtLegajo.Text;
                        MessageBox.Show("Legajo: " + oVendedor.Legajo + "\nApellido: " + oVendedor.Apellido + "\nNombre: " + oVendedor.Nombre);
                        TrabajarVendedor.ModificarVendedor(oVendedor);
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

            this.actualizarListVendedores();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLegajo.Text))
            {
                MessageBoxResult result = MessageBox.Show("Eliminar el vendedor?", "Borrado Vendedor", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    TrabajarVendedor.BorrarVendedor(txtLegajo.Text);
                }
            }
            else
            {
                MessageBox.Show("No hay elemento seleccionado");
            }

            this.actualizarListVendedores();
        }

        public void actualizarListVendedores()
        {
            Binding actualizador = new Binding();
            actualizador.Source = TrabajarVendedor.TraerVendedores();

            listView1.SetBinding(ListView.ItemsSourceProperty, actualizador);
        }


        #endregion

        #region botones de direcciones

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (cont < TrabajarVendedor.DeterminarCantidadVendedores())
            {
                cont++;
                Vendedor v1 = new Vendedor();
                v1 = TrabajarVendedor.TraerActual(cont);
                establecerVendedor(v1);
            }
            else
            {
                MessageBox.Show("No hay otro vendedor adelante");
            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (cont > 1)
            {
                cont--;
                Vendedor v1 = new Vendedor();
                v1 = TrabajarVendedor.TraerActual(cont);
                establecerVendedor(v1);
            }
            else
            {
                MessageBox.Show("No hay otro vendedor atras");
            }
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            Vendedor v1 = new Vendedor();
            v1 = TrabajarVendedor.TraerActual(1);
            establecerVendedor(v1);
            cont = 1;
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            Vendedor v1 = new Vendedor();
            v1 = TrabajarVendedor.TraerActual(TrabajarVendedor.DeterminarCantidadVendedores());
            establecerVendedor(v1);
            cont = TrabajarVendedor.DeterminarCantidadVendedores();
        }

        #endregion

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            DataRowView dt = listView1.SelectedValue as DataRowView;
            Vendedor seleccionado = new Vendedor();
            MessageBox.Show(listView1.SelectedIndex.ToString());
            if (listView1.SelectedIndex != -1)
            {
                cont = listView1.SelectedIndex + 1;
                seleccionado = TrabajarVendedor.TraerActual(cont);
                this.establecerVendedor(seleccionado);
            }
            else
            {
                cont = 1;
                seleccionado = TrabajarVendedor.TraerActual(cont);
                this.establecerVendedor(seleccionado);
            }
            
        }



    }
}
