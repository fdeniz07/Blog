using CoreLayer.Utilities.Results.Abstract;
using EntityLayer.Dtos;

namespace BusinessLayer.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto); // Sifre degisikligi

        IResult SendContactEmail(EmailSendDto emailSendDto); 
    }
}
