namespace ContactManager.App.Models
{
    public class ContactViewModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
