using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Producto
    {
        private string codProducto;

        public string CodProducto
        {
            get { return codProducto; }
            set { codProducto = value; }
        }
       private string categoria;

       public string Categoria
       {
           get { return categoria; }
           set { categoria = value; }
       }
       private string color;

       public string Color
       {
           get { return color; }
           set { color = value; }
       }
       private string descripcion;

       public string Descripcion
       {
           get { return descripcion; }
           set { descripcion = value; }
       }
       private decimal precio;

       public decimal Precio
       {
           get { return precio; }
           set { precio = value; }
       }
    }
}
