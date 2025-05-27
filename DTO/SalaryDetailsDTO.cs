namespace SalaryBE.DTO
{
    public class SalaryDetailsDTO
    {
        public double BasicSalary { get; set; }
        public string SeniorityIncrementRatePercent { get; set; }
        public double TotalSeniorityIncrement { get; set; }
        public double AppointmentIncrement { get; set; }
        public double BaseSalaryBeforeRaise { get; set; }
        public string SalaryIncrementRatePercent { get; set; }
        public double TotalSalaryIncrement { get; set; }
        public double BaseSalaryAfterRaise { get; set; }

    }
}
