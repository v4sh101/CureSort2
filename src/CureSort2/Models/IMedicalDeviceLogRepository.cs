using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Models
{
    public interface IMedicalDeviceLogRepository
    {
        void Add(MedicalDeviceLog medicaldevicelog);
        IEnumerable<MedicalDeviceLog> GetAll();
        MedicalDeviceLog Find(long key);
        void Remove(long key);
        void Update(MedicalDeviceLog medicaldevicelog);
    }
}
