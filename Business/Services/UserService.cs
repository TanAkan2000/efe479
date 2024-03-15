using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public interface IUserService
    {
        IQueryable<UserModel> Query();
        Result Add(UserModel model);
        Result Update(UserModel model);
        Result Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly Db _db;

        public UserService(Db db)
        {
            _db = db;
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Select(u => new UserModel()
            {
                Id = u.Id,
                Username = u.Username,
                Prof = u.Prof,
                BirthDate = u.BirthDate,
                IsAdmin = u.IsAdmin,
                Email = u.Email,
                Password = u.Password,

                BirthDateOutput = u.BirthDate.ToString("MM/dd/yyyy"),
                EmailOutput = u.Email,
                PasswordOutput = u.Password,
                IsAdminOutput = u.IsAdmin ? "Yes" : "No"
            });
        }

        public Result Add(UserModel model)
        {
            User entity = new User()
            {
                Username = model.Username,
                Prof = model.Prof,
                BirthDate = model.BirthDate,
                IsAdmin = model.IsAdmin,
                Email = model.Email,
                Password = model.Password
            };

            _db.Users.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult();
        }

        public Result Update(UserModel model)
        {
            User entity = _db.Users.Find(model.Id);
            if (entity is null)
                return new ErrorResult("User not found!");

            entity.Username = model.Username;
            entity.Prof = model.Prof;
            entity.BirthDate = model.BirthDate;
            entity.IsAdmin = model.IsAdmin;
            entity.Email = model.Email;
            entity.Password = model.Password;

            _db.Users.Update(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            User entity = _db.Users.Find(id);
            if (entity is null)
                return new ErrorResult("User not found!");

            _db.Users.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }
    }
}
