﻿using BachAPI.Models;
using BachAPI.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BachAPI.Services.Implimentations
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        
        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("users");
        }

        public User Get(string id)
        {
            return _users.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public User GetOnGid(string gid)
        {
            return _users.Find<User>(user => user.GoogleID == gid).FirstOrDefault();
        }

        public List<User> Gets()
        {
            return _users.Find(user => true).ToList();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {            
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Id == userIn.Id);
        }        

        public void Remove(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}
