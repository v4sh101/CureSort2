using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CureSort2.Controllers
{
    public class MedicalDeviceLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}