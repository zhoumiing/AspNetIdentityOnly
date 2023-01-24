using AspNetIdentityOnly.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityOnly.Services
{
    public class UserService:IUserService
    {
        //框架内置了UserManager服务，无需手工注入DI Container，只需要直接构造函数注入即可使用。
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResultDTO> RegisterAsync(string username, string password)
        {
            //判定登录用户名是否存在
            IdentityUser existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser is not null)
            {
                return new IdentityResultDTO()
                {
                    Errors = new[] { "登录用户名已存在，无法重复注册!" },
                    Infos = new[] {"用户名"+username+"已被注册过"}
                };
            }
            IdentityUser newUser = new IdentityUser()
            {
                UserName = username,
            };
            // 使用UserManager.CreateAsync方法创建用户，参数IdentityUser 和 密码。
            IdentityResult CreatedUser = await _userManager.CreateAsync(newUser, password);
            //通过IdentityResult 的属性 Succeeded和 Errors 判断创建是否成功并获取Errors 信息。
            if (!CreatedUser.Succeeded)
            {
                return new IdentityResultDTO()
                {
                    Errors = new[] { "数据库创建用户过程中失败！" },
                    Infos = CreatedUser.Errors.Select(p => p.Description)
                };
            }
            return new IdentityResultDTO()
            {
                Infos = new[] {"用户创建成功，用户名"+username+"密码"+password}
            };
        }

        public async Task<IdentityResultDTO> LoginAsync(string username, string password)
        {
            IdentityUser LoginedUser =await _userManager.FindByNameAsync(username);
            if (LoginedUser == null)
            {
                return new IdentityResultDTO()
                {
                    Errors = new[] {"没有"+username+"这个用户名！"}
                };
            }
            // UserManager.CheckPassword 返回一个bool 类型，参数是IdentityUser和密码string
            bool isChecked = await _userManager.CheckPasswordAsync(LoginedUser, password);
            if (!isChecked)
            {
                return new IdentityResultDTO()
                {
                    Errors = new[] { "密码错误！" }
                };
            }
            return new IdentityResultDTO()
            {
                Infos = new[] { username+"已登录成功!" }
            };
        }
    }
}
