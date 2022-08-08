using BachAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachAPI.Services.Interfaces
{
    public interface IUserService
    {
        List<User> Gets();
        User Get(string Id);
        User GetOnGid(string Id);
        User Create(User user);
        void Update(string id, User user);
        void Remove(User user);
        void Remove(string id);

    }
}
