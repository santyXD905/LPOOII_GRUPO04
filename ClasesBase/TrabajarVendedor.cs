using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ClasesBase
{
    public class TrabajarVendedor
    {

        //TraerVendedor para el formulario de vendedor
        public static Vendedor TraerVendedor()
        {
            Vendedor vendedor = new Vendedor("", "", "");
            
            return vendedor;
        }

        //TraerVendedor que devuelva el Legajo, Nombre, Apellido.
        public static ObservableCollection<Vendedor> TraerVendedores()
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM Vendedor";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ObservableCollection<Vendedor> lista = new ObservableCollection<Vendedor>();

            foreach (DataRow x in dt.Rows)
            {
                lista.Add(new Vendedor(x["Legajo"].ToString(), x["Apellido"].ToString(),
                    x["Nombre"].ToString()));
            }

            return lista;
        }

        
        
        

        //Determinar producto existente

        public static bool DeterminarVendedorExistente(string legajo)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Vendedor WHERE legajo LIKE @legajo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@legajo", legajo);

            //ejecutar la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //llenar el data table
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            else return true;
        }

        //Agregar nuevo vendedor

        public static void AgregarVendedor(Vendedor vendedor)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            // crear query

            cmd.CommandType = CommandType.StoredProcedure; // Sin SP -> Text
            cmd.Connection = cnn;

            cmd.CommandText = "AgregarVendedor";
            SqlParameter param = new SqlParameter("@legajo", SqlDbType.VarChar);
            param.Value = vendedor.Legajo;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@nombre", SqlDbType.VarChar);
            param.Value = vendedor.Nombre;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@apellido", SqlDbType.VarChar);
            param.Value = vendedor.Apellido;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();//abrir conexion

            cmd.ExecuteNonQuery(); //ejecutar transaccion
            cnn.Close(); // cerrar conexion
        }

        //Modificar producto

        public static void ModificarVendedor(Vendedor vendedor)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "ModificarVendedor";

            SqlParameter param = new SqlParameter("@legajo", SqlDbType.VarChar);
            param.Value = vendedor.Legajo;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@nombre", SqlDbType.VarChar);
            param.Value = vendedor.Nombre;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@apellido", SqlDbType.VarChar);
            param.Value = vendedor.Apellido;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        //Modificar vendedor

        public static void EliminarVendedor(string codigo)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "EliminarVendedor";
            SqlParameter param = new SqlParameter("@legajo", SqlDbType.VarChar);
            param.Value = codigo;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static Vendedor getByLegajo(string legajo)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Vendedor WHERE legajo = " + legajo;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Vendedor v = new Vendedor(legajo,dt.Rows[0]["apellido"].ToString(),dt.Rows[0]["nombre"].ToString());

            return v;

        }
    }
}
