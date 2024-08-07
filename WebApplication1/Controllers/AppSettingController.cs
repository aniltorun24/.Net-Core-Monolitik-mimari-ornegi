﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AppSettingController : Controller
    {


        private readonly IConfiguration _configuration;

        public AppSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewBag.baseUrl = _configuration["baseUrl"];
            ViewBag.smsKey = _configuration["Keys:Sms"];
            ViewBag.emailKey = _configuration.GetSection("Keys")["email"];//bunlar appsettingden veri okumak için kullanılabilecek yöntemler

            return View();
        }
    }
}
