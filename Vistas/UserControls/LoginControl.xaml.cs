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
namespace Vistas.UserControls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }


        private void btnLoguin(object sender, RoutedEventArgs e)
        {
            Usuario admin = new Usuario(), vendedor = new Usuario();
            admin.Nombre = "santy";
            admin.Password = "santy";
            admin.Rol = "admin";
            vendedor.Nombre = "mayko";
            vendedor.Password = "mayko";
            vendedor.Rol = "vendedor";

            if (txtUser.Text == admin.Nombre && txtPass.Password.ToString() == admin.Password || txtUser.Text == vendedor.Nombre && txtPass.Password.ToString() == vendedor.Password)
            {
                Main main = new Main();
                if (txtUser.Text == admin.Nombre) main.logged = admin;
                else main.logged = vendedor;

                main.validar();
                main.Show();
                Window.GetWindow(this).Close();//metodo para obtener la ventana actual y cerrarla

            }
            else
            {
                txtUser.Text = "";
                txtPass.Password = "";
                txtError.Opacity = 100;
            }
        }
    }
    
}
