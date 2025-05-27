import { Component } from '@angular/core';
import { MolsaSalaryCalcService } from '../../services/molsa-salary-calc.service';
import { SalaryDetails } from '../../models/salaryDetails';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: 'employee.component.html',
  styleUrl: 'employee.component.css'
})
export class EmployeeComponent {

  employeeForm: FormGroup;

  salaryResult?: SalaryDetails;

  constructor(private salaryService: MolsaSalaryCalcService, private fb: FormBuilder) {  
    this.employeeForm = this.fb.group({
      PositionPercentage: [1],
      ProfessionalLevel: [false],
      ManagementLevel: 0,
      TotalYearsOfSeniority: [0],
      IsEligibleForWorkAllowance: [false],
      EligibilityGroup: [false]
    });
 
  }   

  calculateSalary() {
    
    
     /*fix string to INT  --*/
    this.employeeForm.value.PositionPercentage = parseInt(this.employeeForm.value.PositionPercentage);
    this.employeeForm.value.ProfessionalLevel = this.employeeForm.value.ProfessionalLevel ? 1: 0;
    this.employeeForm.value.ManagementLevel = parseInt(this.employeeForm.value.ManagementLevel);
    this.employeeForm.value.EligibilityGroup = this.employeeForm.value.EligibilityGroup ? 1: 0;
    
    const formData = this.employeeForm.value;

    console.log('Calculating salary with the following data:', formData);
    /*alert(`Calculating salary with: ${JSON.stringify(formData, null, 2)}`);*/
    
    this.salaryService.getUpdatedSalary(formData).subscribe({
      next: (result: SalaryDetails) => {
        this.salaryResult = result;
        console.log('Salary calculation result:', result);
      },
      error: (err: any) => {
        console.error('Error calculating salary:', err);
      }
    });
    
  } 

}
