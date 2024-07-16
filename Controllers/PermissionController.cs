using dotnet60_example.Filters;
using dotnet60_example.Service.Interface;
using dotnet60_example.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace dotnet60_example.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [PermissionFilter]
        public IActionResult List()
        {
            try
            {
                ViewData["Title"] = Resources.Language.PermissionManagement;
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
        public IActionResult Search([FromBody] PermissionSearchVM searchVM)
        {
            try
            {
                if (searchVM is null)
                {
                    return BadRequest();
                }

                var list = _permissionService.SearchPermission(searchVM);
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
        public IActionResult Create([FromBody] PermissionCreateVM createVM)
        {
            try
            {
                if (createVM is null)
                {
                    return BadRequest();
                }

                var result = _permissionService.CreatePermission(createVM);
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
        /// <param name="permissionId"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionFilter]
        public IActionResult Delete([FromBody] int permissionId)
        {
            try
            {
                var result = _permissionService.DeletePermission(permissionId);
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
        public IActionResult Edit([FromBody] PermissionEditVM editVM)
        {
            try
            {
                if (editVM is null)
                {
                    return BadRequest();
                }

                var result = _permissionService.EditPermission(editVM);
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
        /// 取得權限
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetPermission([FromBody] int? permissionId)
        {
            try
            {
                if (permissionId is null)
                {
                    return BadRequest();
                }

                var item = _permissionService.GetPermission((int)permissionId);
                return Json(item);
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
