using APSEstate.CONCRETE.Interface;
using APSEstate.CORE.APIResponse;
using APSEstate.CORE.Data;
using APSEstate.CORE.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CONCRETE.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _db;

        public AuthRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ApiSingleResponse<LoginResponseModel>> LoginAsync(LoginModel data)
        {
            var response = new ApiSingleResponse<LoginResponseModel>();
            var connection = _db.Database.GetDbConnection();

            try
            {
                if (data == null)
                {
                    response.success = false;
                    response.message = "Login data is required.";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(data.UserName) ||
                    string.IsNullOrWhiteSpace(data.Password))
                {
                    response.success = false;
                    response.message = "Username and password are required.";
                    return response;
                }

                await using var command = connection.CreateCommand();

                command.CommandText = "dbo.SP_LoginManageUser";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", data.UserName));
                command.Parameters.Add(new SqlParameter("@Password", data.Password));

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response.success = Convert.ToBoolean(reader["IsSuccess"]);
                    response.message = reader["Message"]?.ToString();

                    if (response.success)
                    {
                        response.data = new LoginResponseModel
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            UserTypeId = Convert.ToInt32(reader["UserTypeId"]),
                            UserTypeName = reader["UserTypeName"]?.ToString(),
                            UserName = reader["UserName"]?.ToString(),
                            ReferenceId = reader["ReferenceId"] == DBNull.Value
                                ? null
                                : Convert.ToInt32(reader["ReferenceId"]),
                            CreatedDate = reader["CreatedDate"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["CreatedDate"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                        };
                    }
                }
                else
                {
                    response.success = false;
                    response.message = "No response received from database.";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
                response.data = null;
                return response;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
        }
    }
}
