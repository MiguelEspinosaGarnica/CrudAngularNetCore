using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using VehiclesApp.Server.Models;
using static VehiclesApp.Server.Services.VehicleService;

namespace VehiclesApp.Server.Services
{
    public class VehicleService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "vehicles.json");

        public class PagedResponse<T>
        {
            public IEnumerable<T> Data { get; set; }  // Lista de datos paginados
            public int PageIndex { get; set; }        // Índice de la página actual
            public int PageSize { get; set; }         // Tamaño de página
            public int TotalCount { get; set; }       // Número total de elementos en la colección
            public int TotalPages { get; set; }       // Número total de páginas
            public bool HasNextPage { get; set; }     // Si hay una página siguiente
            public bool HasPreviousPage { get; set; } // Si hay una página anterior

            public PagedResponse(IEnumerable<T> data, int pageIndex, int pageSize, int totalCount)
            {
                Data = data;
                PageIndex = pageIndex;
                PageSize = pageSize;
                TotalCount = totalCount;
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                HasNextPage = PageIndex < TotalPages - 1;
                HasPreviousPage = PageIndex > 0;
            }
        }

        //Get all vehicles without pagination
        public List<Vehicle> GetAllVehicles()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Vehicle>();
            }

            var jsonData = File.ReadAllText(_filePath);

            List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonData) ?? new List<Vehicle>();
            return vehicles;
        }

        //Get all vehicles with pagination
        public PagedResponse<Vehicle> GetAllVehicles(int pageIndex, int pageSize)
        {
            if (!File.Exists(_filePath))
            {
                return new PagedResponse<Vehicle>(new List<Vehicle>(), 0, 0, 0);
            }

            var jsonData = File.ReadAllText(_filePath);

            List<Vehicle> vehiclesRes = JsonConvert.DeserializeObject<List<Vehicle>>(jsonData) ?? new List<Vehicle>();

            vehiclesRes.Reverse();

            var pagedVehicles = vehiclesRes
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            int totalCount = vehiclesRes.Count();

            var pagedResponse = new PagedResponse<Vehicle>(pagedVehicles, pageIndex, pageSize, totalCount);

            return pagedResponse;
        }

        //Get vehicle by ID
        public Vehicle GetVehicleById(int id)
        {
            var vehiclesRes = GetAllVehicles();
            return vehiclesRes.FirstOrDefault(v => v.Id == id) ?? new Vehicle();
        }

        //Add a vehicle
        public void AddVehicle(Vehicle vehicle)
        {
            var vehicles = GetAllVehicles();
            vehicle.Id = vehicles.Count > 0 ? vehicles.Max(v => v.Id) + 1 : 1;
            vehicles.Add(vehicle);
            SaveVehicles(vehicles);
        }

        //Update a vehicle
        public void UpdateVehicle(Vehicle vehicle)
        {
            var vehicles = GetAllVehicles();
            var index = vehicles.FindIndex(v => v.Id == vehicle.Id);
            if (index >= 0)
            {
                vehicles[index] = vehicle;
                SaveVehicles(vehicles);
            }
        }

        //Delete a vehicle
        public void DeleteVehicle(int id)
        {
            var vehicles = GetAllVehicles();
            var vehicleToRemove = vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicleToRemove != null)
            {
                vehicles.Remove(vehicleToRemove);
                SaveVehicles(vehicles);
            }
        }

        //Method to write vehicles on local database
        private void SaveVehicles(List<Vehicle> vehicles)
        {
            var jsonData = JsonConvert.SerializeObject(vehicles, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
