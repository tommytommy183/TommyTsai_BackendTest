using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Channels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebApplication1Controller : ControllerBase
    {
        #region Model
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BackendExamHub;Trusted_Connection=True;";
        public class MyOfficeQueryModel
        {
            public string? cName { get; set; }
            public string? eName { get; set; }
            public string? sName { get; set; }
            public string? email { get; set; }
        }
        public class MyOfficeInsertModel
        {
            public string? cName { get; set; }
            public string? eName { get; set; }
            public string? sName { get; set; }
            public string? email { get; set; }
            public int? status { get; set; }
            public bool? isStop { get; set; }
            public string? stopMemo { get; set; }
            public string? loginID { get; set; }
            public string? loginPW { get; set; }
            public string? memo { get; set; }
            public string? aid { get; set; }
        }
        public class MyOfficeUpdateModel
        {
            public string? SID { get; set; }
            public string? cName { get; set; }
            public string? eName { get; set; }
            public string? sName { get; set; }
            public string? email { get; set; }
            public int? status { get; set; }
            public bool? isStop { get; set; }
            public string? stopMemo { get; set; }
            public string? loginID { get; set; }
            public string? loginPW { get; set; }
            public string? memo { get; set; }
            public string? uid { get; set; }
        }
        public class MyOfficeDeleteModel
        {
            public string? SID { get; set; }
            public string? cName { get; set; }
            public string? eName { get; set; }
            public string? sName { get; set; }
            public string? email { get; set; }
            public int? status { get; set; }
            public bool? isStop { get; set; }
            public string? stopMemo { get; set; }
            public string? loginID { get; set; }
            public string? loginPW { get; set; }
            public string? memo { get; set; }
            public string? uid { get; set; }
        }
        public class MyOfficeModel
        {
            public string? cName { get; set; }
            public string? eName { get; set; }
            public string? sName { get; set; }
            public string? email { get; set; }
            public int? status { get; set; }
            public bool? isStop { get; set; }
            public string? stopMemo { get; set; }
            public string? loginID { get; set; }
            public string? loginPW { get; set; }
            public string? memo { get; set; }
            public DateTime? adt { get; set; }
            public string? aid { get; set; }
            public DateTime? udt { get; set; }
            public string? uid { get; set; }
        }

        #endregion

        [HttpPost("GetMyOffice_ACPD")]
        public IActionResult GetMyOffice_ACPD(MyOfficeQueryModel query)
        {
            var results = new List<Dictionary<string, object>>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("EXEC prGetData @cName, @eName, @sName, @email", conn))
                    {
                        command.Parameters.AddWithValue("@cName", string.IsNullOrEmpty(query.cName) ? (object)DBNull.Value : query.cName);
                        command.Parameters.AddWithValue("@eName", string.IsNullOrEmpty(query.eName) ? (object)DBNull.Value : query.eName);
                        command.Parameters.AddWithValue("@sName", string.IsNullOrEmpty(query.sName) ? (object)DBNull.Value : query.sName);
                        command.Parameters.AddWithValue("@email", string.IsNullOrEmpty(query.email) ? (object)DBNull.Value : query.email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                    row[reader.GetName(i)] = reader[i];
                                results.Add(row);
                            }
                        }
                    }
                    return Ok(new { query, data = results });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
        }

        [HttpPost("InsertMyOffice_ACPD")]
        public IActionResult InsertMyOffice_ACPD(MyOfficeInsertModel data)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sqlCommand = @"EXEC prInsertData
                                    @cName,
                                    @eName,
                                    @sName,
                                    @email,
                                    @status,
                                    @isStop,
                                    @stopMemo,
                                    @loginID,
                                    @loginPW,
                                    @memo,
                                    @aid";

                    using (SqlCommand command = new SqlCommand(sqlCommand, conn))
                    {
                        command.Parameters.AddWithValue("@cName", string.IsNullOrEmpty(data.cName) ? (object)DBNull.Value : data.cName);
                        command.Parameters.AddWithValue("@eName", string.IsNullOrEmpty(data.eName) ? (object)DBNull.Value : data.eName);
                        command.Parameters.AddWithValue("@sName", string.IsNullOrEmpty(data.sName) ? (object)DBNull.Value : data.sName);
                        command.Parameters.AddWithValue("@email", string.IsNullOrEmpty(data.email) ? (object)DBNull.Value : data.email);
                        command.Parameters.AddWithValue("@status", data.status);
                        command.Parameters.AddWithValue("@isStop", data.isStop);
                        command.Parameters.AddWithValue("@stopMemo", string.IsNullOrEmpty(data.stopMemo) ? (object)DBNull.Value : data.stopMemo);
                        command.Parameters.AddWithValue("@loginID", data.loginID);
                        command.Parameters.AddWithValue("@loginPW", data.loginPW);
                        command.Parameters.AddWithValue("@memo", string.IsNullOrEmpty(data.memo) ? (object)DBNull.Value : data.memo);
                        command.Parameters.AddWithValue("@aid", data.aid);

                        command.ExecuteNonQuery();
                        return Ok(new { success = true });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
        }

        [HttpPut("UpdateMyOffice_ACPD")]
        public IActionResult UpdateMyOffice_ACPD(MyOfficeUpdateModel data)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sqlCommand = @"EXEC prUpdateData
                                    @SID,
                                    @cName,
                                    @eName,
                                    @sName,
                                    @email,
                                    @status,
                                    @isStop,
                                    @stopMemo,
                                    @loginID,
                                    @loginPW,
                                    @memo,
                                    @uid";

                    using (SqlCommand command = new SqlCommand(sqlCommand, conn))
                    {
                        command.Parameters.AddWithValue("@SID", string.IsNullOrEmpty(data.SID) ? (object)DBNull.Value : data.SID);
                        command.Parameters.AddWithValue("@cName", string.IsNullOrEmpty(data.cName) ? (object)DBNull.Value : data.cName);
                        command.Parameters.AddWithValue("@eName", string.IsNullOrEmpty(data.eName) ? (object)DBNull.Value : data.eName);
                        command.Parameters.AddWithValue("@sName", string.IsNullOrEmpty(data.sName) ? (object)DBNull.Value : data.sName);
                        command.Parameters.AddWithValue("@email", string.IsNullOrEmpty(data.email) ? (object)DBNull.Value : data.email);
                        command.Parameters.AddWithValue("@status", data.status);
                        command.Parameters.AddWithValue("@isStop", data.isStop);
                        command.Parameters.AddWithValue("@stopMemo", string.IsNullOrEmpty(data.stopMemo) ? (object)DBNull.Value : data.stopMemo);
                        command.Parameters.AddWithValue("@loginID", data.loginID);
                        command.Parameters.AddWithValue("@loginPW", data.loginPW);
                        command.Parameters.AddWithValue("@memo", string.IsNullOrEmpty(data.memo) ? (object)DBNull.Value : data.memo);
                        command.Parameters.AddWithValue("@uid", data.uid);

                        command.ExecuteNonQuery();
                        return Ok(new { success = true });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
        }

        [HttpDelete("UpdateMyOffice_ACPD")]
        public IActionResult DeleteMyOffice_ACPD(MyOfficeDeleteModel data)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sqlCommand = @"EXEC prUpdateData
                                    @SID,
                                    @cName,
                                    @eName,
                                    @sName,
                                    @email,
                                    @status,
                                    @isStop,
                                    @stopMemo,
                                    @loginID,
                                    @loginPW,
                                    @memo,
                                    @uid";

                    using (SqlCommand command = new SqlCommand(sqlCommand, conn))
                    {
                        command.Parameters.AddWithValue("@SID", string.IsNullOrEmpty(data.SID) ? (object)DBNull.Value : data.SID);
                        command.Parameters.AddWithValue("@cName", string.IsNullOrEmpty(data.cName) ? (object)DBNull.Value : data.cName);
                        command.Parameters.AddWithValue("@eName", string.IsNullOrEmpty(data.eName) ? (object)DBNull.Value : data.eName);
                        command.Parameters.AddWithValue("@sName", string.IsNullOrEmpty(data.sName) ? (object)DBNull.Value : data.sName);
                        command.Parameters.AddWithValue("@email", string.IsNullOrEmpty(data.email) ? (object)DBNull.Value : data.email);
                        command.Parameters.AddWithValue("@status", data.status);
                        command.Parameters.AddWithValue("@isStop", data.isStop);
                        command.Parameters.AddWithValue("@stopMemo", string.IsNullOrEmpty(data.stopMemo) ? (object)DBNull.Value : data.stopMemo);
                        command.Parameters.AddWithValue("@loginID", data.loginID);
                        command.Parameters.AddWithValue("@loginPW", data.loginPW);
                        command.Parameters.AddWithValue("@memo", string.IsNullOrEmpty(data.memo) ? (object)DBNull.Value : data.memo);
                        command.Parameters.AddWithValue("@uid", data.uid);

                        command.ExecuteNonQuery();
                        return Ok(new { success = true });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
        }

    }
}