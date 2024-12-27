using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    //構造函數
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {

    }

    //定義Dbset,會映射到資料庫中的 e01 表格
    //DbSet<T> 代表資料庫中的一個資料表, T是模型類別
    public Dbset<DeviceModel> e01 {get;set;}

    protected override void OnConfiuring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle("User Id=atest;Password=atest;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.10)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=greatek)))")
    }

}