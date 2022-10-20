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
using System.Collections.ObjectModel;
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for VistaPrevia.xaml
    /// </summary>
    public partial class VistaPrevia : Window
    {
        public ObservableCollection<Cliente> listaClientes;
        private CollectionViewSource clientes;
        public VistaPrevia()
        {
            
            InitializeComponent();
            clientes = new CollectionViewSource();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (clientes != null) { 
                clientes.Source = listaClientes;
               //se crea un nueva Coneccion
                Binding bind = new Binding();
                bind.Source = clientes;
                //Se asigna a la listView1 Su contenido, y luego especifico el bind
                BindingOperations.SetBinding(listView1, ListView.ItemsSourceProperty,bind);
            }
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if(pd.ShowDialog() == true){
                pd.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir"); 
            }
        }

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
    }
}
