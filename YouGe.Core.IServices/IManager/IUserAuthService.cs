using System;
using YouGe.Core.Models.UsersInfo;

namespace YouGe.Core.Interface.IServices.IManager
{
    public interface IUserAuthService
    {
        ClientUser Authenticate(string name, string password);
    }
}
