using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        // para el hardcode
        public struct usuario
        {
            public string user;
            public string pass;
        }

        public Login()
        {
            InitializeComponent();
        }

        private void btnLoguin(object sender, RoutedEventArgs e)
        {
            usuario admin, vendedor;
            admin.user = "santy";
            admin.pass = "santy";
            vendedor.user = "mayko";
            vendedor.pass = "mayko";

            if (txtUser.Text == admin.user && txtPass.Password.ToString() == admin.pass || txtUser.Text == vendedor.user && txtPass.Password.ToString() == vendedor.pass)
            {
                Main main = new Main();
                if (txtUser.Text == admin.user)
                    main.userLog.tipoUsuer = "admin";
                else
                    main.userLog.tipoUsuer = "vendedor";

                MessageBox.Show("Bienvenido " + txtUser.Text.ToString());

                main.validar();
                main.Show();
                this.Close();
            }
            else
            {
                txtUser.Text = "";
                txtPass.Password = "";
                txtError.Opacity = 100;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState=WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}