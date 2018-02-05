using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CureSort2.Models;

namespace CureSort2.Controllers
{
    public class MedicalDeviceLogController : Controller
    {
       public MedicalDeviceLogController(IMedicalDeviceRepository medicaldevicelogs)
        {
            _medicaldevicelogs = medicaldevicelogs;
        }

        public IMedicalDeviceRepository _medicaldevicelogs { get; set; }

        /**[HttpGet]
        public IEnumerable<MedicalDeviceLog> GetAll()
        {
            return _medicaldevicelogs.GetAll();
        }

        [HttpGet("{id}", Name = "GetMedicalDeviceLog")]
        public IActionResult GetById(long id)
        {
            var item = _medicaldevicelogs.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]**/
    }
}