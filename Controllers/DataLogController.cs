using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CliMvpApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CliMvpApp.Controllers;

public class DataLogController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string _connectionString;

    // public DataLogController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    public DataLogController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleConnection"); //抓取appsettings.json 的資訊
    }

    //public IActionResult Index()
    public async Task<IActionResult> Index()
    {
        var Datalogs = new List<DataLogModel>(); //將抓取的資料放入 Datalogs

        using (var connection = new OracleConnection(_connectionString)) //設定資料庫連線,使用ADO.net操作
        {
            try{
                await connection.OpenAsync(); //開啟資料庫

                string sql = "select customer_code,kind,proc_name from n_datalog_log where kind = 'DATALOG' and customer_code = '1005' and to_char(log_date,'yyyymmdd') = '20241227'";

                using (var commnad = new OracleCommand(sql,connection)) //建立命令來查詢
                {
                    using(var reader = await commnad.ExecuteReaderAsync()) // 執行查詢,並讀取結果
                    {
                        while(await reader.ReadAsync())
                        {
                            Datalogs.Add(new DataLogModel
                            {
                                customer_code = reader.GetString(0),
                                kind = reader.GetString(1),
                                MO = reader.GetString(2)
                            });
                        }
                    }

                }

            }
            catch(Exception ex){
                return StatusCode(500,$"error: {ex.Message}");
            }
        }

        
        return View(Datalogs);
    }

}
