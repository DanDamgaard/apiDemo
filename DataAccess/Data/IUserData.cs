using DataAccess.Modals;

namespace DataAccess.Data;

public interface IUserData
{
    Task DeleteUser(int id);
    //Task<UserModal?> GetUser(int id);
    Task<List<UserModal>> GetUsers();
    Task InsertUser(UserModal user);
    Task UpdateUser(UserModal user);
}