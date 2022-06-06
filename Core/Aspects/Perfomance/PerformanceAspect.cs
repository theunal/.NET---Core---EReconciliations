using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string body = $"Performance <br/>" +
                    $"{invocation.Method.DeclaringType.FullName}" +
                    $".{invocation.Method.Name} --> <span style='color:red;'>{_stopwatch.Elapsed.TotalSeconds}</span> saniye sürdü. " +
                    $"<br/>Geçmemesi gereken süre:  <span style='color:blue;'>{_interval}</span> sn.";
                SendConfirmEmail(body);
            }
            _stopwatch.Reset();
        }

        void SendConfirmEmail(string body)
        {
            string subject = "Performans Maili";

            SendMailDto sendMailDto = new SendMailDto()
            {
                Email = "emutabakat@zohomail.eu",
                Password = "iTezvGfJqZFYqMD",
                Port = 587,
                SMTP = "smtp.zoho.eu",
                SSL = true,
                email = "kyaka144@gmail.com", // perfomans için bilgi gödnderilecek mail
                subject = subject,
                body = body
            };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.Email);
                mail.To.Add(sendMailDto.email);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add();

                using (SmtpClient smtp = new SmtpClient(sendMailDto.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMailDto.Email, sendMailDto.Password);
                    smtp.EnableSsl = sendMailDto.SSL;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
        }
    }
}