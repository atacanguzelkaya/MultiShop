﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Dtos.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]  
    [Route("Admin/Comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        void CommentViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Listesi";
            ViewBag.v0 = "Yorum İşlemleri"; ;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            CommentViewbagList();
            var values = await _commentService.GetAllCommentAsync();
            return View(values);
        }

        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }

        [Route("UpdateComment/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateComment(string id)
        {
            CommentViewbagList();
            var values = await _commentService.GetByIdCommentAsync(id);
            return View(values);
        }
        [Route("UpdateComment/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            await _commentService.UpdateCommentAsync(updateCommentDto);
            return RedirectToAction("Index", "Comment", new { area = "Admin" });
        }
    }
}
