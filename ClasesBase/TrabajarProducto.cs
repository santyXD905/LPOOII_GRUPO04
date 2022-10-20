using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProducto
    {

        //TraerProducto para el formulario de productos

        public static Producto TraerProducto() { 
            Producto producto = new Producto();
            producto.CodProducto = "";
            producto.Categoria = "";
            producto.Color = "";
            producto.Descripcion = "";
            return producto;
        }

        //TraerProductos que devuelva el Código, Categoría, Color,Descripción, Precio.

        public static DataTable TraerProductos()
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText="SELECT * FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;

        }

        //Obtener primer elemento(para el binding)
        public static Producto TraerPrimerProducto() {
            Producto p1 = new Producto();
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            p1.CodProducto = dt.Rows[0]["codProducto"].ToString();
            p1.Categoria = dt.Rows[0]["categoria"].ToString();
            p1.Color = dt.Rows[0]["color"].ToString();
            p1.Descripcion = dt.Rows[0]["descripcion"].ToString();
            p1.Precio = decimal.Parse(dt.Rows[0]["precio"].ToString());
            return p1;
        }

        //Obtener actual
        public static Producto TraerActual(int index) {

            Producto p1 = new Producto();

            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; // SIN SP-> Text
            cmd.Connection = cnn;

            cmd.CommandText = "ObtenerProducto";

            SqlParameter param = new SqlParameter("@actual", SqlDbType.Int);
            param.Value = index;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            p1.CodProducto = dt.Rows[0]["codProducto"].ToString();
            p1.Categoria = dt.Rows[0]["categoria"].ToString();
            p1.Color = dt.Rows[0]["color"].ToString();
            p1.Descripcion = dt.Rows[0]["descripcion"].ToString();
            p1.Precio = decimal.Parse(dt.Rows[0]["precio"].ToString());

            return p1;
        }
        //Determinar cantidad de productos
        public static int DeterminarCantidadProductos()
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt.Rows.Count;

        }

        //Determinar producto existente

        public static bool DeterminarProductoExistente(string codigo) 
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Producto WHERE codProducto LIKE @codigo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@codigo", codigo);

            //ejecutar la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //llenar el data table
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            else return true;
        }
        //Agregar nuevo producto

        public static void AgregarProducto(Producto producto)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            // crear query

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "AgregarProducto";
            SqlParameter param = new SqlParameter("@codigo", SqlDbType.VarChar);
            param.Value = producto.CodProducto;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@categoria", SqlDbType.VarChar);
            param.Value = producto.Categoria;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@color", SqlDbType.VarChar);
            param.Value = producto.Color;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@descripcion", SqlDbType.VarChar);
            param.Value = producto.Descripcion;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@precio", SqlDbType.Decimal, 16)
            {
                Precision = 16,
                Scale = 2
            };
            param.Value = producto.Precio;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();//abrir conexion

            cmd.ExecuteNonQuery(); //ejecutar transaccion
            cnn.Close(); // cerrar conexion

        }

        //Modificar producto

        public static void ModificarProducto(Producto producto)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Connection = cnn;

            cmd.CommandText = "ModificarProducto";

            SqlParameter param = new SqlParameter("@codigo", SqlDbType.VarChar);
            param.Value = producto.CodProducto;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@categoria", SqlDbType.VarChar);
            param.Value = producto.Categoria;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@color", SqlDbType.VarChar);
            param.Value = producto.Color;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@descripcion", SqlDbType.VarChar);
            param.Value = producto.Descripcion;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@precio", SqlDbType.Decimal);
            param.Value = producto.Precio;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        //Borrar producto

        public static void BorrarProducto(string codigo)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Connection = cnn;

            cmd.CommandText = "EliminarProducto";
            SqlParameter param = new SqlParameter("@codigo", SqlDbType.VarChar);
            param.Value = codigo;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        public static Producto getByCod(string cod)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Producto WHERE codProducto = " + cod;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Producto v = new Producto();

            v.Categoria = dt.Rows[0]["categoria"].ToString();
            v.Color = dt.Rows[0]["color"].ToString();
            v.Descripcion = dt.Rows[0]["descripcion"].ToString();
            v.Precio = Decimal.Parse(dt.Rows[0]["precio"].ToString());
            v.CodProducto = cod;

            return v;

        }
    }
}
