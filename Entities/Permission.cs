using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnet60_example.Entities
{
    /// <summary>
    /// 權限
    /// </summary>
    [Table("SysPermission")]
    public class Permission : BaseEntity
    {
        [Key]
        [Column("PermissionId", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }

        [Required]
        [Column("ControllerName", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string? ControllerName { get; set; }

        [Required]
        [Column("ActionName", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string? ActionName { get; set; }

        [Column("Description", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string? Description { get; set; }

        [Required]
        [Column("IsActive", TypeName = "bit")]
        public bool? IsActive { get; set; }

        #region 多對多關聯
        [JsonIgnore]
        public IList<Role> Roles { get; set; }

        [JsonIgnore]
        public IList<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
