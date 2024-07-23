#nullable disable
using System.ComponentModel.DataAnnotations;

namespace TaskBack.Models.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, ErrorMessage = "Department Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        public string Logo { get; set; }

        public int? ParentDepartmentId { get; set; }

        public virtual Department ParentDepartment { get; set; }

        public virtual ICollection<Department> SubDepartments { get; set; } = new List<Department>();
    }
}
