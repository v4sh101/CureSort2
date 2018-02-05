using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CureSort2.Models
{
    public interface IMedicalDeviceRepository
    {
        void Add(MedicalDeviceLog medicaldevicelog);
        IEnumerable<MedicalDeviceLog> GetAll();
        MedicalDeviceLog Find(int key);
        void Remove(int key);
        void Update(MedicalDeviceLog medicaldevicelog);
    }
}
