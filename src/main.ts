import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { EmployeeComponent } from './app/employee/employee.component';
import { importProvidersFrom } from '@angular/core';
//import { bootstrapApplication } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


const routes = [
  { path: '', component: EmployeeComponent }
];

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

  bootstrapApplication(EmployeeComponent, {
  providers: [
    provideRouter(routes),
    importProvidersFrom(HttpClientModule)
  ]
}).catch(err => console.error(err));

