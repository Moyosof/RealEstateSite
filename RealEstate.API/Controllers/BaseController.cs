﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts;
using RealEstate.Application.EmailService;
using RealEstate.Application.SmsService;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IBusinessRule _businessRule;
        private IMailService _mailService;
        private ISmsService _smsService;
        private IFluentEmailClient _fluentEmail;

        private ISqlDBObjects _sql;
        private string _userId;
        private string _username;
        private List<string> _roles;
        // private string _token;

        protected IBusinessRule BusinessRule => _businessRule ??= HttpContext.RequestServices.GetRequiredService<IBusinessRule>();
        protected ISqlDBObjects dBObjects => _sql ??= HttpContext.RequestServices.GetRequiredService<ISqlDBObjects>();
        protected IMailService mailService => _mailService ??= HttpContext.RequestServices.GetRequiredService<IMailService>();
        protected ISmsService smsService => _smsService ??= HttpContext.RequestServices.GetRequiredService<ISmsService>();
        protected IFluentEmailClient fluentEmail => _fluentEmail ??= HttpContext.RequestServices.GetRequiredService<IFluentEmailClient>();

        protected string UserId => _userId ??= BusinessRule.GetLoggedInUserId();
        protected string UserEmail => _username ??= BusinessRule.GetCurrentLoggedinUserEmail();
        protected List<string> UserRoles => _roles ??= BusinessRule.GetCurrentLoggedinUserRole();
        //protected string token => _token ??= Util.GenerateRandomDigits(5);
    }
}
