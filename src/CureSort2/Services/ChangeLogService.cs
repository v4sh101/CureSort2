using CureSort2.Attributes;
using CureSort2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CureSort2.Services
{
    public class ChangeLogService
    {
        public List<ChangeLog> GetChanges(object oldItem, object newItem, string user, int? id)
        {
            List<ChangeLog> logs = new List<ChangeLog>();

            var oldType = oldItem.GetType();
            var newType = newItem.GetType();
            if(oldType != newType)
            {
                return logs;
            }

            var oldProperties = oldType.GetProperties();
            var newProperties = newType.GetProperties();

            var dateChanged = DateTime.UtcNow;
            var primaryKey = (int)id;
            var currentuser = user;
            var className = oldItem.GetType().Name;

            foreach (var oldProperty in oldProperties)
            {
                var matchingProperty = newProperties.Where(x => x.Name == oldProperty.Name && x.PropertyType == oldProperty.PropertyType).FirstOrDefault();
                
                if(!String.IsNullOrEmpty(oldProperty.GetValue(oldItem).ToString()))
                {
                    var oldValue = oldProperty.GetValue(oldItem).ToString();
                    var newValue = matchingProperty.GetValue(newItem).ToString();
                    if (matchingProperty != null && oldValue != newValue)
                    {
                        logs.Add(new ChangeLog()
                        {
                            PrimaryKey = primaryKey,
                            DateChanged = dateChanged,
                            ClassName = className,
                            EditBy = currentuser,
                            PropertyName = matchingProperty.Name,
                            OldValue = oldProperty.GetValue(oldItem).ToString(),
                            NewValue = matchingProperty.GetValue(newItem).ToString()
                        });
                    }
                }
                
            }

            return logs;

        }
    }
}
