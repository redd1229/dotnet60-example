using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet60_example.Entities
{
    [Table("SysRolePermission")]
    [PrimaryKey(nameof(RoleId), nameof(PermissionId))]
    public class RolePermission : BaseEntity
    {
        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }

        [Column("PermissionId", TypeName = "int")]
        public int PermissionId { get; set; }
    }
}
