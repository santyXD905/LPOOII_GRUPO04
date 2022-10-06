using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Cliente : INotifyPropertyChanged
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

      public Cliente(string dni, string nombre, string apellido, string direccion)
      {
          this.Dni = dni;
          this.Direccion = direccion;
          this.Nombre = nombre;
          this.Apellido = apellido;
      }

      public Cliente()
      {
          // TODO: Complete member initialization
      }


      public event PropertyChangedEventHandler PropertyChanged;

      public void Notify(string prop)
      {
          if (this.PropertyChanged != null)
          {
              PropertyChanged(this, new PropertyChangedEventArgs(prop));
          }
      }
    }
}
