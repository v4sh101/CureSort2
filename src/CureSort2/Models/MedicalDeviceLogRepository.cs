using CureSort2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Models
{
    public class MedicalDeviceLogRepository :IMedicalDeviceRepository
    {
        private readonly CureContext _context;

        public MedicalDeviceLogRepository(CureContext context)
        {
            _context = context;
        }

        public IEnumerable<MedicalDeviceLog> GetAll()
        {
            return _context.MedicalDeviceLogs.ToList();
        }

        public void Add(MedicalDeviceLog medicaldevicelog)
        {
            _context.MedicalDeviceLogs.Add(medicaldevicelog);
            _context.SaveChanges();
        }

        public MedicalDeviceLog Find(int key)
        {
            return _context.MedicalDeviceLogs.First(t => t.ID == key);
        }

        public void Remove(int key)
        {
            var entity = _context.MedicalDeviceLogs.First(t => t.ID == key);
            _context.MedicalDeviceLogs.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(MedicalDeviceLog medicaldevicelog)
        {
            _context.MedicalDeviceLogs.Update(medicaldevicelog);
            _context.SaveChanges();
        }
    }
}
