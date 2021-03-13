using CodeFactoryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeFactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAPI : ControllerBase
    {
        private SignInManager<User> signInManager;

        public AccountAPI(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
    }
}
