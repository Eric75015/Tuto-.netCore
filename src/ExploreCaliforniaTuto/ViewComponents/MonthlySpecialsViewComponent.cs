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
        private readonly SpecialsDataContext _specials;
        public MonthlySpecialsViewComponent(SpecialsDataContext specials)
        {
            _specials = specials;

        }
        public IViewComponentResult Invoke()
        {
            var spec = _specials.GetMonthlySpecials();
            return View(spec);
        }
    }
}
