using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet60_example.Entities
{
    public class BaseEntity
    {
        [Required]
        [StringLength(10)]
        [Column("CreateUser", TypeName = "nvarchar(10)")]
        public string? CreateUser { get; set; }

        [Required]
        [Column("CreateDate", TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [StringLength(10)]
        [Column("UpdateUser", TypeName = "nvarchar(10)")]
        public string? UpdateUser { get; set; }

        [Column("UpdateDate", TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
    }
}
