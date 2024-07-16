using dotnet60_example.ViewModels;

namespace dotnet60_example.Service.Interface
{
    public interface IPermissionService
    {
        /// <summary>
        /// 查詢權限
        /// </summary>
        /// <param name="searchVM"></param>
        /// <returns></returns>
        IList<PermissionVM> SearchPermission(PermissionSearchVM searchVM);

        /// <summary>
        /// 依Id取得權限相關資訊
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        ResultVM GetPermission(int permissionId);

        /// <summary>
        /// 新增權限
        /// </summary>
        /// <param name="createVM"></param>
        /// <returns></returns>
        ResultVM CreatePermission(PermissionCreateVM createVM);

        /// <summary>
        /// 刪除權限
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        ResultVM DeletePermission(int permissionId);

        /// <summary>
        /// 編輯權限
        /// </summary>
        /// <param name="editVM"></param>
        /// <returns></returns>
        ResultVM EditPermission(PermissionEditVM editVM);
    }
}
