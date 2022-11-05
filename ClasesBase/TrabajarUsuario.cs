using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarUsuario
    {

        //Verifica el nombre de usuario y contraseña
        public static bool verificarCredenciales(string nombreUsuario, string password)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Usuario WHERE nombreUsuario = @username AND password = @password";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@username", nombreUsuario);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            return true;
        }
        //Obtener Usuario
        public static DataTable obtenerUsuario(string nombreUsuario)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Usuario WHERE nombreUsuario = @username";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@username", nombreUsuario);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
