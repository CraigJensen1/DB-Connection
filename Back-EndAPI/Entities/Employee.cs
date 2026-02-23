using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back_EndAPI.Entities;

[Table("employee")]
[Index("Email", Name = "employee_email_key", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("employee_id")]
    public Guid EmployeeId { get; set; }

    [Column("first_name")]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(25)]
    public string Phone { get; set; } = null!;

    [Column("job_title")]
    [StringLength(150)]
    public string JobTitle { get; set; } = null!;

    [Column("salary")]
    [Precision(12, 2)]
    public decimal Salary { get; set; }

    [Column("hire_date")]
    public DateOnly HireDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }
}
