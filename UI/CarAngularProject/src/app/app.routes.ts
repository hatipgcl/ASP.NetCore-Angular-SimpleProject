import { Routes } from '@angular/router';
import { CarListComponent } from './car-list/car-list.component';
import { CarCreateComponent } from './car-create/car-create.component';
import { CarUpdateComponent } from './car-update/car-update.component';

export const routes: Routes = [
    { path: '', redirectTo: '/car-list', pathMatch: 'full' },
    { path: 'car-list', component: CarListComponent },
    { path: 'car-create', component: CarCreateComponent },
    { path: 'update-car/:id', component: CarUpdateComponent },
    
  ];