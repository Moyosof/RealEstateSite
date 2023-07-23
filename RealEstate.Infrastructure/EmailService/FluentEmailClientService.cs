using FluentEmail.Core;
using RealEstate.Application.EmailService;
using RealEstate.Domain.Entities.ReadOnly;
using RealEstate.DTO.WriteOnly.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.EmailService
{
    public class FluentEmailClientService : IFluentEmailClient
    {
        private readonly IFluentEmail _fluentEmail;
        public FluentEmailClientService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendChangePasswordEmail(string userEmail)
        {
            var emailTemplate = "<p>Hello,</p> <p>You have successfully changed your password</P>";
            var newEmail = _fluentEmail
                .To(userEmail)
                .Subject("[TestMode] RealEstate Password Change")
                .UsingTemplate<string>(emailTemplate, userEmail);
            await newEmail.SendAsync();
        }

        public async Task SendForgotPasswordEmail(ForgetPasswordDTO forgetPassword, string link)
        {
            var emailTemplate = $"<p> Click <a href=\"{link}\">here</a> to reset your account password </p>";
            var newEmail = _fluentEmail
                .To(forgetPassword.Email)
                .Subject("[TestMode] Real Estate Password reset")
                .UsingTemplate<ForgetPasswordDTO>(emailTemplate, forgetPassword);
            await newEmail.SendAsync();
        }


        public async Task SendResetPasswordEmail(ResetPasswordDTO resetPassword)
        {
            var emailTemplate = "<p>Your Password was successfully reset</p>";
            var newEmail = _fluentEmail
                .To(resetPassword.Email)
                .Subject("[TestMode] Reset Password Successful")
                .UsingTemplate<ResetPasswordDTO>(emailTemplate, resetPassword);
            await newEmail.SendAsync();
        }

        public async Task SendOneTimeCodeEmail(object source, OneTimeCodeDTO oneTimeCodeDTO, CancellationToken cancellationToken)
        {
            var emailTemplate = "<p>Your OTP is <i>@Model.Token</i> .</p>";
            var newEmail = _fluentEmail
                .To(oneTimeCodeDTO.Sender)
                .Subject("[TestMode] One Time Code")
                .UsingTemplate<OneTimeCodeDTO>(emailTemplate, oneTimeCodeDTO);
            await newEmail.SendAsync(cancellationToken);
        }
    }
}
