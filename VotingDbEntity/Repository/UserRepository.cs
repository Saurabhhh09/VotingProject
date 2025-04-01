using ShowModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingDbEntity.Entities;
using VotingDbEntity.Enums;

namespace VotingDbEntity.Repository
{
    public class UserRepository
    {
        private readonly VotingDbContext _db = new VotingDbContext();

        public async Task<string> AddUserAsync(AddNewUserModel model, EnumRole role)
        {
            try
            {
                var mail = await _db.Users.Where(u => u.Email == model.Email).CountAsync();
                if (mail == 0)
                {
                    var user = new User
                    {
                        Fname = model.FirstName,
                        Lname = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                        Role = RoleEnum.User,
                        Date = DateTime.UtcNow
                    };
                    _db.Users.Add(user);
                    await _db.SaveChangesAsync();
                    return null;
                }
                else
                {
                    return "This Email is already registered.Try another email !!";
                }

            }
            catch (Exception)
            {
                return "Something went wrong";
            }

        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var ListUsers = new List<UserModel>();           
            var users = await _db.Users.ToListAsync();
            foreach (var user in users)
            {
                var newUser = new UserModel
                {
                    Id = user.UserId,
                    First_Name = user.Fname,
                    Last_Name = user.Lname,
                    Email = user.Email,
                    Role = (EnumRole)user.Role,
                };
                ListUsers.Add(newUser);
            }
            return ListUsers;
        }

        public async Task<LoginResultModel> LoginCheckAsync(LoginModel model)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email.ToLower() && u.Password == model.Password);
                if (user == null)
                {
                    return new LoginResultModel
                    {
                        IsSuccessful = false,
                        //Role = null
                    };
                }

                return new LoginResultModel
                {
                    IsSuccessful = true,
                    Role = user.Role.ToString(),
                    UserId = user.UserId,
                };
            }
            catch (Exception)
            {
                return new LoginResultModel
                {
                    IsSuccessful = false,
                    //Role = null
                };
            }
        }


        public async Task<UserModel> GetUserbyIdAsync(int id)
        {
            var user = await _db.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();
            var newUser = new UserModel
            {
                Id = user.UserId,
                First_Name = user.Fname,
                Last_Name = user.Lname,
                Email = user.Email,
                Password = user.Password,
                Role = (EnumRole)user.Role,
                Date = user.Date.ConvertUtcToLocalTime()
            };
            return newUser;
        }

        public async Task<bool> UpdateUserAsync(UserModel model)
        {
            try
            {
                var user = await _db.Users.Where(u => u.UserId == model.Id).FirstOrDefaultAsync();
                user.Fname = model.First_Name;
                user.Lname = model.Last_Name;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Role = (RoleEnum)model.Role;
                _db.Users.AddOrUpdate(user);
                await _db.SaveChangesAsync();               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateUserByAdminAsync(UserModel model)
        {
            try
            {
                var user = await _db.Users.Where(u => u.UserId == model.Id).FirstOrDefaultAsync();
                user.Fname = model.First_Name;
                user.Lname = model.Last_Name;
                user.Email = model.Email;
                user.Role = (RoleEnum)model.Role;
                _db.Users.AddOrUpdate(user);
                await _db.SaveChangesAsync();               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _db.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
