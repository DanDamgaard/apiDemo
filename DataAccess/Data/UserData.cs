using DataAccess.DbAccess;
using DataAccess.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<List<UserModal>> GetUsers() 
    {
        List<UserModal> people = await _db.LoadData<UserModal, dynamic>("Call GetUsers;", new { });

        return people;
    }

    //public async Task<UserModal?> GetUser(int id)
    //{
    //    var results = await _db.LoadData<UserModal, dynamic>("dbtest.GetSingleUser", new { Id = id });

    //    return results.FirstOrDefault();
    //}

    public Task InsertUser(UserModal user) => _db.SaveData("dbtest.CreateUser", new { user.FirstName, user.LastName });
    public Task UpdateUser(UserModal user) => _db.SaveData("dbtest.UpdateUser", user);
    public Task DeleteUser(int id) => _db.SaveData("dbtest.DeleteUser", new { Id = id });

}
