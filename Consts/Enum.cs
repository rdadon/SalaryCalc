namespace SalaryBE.Consts
{
    public class Enum
    {
        public enum ManagementLevel : short
        {
            NoLevel = 0,
            Level1 = 1,
            Level2 = 2,
            Level3 = 3,
            Level4 = 4
        }

        public enum ProfessionalLevel : short
        {
            Beginner = 0,
            Experienced = 1
        }

        public enum PositionPercentage : int
        {
            FullTime = 100,
            ThreeQuarter = 75,
            HalfTime = 50
        }
    }
}
