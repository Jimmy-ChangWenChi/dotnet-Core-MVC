using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CliMvpApp.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace CliMvpApp.Controllers;

public class DeviceController : Controller
{
    // public DeviceController(ApplicationDbContext context){
    //     _context = context;
    // }
    
    public async Task<IActionResult> device()
    {   
        try{
            
            using (var context = new ApplicationDbContext())
            {
                //var DeviceLogs = await context.e01.where(d => d.pno == "WUR080W6~11" && d.mechine == "V7100").Select(d => new {d.pno,d.mechine,d.cust_no}).ToListAsync();
                var DeviceLogs = await context.e01.Select(d => new {d.pno,d.mechine,d.cust_no}).ToListAsync();
            
            }
        }
        catch(Exception ex){
            Console.WriteLine($"錯誤:{ex.Message}");
        }
        
        //將資料傳到View
        ViewBag.Data = DeviceLogs;

        return View();
    }
}
