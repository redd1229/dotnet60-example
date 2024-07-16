using dotnet60_example.ViewModels;

namespace dotnet60_example.Service.Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 查詢角色
        /// </summary>
        /// <param name="searchVM"></param>
        /// <returns></returns>
        IList<RoleVM> SearchRole(RoleSearchVM searchVM);

        /// <summary>
        /// 依Id取得角色相關資訊
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        ResultVM GetRole(int roleId);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="createVM"></param>
        /// <returns></returns>
        ResultVM CreateRole(RoleCreateVM createVM);

        /// <summary>
        /// 刪除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        ResultVM DeleteRole(int roleId);

        /// <summary>
        /// 編輯角色
        /// </summary>
        /// <param name="editVM"></param>
        /// <returns></returns>
        ResultVM EditRole(RoleEditVM editVM);
    }
}
