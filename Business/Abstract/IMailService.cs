using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IMailService
    {
        IResult SendMail(SendMailDto sendMailDto);
    }
}
