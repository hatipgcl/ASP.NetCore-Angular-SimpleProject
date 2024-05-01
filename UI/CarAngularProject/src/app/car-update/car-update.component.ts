import { Component, OnInit } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../car.service';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-car-update',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './car-update.component.html',
  styleUrl: './car-update.component.scss'
})
export class CarUpdateComponent implements OnInit {
    car: Car = {
      id: 0,
      plaque: '',
      year: 0,
      inspectionDate: new Date(),
      photoPath: ''
    };
    
  constructor(private router: Router,
    private route: ActivatedRoute,
    private carService: CarService,
      ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = +params['id'];
      if (id) {
        this.getCar(id);
      }
    });
  }
  
  getCar(id: number): void {
    this.carService.getCar(id)
      .subscribe(car => this.car = car);
  }

  onSubmit(): void {
    this.carService.updateCar(this.car.id, this.car)
      .subscribe(
        (response) => {
          console.log('Car updated successfully!', response);
          this.router.navigate(['/car-list']);
        },
        (error) => {
          console.error('Error updating car:', error);
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
          
          this.car.photoPath = base64String;
        }
      };
      reader.readAsDataURL(file);
    }
  }
}