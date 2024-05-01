import { Component, OnInit } from '@angular/core';
import { Car } from '../models/car';
import { CarService } from '../car.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-list',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './car-list.component.html',
  styleUrl: './car-list.component.scss'
})
export class CarListComponent implements OnInit {
  cars: Car[]=[];

  constructor(private carService: CarService,private router: Router) { }

  ngOnInit(): void {
    this.getCars();
  }

  getCars(): void {
    this.carService.getCars()
      .subscribe(cars => this.cars = cars);
  }
  goToCreateCar(): void {
    this.router.navigateByUrl('/car-create');
  }

  goToUpdateCar(id: number): void {
    this.router.navigateByUrl(`/update-car/${id}`);
  }

  deleteCar(id: number): void {
    if (confirm('Are you sure you want to delete this car?')) {
      this.carService.deleteCar(id)
        .subscribe(() => {
          this.getCars();
        });
    }
  }

}
