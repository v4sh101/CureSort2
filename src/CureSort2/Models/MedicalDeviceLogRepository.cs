using CureSort2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Models
{
    public class MedicalDeviceLogRepository : IMedicalDeviceLogRepository
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

        public MedicalDeviceLog Find(long key)
        {
            return _context.MedicalDeviceLogs.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = _context.MedicalDeviceLogs.First(t => t.Key == key);
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
