import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VehicleService } from '../../services/vehicle.service';
import { Vehicle } from '../../models/vehicle.model'; // Ajusta la ruta según tu estructura de carpetas
import { VehicleFormComponent } from '../vehicle-form/vehicle-form.component';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource<Vehicle>();
  displayedColumns: string[] = ['brand', 'model', 'year', 'color', 'seatsNumber', 'cylinderCapacity','actions'];
  pageSize = 15; // Tamaño de página por defecto
  totalItems = 0; // Total de vehículos

  constructor(
    private vehicleService: VehicleService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadVehicles(0, this.pageSize); // Carga la lista de vehículos al inicializar el componente
  }

  loadVehicles(pageIndex: number, pageSize: number): void {
    console.log(pageIndex, pageSize);
    this.vehicleService.getAllVehicles(pageIndex, pageSize).subscribe(data => {
      this.dataSource.data = data.data; // Asignar los datos de la página actual
      this.totalItems = data.totalCount;
    });
  }

  addVehicle(): void {
    const dialogRef = this.dialog.open(VehicleFormComponent, {
      width: '300px',
      data: { vehicle: {} }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.vehicleService.createVehicle(result).subscribe(() => {
          this.snackBar.open('Vehicle added successfully', 'Close', { duration: 2000 });
          this.paginator.pageIndex = 0;
          this.loadVehicles(0, this.pageSize);
        });
      }
    });
  }

  editVehicle(vehicle: Vehicle): void {
    const dialogRef = this.dialog.open(VehicleFormComponent, {
      width: '300px',
      data: { vehicle }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.vehicleService.updateVehicle(result).subscribe(() => {
          this.snackBar.open('Vehicle updated successfully', 'Close', { duration: 2000 });
          this.paginator.pageIndex = 0;
          this.loadVehicles(0, this.pageSize);
        });
      }
    });
  }

  deleteVehicle(id: number): void {
    this.vehicleService.deleteVehicle(id).subscribe(() => {
      this.snackBar.open('Vehicle successfully removed', 'Close', { duration: 2000 });
      this.paginator.pageIndex = 0;
      this.loadVehicles(0, this.pageSize);
    });
  }

  onPageChange(event: any) {

    const pageIndex = event.pageIndex;
    const pageSize = event.pageSize;
    this.loadVehicles(pageIndex, pageSize); // Cargar los datos de la nueva página
  }
}
