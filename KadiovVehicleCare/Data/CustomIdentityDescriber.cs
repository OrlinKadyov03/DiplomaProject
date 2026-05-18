using Microsoft.AspNetCore.Identity;

namespace KadiovVehicleCare.Data
{
    public class CustomIdentityDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "Вече съществува потребител с този имейл."
            };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = "Вече съществува акаунт с този имейл."
            };
        }
    }
}
