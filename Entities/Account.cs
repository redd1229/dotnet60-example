using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnet60_example.Entities
{
    /// <summary>
    /// 帳號
    /// </summary>
    [Table("SysAccount")]
    public class Account : BaseEntity
    {
        [Key]
        [Column("UserId", TypeName = "nvarchar(10)")]
        [StringLength(10)]
        public string? UserId { get; set; }

        [Required]
        [Column("UserName", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string? UserName { get; set; }

        [Column("Email", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Column("D1Name", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string? D1Name { get; set; }

        [Column("D2Name", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string? D2Name { get; set; }

        [Column("D3Name", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string? D3Name { get; set; }

        [Required]
        [Column("IsActive", TypeName = "bit")]
        public bool? IsActive { get; set; }

        #region 多對多關聯
        [JsonIgnore]
        public IList<Role> Roles { get; set; }

        [JsonIgnore]
        public IList<AccountRole> AccountRoles { get; set; }
        #endregion
    }
}
