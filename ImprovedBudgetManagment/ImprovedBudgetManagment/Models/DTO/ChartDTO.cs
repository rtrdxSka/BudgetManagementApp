namespace ImprovedBudgetManagment.Models.DTO
{
    public class ChartDTO
    {
        public string Name { get; set; }
        public decimal[] Data { get; set; } = new decimal[12];
    }
}
