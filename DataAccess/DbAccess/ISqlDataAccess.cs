namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string storedProcedure, U paramerters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T paramerters, string connectionId = "Default");
    }
}