//using Application.BindingModel;
//using Application.Concrete;
//using Application.Infrastructure;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;

//namespace api.Controllers
//{
//    [Area("Admin")]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AdminRoleController : Controller
//    {
//        private readonly RoleManager<Role> _roleManager;

//        public AdminRoleController(RoleManager<Role> roleManager)
//        {
//            _roleManager=roleManager;
//        }

//        [HttpPost("role")]
//        public async Task<ActionResponse<RoleBindingModel>> CreateRole([FromBody] RoleBindingModel model, string id)
//        {
//            ActionResponse<RoleBindingModel> actionResponse = new()
//            {
//                ResponseType = ResponseType.Ok
//            };

//            IdentityResult result = null;
//            if (id!=null)
//            {
//                Role role = await _roleManager.FindByIdAsync(id);
//                role.Name = model.Role;
//                result= await _roleManager.UpdateAsync(role);
//            }
//            else
//            {
//                result = await _roleManager.CreateAsync(new Role
//                {
//                    Name=model.Role,
//                    DateTime = DateTime.UtcNow
//                });
//            }

//            if (result.Succeeded)
//            {
//                actionResponse.IsSuccessful=true;

//            }
//            return actionResponse;
//        }

//        public async Task<ActionResponse<RoleBindingModel>> DeleteRole([FromBody] RoleBindingModel model, string id)
//        {
//            ActionResponse<RoleBindingModel> actionResponse = new()
//            {
//                ResponseType = ResponseType.Ok
//            };

//            Role role = await _roleManager.FindByIdAsync(id);
//            IdentityResult result = await _roleManager.DeleteAsync(role);

//            if (result.Succeeded)
//            {
//                actionResponse.IsSuccessful=true;

//            }
//            return actionResponse;
//        }






//    }
//}
