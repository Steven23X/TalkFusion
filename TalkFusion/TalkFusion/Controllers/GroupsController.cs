﻿using Microsoft.AspNetCore.Mvc;
using TalkFusion.Data;
using TalkFusion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace TalkFusion.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;

        public GroupsController(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            var groups = from g in db.Groups select g;

            ViewBag.Groups= groups;

            return View();
        }
    }
}
