using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ClasesBase
{
    public class TrabajarCliente
    {

        public static void GuardarCliente(Cliente cliente)
        {

            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "AgregarCliente";

            SqlParameter param = new SqlParameter("@dni", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Dni;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@nombre", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Nombre;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@apellido", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Apellido;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@direccion", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Direccion;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            
        }


        public static void ModificarCliente(Cliente cliente)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.CommandText = "ModificarCliente";

            SqlParameter param = new SqlParameter("@dni", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Dni;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@nombre", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Nombre;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@apellido", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Apellido;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@direccion", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cliente.Direccion;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        public static void EliminarCliente(string dni)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.CommandText = "EliminarCliente";

            SqlParameter param = new SqlParameter("@dni", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = dni;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        public static ObservableCollection<Cliente> TraerClientes()
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM Cliente";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ObservableCollection<Cliente> lista = new ObservableCollection<Cliente>();

            foreach (DataRow x in dt.Rows)
            {
                lista.Add(new Cliente(x["dni"].ToString(), x["nombre"].ToString(),
                    x["apellido"].ToString(), x["direccion"].ToString()));
            }

            return lista;
        }

        public static Cliente getByDni(string dni)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Cliente WHERE dni LIKE '" + dni + "'";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Cliente v = new Cliente();

            v.Nombre = dt.Rows[0]["nombre"].ToString();
            v.Apellido = dt.Rows[0]["apellido"].ToString();
            v.Direccion = dt.Rows[0]["direccion"].ToString();
            v.Dni = dni;

            return v;

        }

    }
}
