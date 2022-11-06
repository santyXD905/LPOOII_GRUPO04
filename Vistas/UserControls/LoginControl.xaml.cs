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
using System.Data;
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
            if (txtUser.Text == "" || txtPass.Password == "")
            {
                txtError.Opacity = 100;
                txtError.Text = "*Ingrese sus credenciales";
            }
            else if (TrabajarUsuario.verificarCredenciales(txtUser.Text,txtPass.Password))
            {
                Main main = new Main();
                DataTable dt = TrabajarUsuario.obtenerUsuario(txtUser.Text);

                Usuario user = new Usuario();
                user.Nombre = dt.Rows[0]["nombreUsuario"].ToString();
                user.Password = dt.Rows[0]["password"].ToString();
                user.Rol = dt.Rows[0]["rol"].ToString();

                main.logged = user;

                main.validar();
                main.Show();
                Window.GetWindow(this).Close();//metodo para obtener la ventana actual y cerrarla

            }
            else
            {
                txtError.Text = "*Usuario y/o Contraseña incorrectos";
                txtUser.Text = "";
                txtPass.Password = "";
                txtError.Opacity = 100;
            }
        }
    }
    
}
