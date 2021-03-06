﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineTraining.Entities.Db;
using OnlineTraining.Entities.Entities;
using OnlineTraining.Helper.Config;
using OnlineTraining.Repositories.Interfaces;

namespace OnlineTraining.Repositories.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly IMongoCollection<Bookmark> _bookmarkRepository;
        private readonly IMongoCollection<Course> _courseRepository;
        public BookmarkRepository(IOptions<OtaConfig> config)
        {
            var mongoConnect = new MongoContext(config);
            _bookmarkRepository = mongoConnect.GetConnection().GetCollection<Bookmark>("Bookmarks");
            _courseRepository = mongoConnect.GetConnection().GetCollection<Course>("Courses");
        }

        public async Task<List<Bookmark>> GetBookMarkByUserId(string userId)
        {
            var bookmarkList = await
                _bookmarkRepository.Find(bm => bm.UserId == userId).ToListAsync();
            return bookmarkList;
        }

        public async Task<List<Course>> GetCourseBookMarkByUserId(string userId)
        {
            var listCourses = new List<Course>();
            var bookmarkList = await
                _bookmarkRepository.Find(bm => bm.UserId == userId).ToListAsync();

            foreach (var bookmark in bookmarkList)
            {
                var courseId = bookmark.CourseId;
                var course = await _courseRepository.Find(c => c.Id == courseId).SingleAsync();
                listCourses.Add(course);
            }

            return listCourses;
        }

        public async Task<bool> BookmarkCourse(string courseId, string userId)
        {
            var bookmarkExist =
                 _bookmarkRepository.Find(bm => bm.CourseId == courseId && bm.UserId == userId);
            
            if (bookmarkExist.Count() > 0) {
                return false;
            }

            var bookmark = new Bookmark
            {
                CourseId = courseId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                ModifieddDate = DateTime.Now
            };
            await _bookmarkRepository.InsertOneAsync(bookmark);

            return true;
        }

        public async Task<bool> UnBookmarkCourse(string courseId, string userId)
        {
            await _bookmarkRepository.FindOneAndDeleteAsync(bm => bm.CourseId == courseId && bm.UserId == userId);
            return true;
        }
    }
}
