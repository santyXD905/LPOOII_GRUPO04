using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Cliente
    {
       private string dni;

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }
       private string apellido;

      public string Apellido
      {
          get { return apellido; }
          set { apellido = value; }
      }
      private string nombre;

      public string Nombre
      {
          get { return nombre; }
          set { nombre = value; }
      }
      private string direccion;

      public string Direccion
      {
          get { return direccion; }
          set { direccion = value; }
      }

    }
}
