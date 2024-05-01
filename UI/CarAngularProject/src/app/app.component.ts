import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { HttpClientModule } from '@angular/common/http';
import { CarListComponent } from './car-list/car-list.component';
import { CarCreateComponent } from './car-create/car-create.component';
import { CarUpdateComponent } from './car-update/car-update.component';




@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, HeaderComponent, HttpClientModule,CarListComponent,CarCreateComponent,CarUpdateComponent]
    
})
export class AppComponent {
  title = 'CarAngularProject';

}
