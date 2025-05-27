import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MolsaSalaryCalcService } from '../services/molsa-salary-calc.service';

@Component({
  selector: 'app-root',
   standalone: true,
  imports: [RouterOutlet],
  template: `<router-outlet></router-outlet>`

})

export class AppComponent {
  title = 'salaryCalc1';


constructor(private calcService: MolsaSalaryCalcService) {}

submitForm() {
  console.log ('I am going to call the web API.... ');
  
  
    }

  }
