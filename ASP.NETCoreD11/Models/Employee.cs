namespace ASP.NETCoreD11.Models
{
    public class Employee
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal Salary { get; set; }
        /*------------------------------------------------------------------*/
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        /*------------------------------------------------------------------*/
    }
}
