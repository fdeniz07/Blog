using BusinessLayer.Abstract;
using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;
using System;
using System.Net;
using System.Net.Mail;
using CoreLayer.Utilities.Results.ComplexTypes;
using CoreLayer.Utilities.Results.Concrete;
using EntityLayer.Concrete;
using Microsoft.Extensions.Options;

namespace BusinessLayer.Concrete
{
    public class MailManager:IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public IResult Send(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail), //fatih_deniz_07@hotmail.com
                To = { new MailAddress(emailSendDto.Email) }, //fdeniz07@gmail.com
                Subject = emailSendDto.Subject, //Sifremi unuttum  // Siparisiniz basariyla kargolandi
                IsBodyHtml = true, 
                Body = emailSendDto.Message //Yeni sifreniz : abcdef
            };
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);

            return new Result(ResultStatus.Success, $"E-Postanız başarıyla gönderilmiştir.");
        }

        public IResult SendContactEmail(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail), //Bekir Demir bekirdemir@gmail.com (iletisim kismindan mesaj gönderen kisinin mail adresi) // Blog uygulamaniza yeni bir icerik önerisi
                To = { new MailAddress("blog@denizfatih.com") }, // blog@denizfatih.com (Bu adres bizim blog sistemimizden gelen mesajlari tutan mail adresimiz)
                Subject = emailSendDto.Subject,
                IsBodyHtml = true, //Epostanin daha güzel gözükmesi icin
                Body = $"Gönderen Kişi:  <b>{emailSendDto.Name},</b><br/>Gönderen E-Posta Adresi:  <b>{emailSendDto.Email}</b><br/><br/><br/>{emailSendDto.Message}"
            };
            SmtpClient smtpClient = new SmtpClient //fatih_deniz_07@hotmail.com (Bizim sistemimizin mail adresi)
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username,_smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(message);

            return new Result(ResultStatus.Success, $"E-Postanız başarıyla gönderilmiştir.");
        }
    }
}
