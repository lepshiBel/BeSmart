using BeSmart.Application.Interfaces;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeSmart.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IServiceMembership serviceMembership;
        public MembershipController(IServiceMembership serviceMembership)
        {
            this.serviceMembership = serviceMembership;
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet(nameof(GetAllMemberships))]
        public async Task<ActionResult<List<Membership>>> GetAllMemberships()
        {
            var memberships = await serviceMembership.GetAllMembershipsAsync();

            if (!memberships.Any())
            {
                return NoContent();
            }

            return Ok(memberships);
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet(nameof(GetMembershipsForUser))]
        public async Task<ActionResult<List<Membership>>> GetMembershipsForUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return BadRequest();

            var userClaims = identity.Claims;
            var userId = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == "id")?.Value);

            var memberships = await serviceMembership.GetAllMembershipsForUserAsync(userId);

            if (!memberships.Any())
            {
                return NoContent();
            }

            return Ok(memberships);
        }


        //[Authorize]
        //[HttpPost("Create")]
        //public async Task<ActionResult> Post([FromBody] CategoryCreationDTO categoryDto)
        //{
        //    var createdCategory = await serviceCategory.AddCategoryAsync(categoryDto);

        //    if (createdCategory is null)
        //    {
        //        return BadRequest("Category object is invalid");
        //    }

        //    return Ok(createdCategory);
        //}
    }

}






   