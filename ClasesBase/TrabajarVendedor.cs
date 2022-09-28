using System;
using System.Collections.Generic;
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
            Vendedor vendedor = new Vendedor();
            vendedor.Legajo = "";
            vendedor.Nombre = "";
            vendedor.Apellido = "";
            
            return vendedor;
        }

        //TraerVendedor que devuelva el Legajo, Nombre, Apellido.

        public static DataTable TraerVendedores()
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Vendedor";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;
        }

        //Obtener primer elemento(para el binding)
        public static Vendedor TraerPrimerVendedor()
        {
            Vendedor v1 = new Vendedor();
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM Vendedor";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            v1.Legajo = dt.Rows[0]["Legajo"].ToString();
            v1.Nombre = dt.Rows[0]["Nombre"].ToString();
            v1.Apellido = dt.Rows[0]["Apellido"].ToString();
            
            return v1;
        }
        //Obtener actual
        public static Vendedor TraerActual(int index)
        {

            Vendedor v1 = new Vendedor();

            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Connection = cnn;

            cmd.CommandText = "ObtenerVendedor";

            SqlParameter param = new SqlParameter("@actual", SqlDbType.Int);
            param.Value = index;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            v1.Legajo = dt.Rows[0]["Legajo"].ToString();
            v1.Nombre = dt.Rows[0]["Nombre"].ToString();
            v1.Apellido = dt.Rows[0]["Apellido"].ToString();
            
            return v1;
        }
        //Determinar cantidad de productos
        public static int DeterminarCantidadVendedores()
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Vendedor";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt.Rows.Count;

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

        public static void BorrarVendedor(string codigo)
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
    }
}
