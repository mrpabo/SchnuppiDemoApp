using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace SchnuppiDemoApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public bool IsAdministrator { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public static User FromEntity(SchnuppiDemoApp.Entities.User entity)
        {
            return new User()
            {
                ID = entity.ID,
                Username = entity.Username,
                Name = entity.Name,
                IsAdministrator = entity.IsAdministrator,
                CreatedDate = entity.CreatedDate,
                CreatedUser = entity.CreatedUser,
                ModifiedDate = entity.ModifiedDate,
                ModifiedUser = entity.ModifiedUser,
            };
        }
    }
}
