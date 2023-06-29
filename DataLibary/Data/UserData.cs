using DataLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibary.Data
{
    public class UserData : IUserData
    {
        private readonly IDataAccess _db;

        public UserData(IDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers() => _db.LoadData<UserModel, dynamic>("Call GetUsers;", new { });

        public async Task<UserModel?> GetUser(int id)
        {
            var result = await _db.LoadData<UserModel, dynamic>("Call GetSingleUser(@ID)", new { ID = id });

            return result.First();
        }

        public Task InsertUser(UserModel user) => _db.SaveData("Call CreateUser(@fn,@ln)", new { fn = user.FirstName, ln = user.LastName});
        public Task UpdateUser(UserModel user) => _db.SaveData("Call UpdateUser(@UserId, @FirstName, @LastName)", new { UserId = user.id, FirstName = user.FirstName, LastName = user.LastName });
        public Task DeleteUser(int id) => _db.SaveData("Call DeleteUser(@UserId)", new { UserId = id});

        
    }
}
