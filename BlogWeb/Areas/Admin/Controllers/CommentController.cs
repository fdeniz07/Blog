using AutoMapper;
using BlogWeb.Areas.Admin.Models;
using BlogWeb.Helpers.Abstract;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Extensions;
using CoreLayer.Utilities.Results.ComplexTypes;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BlogWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        public CommentController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, ICommentService commentService) : base(userManager, mapper, imageHelper)
        {
            _commentService = commentService;
        }

        [Authorize(Roles = "SuperAdmin,Comment.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _commentService.GetAllByNonDeletedAsync();
            return View(result.Data);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Read")]
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _commentService.GetAllByNonDeletedAsync();
            var commentsResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            });
            return Json(commentsResult);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Read")]
        [HttpGet]
        public async Task<IActionResult> GetDetail(int commentId)
        {
            var result = await _commentService.GetAsync(commentId);
            if (result.ResultStatus==ResultStatus.Success)
            {
                return PartialView("_CommentDetailPartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "SuperAdmin,Comment.Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(int commentId)
        {
            var result = await _commentService.DeleteAsync(commentId, LoggedInUser.UserName);
            var commentResult = JsonSerializer.Serialize(result,new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(commentResult);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Update")]
        [HttpPost]
        public async Task<IActionResult> Approve(int commentId)
        {
            var result = await _commentService.ApproveAsync(commentId, LoggedInUser.UserName);
            var commentResult = JsonSerializer.Serialize(result,new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(commentResult);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int commentId)
        {
            var result = await _commentService.GetCommentUpdateDtoAsync(commentId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CommentUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "SuperAdmin,Comment.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CommentUpdateDto commentUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _commentService.UpdateAsync(commentUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var commentUpdateAjaxModel = JsonSerializer.Serialize(new CommentUpdateAjaxViewModel
                    {
                        CommentDto = result.Data,
                        CommentUpdatePartial = await this.RenderViewToStringAsync("_CommentUpdatePartial", commentUpdateDto)
                    },new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    return Json(commentUpdateAjaxModel);
                }
            }
            var commentUpdateAjaxErrorModel = JsonSerializer.Serialize(new CommentUpdateAjaxViewModel
            {
                CommentUpdatePartial = await this.RenderViewToStringAsync("_CommentUpdatePartial", commentUpdateDto)
            });
            return Json(commentUpdateAjaxErrorModel);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedComments()
        {
            var result = await _commentService.GetAllByDeletedAsync();
            return View(result.Data);

        }

        [Authorize(Roles = "SuperAdmin,Comment.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedComments()
        {
            var result = await _commentService.GetAllByDeletedAsync();
            var comments = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(comments);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int commentId)
        {
            var result = await _commentService.UndoDeleteAsync(commentId, LoggedInUser.UserName);
            var undoDeleteCommentResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(undoDeleteCommentResult);
        }

        [Authorize(Roles = "SuperAdmin,Comment.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int commentId)
        {
            var result = await _commentService.HardDeleteAsync(commentId);
            var hardDeletedCommentResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(hardDeletedCommentResult);
        }
    }
}
