using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class Proveedores: ObservableCollection<Proveedor>
    {
        public Proveedores()
        {
            foreach (Proveedor p in TrabajarProveedor.TraerProveedores())
            {
                Add(new Proveedor(p.CUIT,p.RazonSocial,p.Domicilio,p.Telefono));
            }
        }
    }
}
