using dotnet60_example.Filters;
using dotnet60_example.Service.Interface;
using dotnet60_example.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace dotnet60_example.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [PermissionFilter]
        public IActionResult List()
        {
            try
            {
                ViewData["Title"] = Resources.Language.RoleManagement;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="searchVM"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Search([FromBody] RoleSearchVM searchVM)
        {
            try
            {
                if (searchVM is null)
                {
                    return BadRequest();
                }

                var list = _roleService.SearchRole(searchVM);
                return PartialView("Datatable", list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"系統錯誤: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="createVM"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Create([FromBody] RoleCreateVM createVM)
        {
            try
            {
                if (createVM is null)
                {
                    return BadRequest();
                }

                var result = _roleService.CreateRole(createVM);
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
        /// 刪除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Delete([FromBody] int roleId)
        {
            try
            {
                var result = _roleService.DeleteRole(roleId);
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
        /// 修改
        /// </summary>
        /// <param name="editVM"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Edit([FromBody] RoleEditVM editVM)
        {
            try
            {
                if (editVM is null)
                {
                    return BadRequest();
                }

                var result = _roleService.EditRole(editVM);
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
        /// 取得角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRole([FromBody] int roleId)
        {
            try
            {
                var result = _roleService.GetRole(roleId);
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
