using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet60_example.Entities
{
    [Table("SysAccountRole")]
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    public class AccountRole : BaseEntity
    {
        [Column("UserId", TypeName = "nvarchar(10)")]
        [StringLength(10)]
        public string? UserId { get; set; }

        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }
    }
}
