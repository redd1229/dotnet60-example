using dotnet60_example.Filters;
using dotnet60_example.Service.Interface;
using dotnet60_example.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace dotnet60_example.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [PermissionFilter]
        public IActionResult List()
        {
            try
            {
                ViewData["Title"] = Resources.Language.AccountManagement;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        /// <summary>
        /// 查詢帳號
        /// </summary>
        /// <param name="searchVM"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Search([FromBody] AccountSearchVM searchVM)
        {
            try
            {
                if (searchVM is null)
                {
                    return BadRequest();
                }

                var list = _accountService.SearchAccount(searchVM);
                return PartialView("Datatable", list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"系統錯誤: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 查詢HR帳號
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IActionResult SearchHrAccount([FromBody] string userId)
        {
            try
            {
                if (userId is null)
                {
                    return BadRequest();
                }

                var result = _accountService.SearchHrAccount(userId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResultVM
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="createVM"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Create([FromBody] AccountCreateVM createVM)
        {
            try
            {
                if (createVM is null)
                {
                    return BadRequest();
                }

                var result = _accountService.CreateAccount(createVM);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResultVM
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Delete([FromBody] string userId)
        {
            try
            {
                if (userId is null)
                {
                    return BadRequest();
                }

                var result = _accountService.DeleteAccount(userId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResultVM
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// 修改帳號
        /// </summary>
        /// <param name="editVM"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Edit([FromBody] AccountEditVM editVM)
        {
            try
            {
                if (editVM is null)
                {
                    return BadRequest();
                }

                var result = _accountService.EditAccount(editVM);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResultVM
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// 取得帳號
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAccount([FromBody] string userId)
        {
            try
            {
                if (userId is null)
                {
                    return BadRequest();
                }

                var result = _accountService.GetAccount(userId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResultVM
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Error: {ex.Message}"
                });
            }
        }
    }
}
