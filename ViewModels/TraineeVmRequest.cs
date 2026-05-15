namespace MVC02.ViewModels
{
    public class TraineeVmRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? ImageUrl { get; set; }
        public int Grade { get; set; }
        public int DeptId { get; set; }
    }
}
