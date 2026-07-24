using System;
using System.Collections.Generic;
using System.Text;
using VehiculosExpress.Models;
using VehiculosExpress.Repository;

namespace VehiculosExpress.Services
{
    internal class VehiculoService
    {
        private readonly VehiculosRepository _vehiculosRepository;

        public VehiculoService(VehiculosRepository vehiculosRepository)
        {
            _vehiculosRepository = vehiculosRepository;
        }


        public void AgregarVehiculo(Vehiculos vehiculo)
        {
            _vehiculosRepository.AgregarVehiculo(vehiculo);
        }


        public List<Vehiculos> ObtenerVehiculos()
        {
            return _vehiculosRepository.ObtenerVehiculos();

        }

        public void ActualizarVehiculo(Vehiculos vehiculo)
        {
            _vehiculosRepository.ActualizarVehiculo(vehiculo);
        }

        public void EliminarVehiculo(int id)
        {
            _vehiculosRepository.EliminarVehiculo(id);


        }
    }

}
