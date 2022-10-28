using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Cliente : INotifyPropertyChanged , IDataErrorInfo
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

        
    //implementacion de validacion de datos

      public string Error
      {
          get { throw new NotImplementedException(); }
      }

      public string this[string columnName]
      {
          get
          {
              string msg_error = null;
              switch (columnName)
              {
                  case "Dni":
                      msg_error = verificarDni();
                      break;
                  case "Nombre":
                      msg_error = verificarNombre();
                      break;
                  case "Apellido":
                      msg_error = verificarApellido();
                      break;
                  case "Direccion":
                      msg_error = verificarDireccion();
                      break;
              }
              return msg_error;
          }
      }

      //AÑADIR MÁS VALIDACIONES

      private string verificarDni()
      {
          if (String.IsNullOrEmpty(Dni))
          {
              return "El DNI es obligatorio";
          }
          return null;
      }

      private string verificarNombre()
      {
          if (String.IsNullOrEmpty(Nombre))
          {
              return "El nombre es obligatorio";
          }
          return null;
      }

      private string verificarApellido()
      {
          if (String.IsNullOrEmpty(Apellido))
          {
              return "El Apellido es obligatorio";
          }
          return null;
      }

      private string verificarDireccion()
      {
          if (String.IsNullOrEmpty(Direccion))
          {
              return "La Direccion es obligatoria";
          }
          return null;
      }

    }
}
