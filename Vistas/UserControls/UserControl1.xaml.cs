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
using System.Threading.Tasks;


namespace Vistas.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        // para el hardcode
        public struct usuario
        {
            public string user;
            public string pass;
        }

        public UserControl1()
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
