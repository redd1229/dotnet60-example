using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnet60_example.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("SysRole")]
    public class Role : BaseEntity, IEquatable<Role>
    {
        [Key]
        [Column("RoleId", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required]
        [Column("RoleName", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string? RoleName { get; set; }

        [Column("Description", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string? Description { get; set; }

        [Required]
        [Column("IsActive", TypeName = "bit")]
        public bool? IsActive { get; set; }

        #region 多對多關聯
        [JsonIgnore]
        public IList<Account> Accounts { get; set; }

        [JsonIgnore]
        public IList<AccountRole> AccountRoles { get; set; }

        [JsonIgnore]
        public IList<Permission> Permissions { get; set; }

        [JsonIgnore]
        public IList<RolePermission> RolePermissions { get; set; }
        #endregion

        #region override Equals and GetHashCode to compare entity
        public bool Equals(Role other)
        {
            if (other == null) return false;
            return this.RoleId == other.RoleId;
        }

        public override bool Equals(object obj) => Equals(obj as Role);
        public override int GetHashCode() => RoleId.GetHashCode();
        #endregion
    }
}
