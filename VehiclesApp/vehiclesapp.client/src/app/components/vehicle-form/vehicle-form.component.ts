import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Vehicle } from '../../models/vehicle.model';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent {
  vehicleForm: FormGroup;
  colors: string[] = ['Rojo', 'Azul', 'Verde', 'Negro', 'Blanco'];
  brands: string[] = ['Toyota', 'Ford', 'Chevrolet', 'Honda', 'Nissan'];
  seatsNumbers: number[] = [2, 4, 5, 7];
  years: number[] = Array.from({ length: 20 }, (_, i) => new Date().getFullYear() - i); // Años hacia atrás

  constructor(
    public dialogRef: MatDialogRef<VehicleFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {
    this.vehicleForm = this.fb.group({
      id: [data.vehicle.id ? data.vehicle.id : 0],
      brand: [data.vehicle ? data.vehicle.brand : ''],
      model: [data.vehicle ? data.vehicle.model : ''],
      year: [data.vehicle ? data.vehicle.year : null],
      color: [data.vehicle ? data.vehicle.color : ''],
      seatsNumber: [data.vehicle ? data.vehicle.seatsNumber : ''],
      cylinderCapacity: [data.vehicle ? data.vehicle.cylinderCapacity : ''],
    });
  }

  save(): void {
    console.log(this.vehicleForm.value);
    this.dialogRef.close(this.vehicleForm.value);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
