using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Producto : IDataErrorInfo
    {

        public Producto(string cod,string cat,string col, string des, decimal pres, string img)
        {
            this.codProducto = cod;
            this.categoria = cat;
            this.color = col;
            this.descripcion = des;
            this.precio = pres;
            this.imagen = img;
        }

        public override string ToString()
        {
            return codProducto.ToString() + ", " + precio.ToString();
        }

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

       private string imagen;

       public string Imagen
       {
           get { return imagen; }
           set { imagen = value; }
       }

       private decimal? precio;

       public decimal? Precio
       {
           get { return precio; }
           set { precio = value; }
       }

       public string Error
       {
           get { throw new NotImplementedException(); }
       }

       public string this[string columnName]
       {
           get {
               string msg_error = null;
               switch(columnName){
                    case "CodProducto":
                       msg_error = verificarCodProducto();
                       break;
                    case "Categoria":
                       msg_error = verificarCategoria();
                       break;
                    case "Color":
                       msg_error = verificarColor();
                       break;
                    case "Descripcion":
                       msg_error = verificarDescripcion();
                       break;
                    case "Imagen":
                       msg_error = verificarImagen();
                       break;
                    case "Precio":
                       msg_error = verificarPrecio();
                       break;
               }
               return msg_error;
           }
       }
       private string verificarCodProducto() {
           if (String.IsNullOrEmpty(CodProducto)) { 
                return "El codigo es obligatorio";
           }
           return null;
       }
       private string verificarCategoria()
       {
           if (String.IsNullOrEmpty(Categoria))
           {
               return "La categoria es obligatoria";
           }
           return null;
       }
       private string verificarColor()
       {
           if (String.IsNullOrEmpty(Color))
           {
               return "El color es obligatorio";
           }
           return null;
       }
       private string verificarDescripcion()
       {
           if (String.IsNullOrEmpty(Descripcion))
           {
               return "La descripcion es obligatoria";
           }
           return null;
       }
       private string verificarPrecio()
       {
           if (Precio < 0)
           {
               return "No debe ser menor";
           }
           return null;
       }
       private string verificarImagen()
       {
           if (String.IsNullOrEmpty(Imagen))
           {
               return "La imagen es obligatoria";
           }
           return null;
       }
    }
}
