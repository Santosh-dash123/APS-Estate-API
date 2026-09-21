using APSEstate.CONCRETE.Interface;
using APSEstate.CORE.APIResponse;
using APSEstate.CORE.Data;
using APSEstate.CORE.Enum;
using APSEstate.CORE.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APSEstate.CONCRETE.Repository
{
    public class BuilderRepository : IBuilderRepository
    {
        private readonly AppDbContext _db;

        public BuilderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<BuilderGetModel>> GetBuilderAsync(int? builderId)
        {
            var response = new ApiResponse<BuilderGetModel>();
            var connection = _db.Database.GetDbConnection();

            try
            {
                await using var command = connection.CreateCommand();

                command.CommandText = "dbo.SP_GetBuilder";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(
                    new SqlParameter("@BuilderId", builderId ?? (object)DBNull.Value)
                );

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                var result = new List<BuilderGetModel>();

                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(new BuilderGetModel
                    {
                        BuilderId = Convert.ToInt32(reader["BuilderId"]),
                        AdminId = reader["AdminId"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(reader["AdminId"]),
                        BuilderName = reader["BuilderName"]?.ToString(),
                        Address = reader["Address"]?.ToString(),
                        City = reader["City"]?.ToString(),
                        District = reader["District"]?.ToString(),
                        PIN = reader["PIN"]?.ToString(),
                        State = reader["State"]?.ToString(),
                        PhoneNo = reader["PhoneNo"]?.ToString(),
                        EmailId = reader["EmailId"]?.ToString(),
                        CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                        UpdationDate = reader["UpdationDate"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["UpdationDate"]),
                        CreatedBy = reader["CreatedBy"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(reader["CreatedBy"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                        IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                    });
                }

                response.success = true;
                response.message = "Builder data fetched successfully.";
                response.data = result;

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

        public async Task<ApiSingleResponse<BuilderGetModel>> SaveBuilderAsync(BuilderSaveModel data,EnumAction action)
        {
            var response = new ApiSingleResponse<BuilderGetModel>();
            var connection = _db.Database.GetDbConnection();

            try
            {
                if (data == null)
                {
                    response.success = false;
                    response.message = "No data provided.";
                    return response;
                }

                await using var command = connection.CreateCommand();

                command.CommandText = "dbo.SP_SaveBuilder";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(
                    new SqlParameter("@Action", action.ToString())
                );

                command.Parameters.Add(
                    new SqlParameter("@BuilderId",
                        data.BuilderId ?? (object)DBNull.Value)
                );

                command.Parameters.Add(
                    new SqlParameter("@AdminId",
                        data.AdminId ?? (object)DBNull.Value)
                );

                command.Parameters.Add(
                    new SqlParameter("@BuilderName",
                        string.IsNullOrWhiteSpace(data.BuilderName)
                            ? (object)DBNull.Value
                            : data.BuilderName)
                );

                command.Parameters.Add(
                    new SqlParameter("@Address",
                        string.IsNullOrWhiteSpace(data.Address)
                            ? (object)DBNull.Value
                            : data.Address)
                );

                command.Parameters.Add(
                    new SqlParameter("@City",
                        string.IsNullOrWhiteSpace(data.City)
                            ? (object)DBNull.Value
                            : data.City)
                );

                command.Parameters.Add(
                    new SqlParameter("@District",
                        string.IsNullOrWhiteSpace(data.District)
                            ? (object)DBNull.Value
                            : data.District)
                );

                command.Parameters.Add(
                    new SqlParameter("@PIN",
                        string.IsNullOrWhiteSpace(data.PIN)
                            ? (object)DBNull.Value
                            : data.PIN)
                );

                command.Parameters.Add(
                    new SqlParameter("@State",
                        string.IsNullOrWhiteSpace(data.State)
                            ? (object)DBNull.Value
                            : data.State)
                );

                command.Parameters.Add(
                    new SqlParameter("@PhoneNo",
                        string.IsNullOrWhiteSpace(data.PhoneNo)
                            ? (object)DBNull.Value
                            : data.PhoneNo)
                );

                command.Parameters.Add(
                    new SqlParameter("@EmailId",
                        string.IsNullOrWhiteSpace(data.EmailId)
                            ? (object)DBNull.Value
                            : data.EmailId)
                );

                command.Parameters.Add(
                    new SqlParameter("@Password",
                        string.IsNullOrWhiteSpace(data.Password)
                            ? (object)DBNull.Value
                            : data.Password)
                );

                command.Parameters.Add(
                    new SqlParameter("@CreatedBy",
                        data.CreatedBy ?? (object)DBNull.Value)
                );

                command.Parameters.Add(
                    new SqlParameter("@IsActive", data.IsActive)
                );

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    response.success =
                        reader["Success"] != DBNull.Value &&
                        Convert.ToInt32(reader["Success"]) == 1;

                    response.message = reader["Message"]?.ToString();

                    var id = reader["Id"] == DBNull.Value
                        ? (int?)null
                        : Convert.ToInt32(reader["Id"]);

                    if (response.success && id.HasValue && action != EnumAction.DELETE)
                    {
                        response.data = new BuilderGetModel
                        {
                            BuilderId = id.Value,
                            AdminId = data.AdminId,
                            BuilderName = data.BuilderName,
                            Address = data.Address,
                            City = data.City,
                            District = data.District,
                            PIN = data.PIN,
                            State = data.State,
                            PhoneNo = data.PhoneNo,
                            EmailId = data.EmailId,
                            CreatedBy = data.CreatedBy,
                            IsActive = data.IsActive
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