using SchnuppiDemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchnuppiDemoApp.Models;

namespace SchnuppiDemoApp.Managers
{
    public class UsersManager
    {
        public UsersManager() { }

        #region CRUD

        public List<Models.User> GetUsers()
        {
            using (var db = new SchnuppiEntities())
            {
                return db.Users.Select(x => Models.User.FromEntity(x)).ToList();
            }
        }

        public Models.User GetUser(int id)
        {
            using (var db = new SchnuppiEntities())
            {
                Entities.User userEntity = db.Users.Find(id);

                return Models.User.FromEntity(userEntity);
            }
        }

        public Models.User AddUser(string name, string password, string userName, bool isAdministrator = false)
        {
            using (var db = new SchnuppiEntities())
            {
                var newUser = new Entities.User()
                {
                    Name = name,
                    Password = password,
                    Username = userName,
                    IsAdministrator = isAdministrator,
                    CreatedDate = DateTime.Now,
                    CreatedUser = Environment.UserName
                };

                Entities.User createdUser = db.Users.Add(newUser);

                db.SaveChanges();

                return Models.User.FromEntity(createdUser);
            }
        }

        public Models.User UpdateUser(int id, string name, string userName)
        {
            using (var db = new SchnuppiEntities())
            {
                var userToUpdate = db.Users.Find(id);
                if (userToUpdate == null)
                {
                    return null;
                }

                userToUpdate.Name = name;
                userToUpdate.Username = userName;

                db.SaveChanges();

                var updateUser = db.Users.Find(id);

                return Models.User.FromEntity(updateUser);
            }
        }

        public bool DeleteUser(int id)
        {
            using (var db = new SchnuppiEntities())
            {
                var userToDelete = db.Users.Find(id);

                if (userToDelete != null)
                {
                    db.Users.Remove(userToDelete);
                    db.SaveChanges();

                    return true;
                }

                return false;
            }
        }


        #endregion

        public Models.User UpdateUserAdminRights(int id, bool IsAdministrator)
        {
            using (var db = new SchnuppiEntities())
            {
                var userToUpdate = db.Users.Find(id);
                if (userToUpdate == null)
                {
                    return null;
                }

                userToUpdate.IsAdministrator = IsAdministrator;

                db.SaveChanges();

                var updateUser = db.Users.Find(id);

                return Models.User.FromEntity(updateUser);
            }
        }

        public bool UpdateUserChangePassword(int id, string oldPassword, string newPassword, string newPasswordConfirm)
        {
            using (var db = new SchnuppiEntities())
            {
                var userToUpdate = db.Users.Find(id);
                if (userToUpdate == null)
                {
                    // USer not found
                    return false;
                }

                if (userToUpdate.Password != oldPassword)
                {
                    // old Password wrong
                    return false;
                }
                if (newPassword != newPasswordConfirm)
                {
                    // new and confirm password mismatch
                    return false;
                }

                userToUpdate.Password = newPassword;

                db.SaveChanges();

                return true;
            }
        }

        public Models.User TryLogin(string userName, string clearTextPassword)
        {
            using (var db = new SchnuppiEntities())
            {
                var user = db.Users.Where(x => x.Username == userName && x.Password == clearTextPassword).FirstOrDefault();

                if (user != null)
                {
                    // User found, login successful
                                        
                    return Models.User.FromEntity(user);
                }

                // User not found or wrong password
                return null;
            }
        }
    }
}