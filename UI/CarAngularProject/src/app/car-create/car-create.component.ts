import { Component } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../car.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-car-create',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './car-create.component.html',
  styleUrl: './car-create.component.scss'
})
export class CarCreateComponent {
  newCar: Car = {
    id: 0,
    plaque: '',
    year: 0,
    inspectionDate: new Date(),
    photoPath: ''
  }; // Yeni araç nesnesi için boş bir Car objesi

  constructor(private carService: CarService,private router: Router) {}

  onSubmit(): void {
    this.carService.addCar(this.newCar).subscribe(
      (response) => {
        console.log('Car added successfully!', response);
        this.router.navigate(['/car-list']);
      },
      (error) => {
        console.error('Error adding car:', error);

      }
    );

    
  }

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    const files: FileList | null = fileInput.files;
  
    if (files && files.length > 0) {
      const file: File = files[0];
      const reader = new FileReader();
        reader.onload = () => {
        const base64String: string | ArrayBuffer | null = reader.result;
        if (typeof base64String === 'string') {
          
          this.newCar.photoPath = base64String;
        }
      };
      reader.readAsDataURL(file);
    }
  }
}