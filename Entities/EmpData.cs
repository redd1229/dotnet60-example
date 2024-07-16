using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet60_example.Entities
{
    /// <summary>
    /// HR資料
    /// </summary>
    [Table("EmpData")]
    public class EmpData
    {
        [Key]
        [Column("Id")]
        public string? Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("D1Name")]
        public string? D1Name { get; set; }

        [Column("D2Name")]
        public string? D2Name { get; set; }

        [Column("D3Name")]
        public string? D3Name { get; set; }

        [Column("OffWork")]
        public string? OffWork { get; set; }

        [Column("Password")]
        public string? Password { get; set; }

        [Column("Email")]
        public string? Email { get; set; }
    }
}
