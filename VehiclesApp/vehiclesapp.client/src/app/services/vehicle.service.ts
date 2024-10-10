import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/vehicle.model'; // Asegúrate de que esta ruta sea correcta

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = 'http://localhost:5000/api/Vehicle'; // Cambia la URL según tu API

  constructor(private http: HttpClient) { }

  getAllVehicles(pageIndex: number, pageSize: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  getVehicleById(id: number): Observable<Vehicle> {
    return this.http.get<Vehicle>(`${this.apiUrl}/${id}`);
  }

  createVehicle(vehicle: Vehicle): Observable<Vehicle> {
    console.log(vehicle);
    return this.http.post<Vehicle>(this.apiUrl, vehicle);
  }

  updateVehicle(vehicle: Vehicle): Observable<Vehicle> {
    return this.http.put<Vehicle>(`${this.apiUrl}/${vehicle.id}`, vehicle);
  }

  deleteVehicle(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
