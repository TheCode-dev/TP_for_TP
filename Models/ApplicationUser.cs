using Microsoft.AspNetCore.Identity;

namespace TP_KP_Belyshev.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public static ApplicationUser GetFromApplicationUserEntity(ApplicationUserEntity obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new ApplicationUser()
            {
                Email = obj.Email,
                Id = obj.ID,
                UserName = obj.FirstName + " " + obj.LastName
            };
        }
    }
}
