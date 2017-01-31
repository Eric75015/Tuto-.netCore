using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCaliforniaTuto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCaliforniaTuto.ViewComponents
{
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        //private readonly SpecialsDataContext _specials;
        //public MonthlySpecialsViewComponent(SpecialsDataContext specials)
        //{
        //    _specials = specials;

        //}
        private readonly BlogDataContext _db;
        public MonthlySpecialsViewComponent(BlogDataContext db)
        {
            _db = db;

        }
        public IViewComponentResult Invoke()
        {
            var spec = _db.Specials.ToArray();
            return View(spec);
        }
    }
}
