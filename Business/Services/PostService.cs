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
    public interface IPostService
    {
        IQueryable<PostModel> Query();
        Result Add(PostModel model);
        Result Update(PostModel model);
        Result Delete(int id);
    }

    public class PostService : IPostService
    {
        private readonly Db _db;

        public PostService(Db db)
        {
            _db = db;
        }

        public IQueryable<PostModel> Query()
        {
            return _db.Posts.Select(p => new PostModel()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                PostedOn = p.PostedOn,

                PostOutput = p.Id,
                PostedOnOutput = p.PostedOn.ToString("MM/dd/yyyy")
            });
        }

        public Result Add(PostModel model)
        {
            Post entity = new Post()
            {
                Title = model.Title,
                Content = model.Content,
                PostedOn = DateTime.Now,

            };

            _db.Posts.Add(entity);
            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult();
        }

        public Result Update(PostModel model)
        {
            Post entity = _db.Posts.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Post not found!");

            entity.Title = model.Title;
            entity.Content = model.Content;

            _db.Posts.Update(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Post entity = _db.Posts.Find(id);
            if (entity is null)
                return new ErrorResult("Post not found!");

            _db.Posts.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }
    }
}
