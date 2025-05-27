using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryBE.DTO;
using SalaryBE.Consts;

namespace SalaryBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryCalcController : ControllerBase
    {

        [HttpPost]
        //This method calculates the salary based on the employee's details provided in the request body.
        public ActionResult<SalaryDetailsDTO> CalculateSalary([FromBody] EmployeeDTO employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }

            double baseSalary = 0;
            // Calculate basis salary  (=Yesod)
            //first we determine the base salary based on the position percentage (אחוז מישרה)
            switch (employee.PositionPercentage)
            {
                case (int)Consts.Enum.PositionPercentage.FullTime:
                    baseSalary = 170; 
                    break;
                case (int)Consts.Enum.PositionPercentage.ThreeQuarter:
                    baseSalary = 127.5;
                    break;
                case (int)Consts.Enum.PositionPercentage.HalfTime:
                    baseSalary = 85; 
                    break;
            }

            //Proffesional level multiplier
            double professionalLevelMultiplier = employee.ProfessionalLevel switch
            {
                (int)Consts.Enum.ProfessionalLevel.Beginner => 100,
                (int)Consts.Enum.ProfessionalLevel.Experienced => 120,
                _ => throw new ArgumentException("Invalid professional level!")
            };

            // Calculate old salary -  without ANY additonals increments
            double oldSalary = baseSalary * professionalLevelMultiplier; 

            //Management level multiplier
            double managementLevelMultiplier = employee.ManagementLevel switch
            {
                (int)Consts.Enum.ManagementLevel.NoLevel => 0,
                (int)Consts.Enum.ManagementLevel.Level1 => 20,
                (int)Consts.Enum.ManagementLevel.Level2 => 40,
                (int)Consts.Enum.ManagementLevel.Level3 => 60,
                (int)Consts.Enum.ManagementLevel.Level4 => 80,
                _ => throw new ArgumentException("Invalid management level!")
            };  
            var yesodSalary = ((professionalLevelMultiplier + managementLevelMultiplier) * baseSalary);

            // Calculate years of seniority multiplier (first we truncate the decimal point)
            var vetekIncramentPercentage = ((int)employee.TotalYearsOfSeniority * 1.25);
            var vetek = (((int)employee.TotalYearsOfSeniority) * 0.0125) * yesodSalary;
            double vetek2 = (double)vetek;
            vetek2 = TruncateDouble(vetek2, 2);

            // Calculate eligibleForWorkAllowance
            double workAllowance = 0;
            if (employee.IsEligibleForWorkAllowance)
            {
                // Work allowance multiplier based on eligibility group
                double workAllowanceMultiplier = employee.EligibilityGroup switch
                {
                    1 => 0.01, // 1%
                    2 => 0.005, // 0.5%
                    _ => throw new ArgumentException("Invalid eligibility group!")
                };
                // Calculate work allowance
                 workAllowance = yesodSalary * workAllowanceMultiplier;
            }

            // Calculate basis salary
            double basisSalary = yesodSalary + vetek + workAllowance;

            // Calculate total salary increment 
            double totalSalaryIncrement = 0;
            if (basisSalary < 20000)
            {
                totalSalaryIncrement = basisSalary * (0.015);
            }
            if (basisSalary >= 20000 && basisSalary < 30000)
            {
                totalSalaryIncrement = basisSalary * 0.0125;
            }
            if (basisSalary > 30000)
            {
                totalSalaryIncrement = basisSalary * 0.01;
            }
            // Add the management level increment to the total salary increment
            //Management level multiplier
            double managementLevel = employee.ManagementLevel switch
            {
                (int)Consts.Enum.ManagementLevel.NoLevel => 0,
                (int)Consts.Enum.ManagementLevel.Level1 => 0.001,
                (int)Consts.Enum.ManagementLevel.Level2 => 0.002,
                (int)Consts.Enum.ManagementLevel.Level3 => 0.003,
                (int)Consts.Enum.ManagementLevel.Level4 => 0.004,
                _ => throw new ArgumentException("Invalid management level!")
            };

            totalSalaryIncrement+= (basisSalary * managementLevel);
            managementLevel = managementLevel * 100; // Convert to percentage for display


            // Calculate NEW base salary
            var newBasisSalary = basisSalary + totalSalaryIncrement;

            //build the result object
            var result = new SalaryDetailsDTO
            {
                BasicSalary = yesodSalary,
                SeniorityIncrementRatePercent = vetekIncramentPercentage.ToString() + "%",
                TotalSeniorityIncrement = vetek2,
                AppointmentIncrement = workAllowance,
                BaseSalaryBeforeRaise = basisSalary,
                SalaryIncrementRatePercent = ((1 + managementLevel).ToString()) + "%",
                TotalSalaryIncrement = totalSalaryIncrement,
                BaseSalaryAfterRaise = newBasisSalary
            };

            return Ok(result);
        }

        public static double TruncateDouble(double value, int digits)
        {
            double factor = Math.Pow(10, digits);
            return Math.Truncate(value * factor) / factor;
        }
    }
    }