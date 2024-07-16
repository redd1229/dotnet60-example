using dotnet60_example.ViewModels;

namespace dotnet60_example.Service.Interface
{
    public interface IAccountService
    {
        /// <summary>
        /// 查詢帳號
        /// </summary>
        /// <param name="searchVM"></param>
        /// <returns></returns>
        IList<AccountVM> SearchAccount(AccountSearchVM searchVM);

        /// <summary>
        /// 查詢帳號
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ResultVM SearchHrAccount(string userId);

        /// <summary>
        /// 依Id取得帳號相關資訊
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ResultVM GetAccount(string userId);

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="createVM"></param>
        /// <returns></returns>
        ResultVM CreateAccount(AccountCreateVM createVM);

        /// <summary>
        /// 註銷帳號
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ResultVM DeleteAccount(string userId);

        /// <summary>
        /// 編輯帳號
        /// </summary>
        /// <param name="editVM"></param>
        /// <returns></returns>
        ResultVM EditAccount(AccountEditVM editVM);
    }
}
