using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.Helpers.Identity
{
    public interface IAuthenticationService
    {
        Task<Employee> AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public async Task<Employee> AuthenticateUser(string username, string clearTextPassword)
        {
            /*
            InternalUserData userData = _users.FirstOrDefault(u => u.Username.Equals(username)
                && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Username)));
            */
            Account userData =
                await new EducationProcessContext().Accounts.AsNoTracking().Include(x => x.Employee).FirstOrDefaultAsync(x =>
                    x.Username == username && x.Password == clearTextPassword);
            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
            return userData.Employee;
        }

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
}
