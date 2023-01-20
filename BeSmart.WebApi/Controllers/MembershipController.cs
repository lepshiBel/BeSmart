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

        //[Authorize(Roles = "admin")]
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

        [Authorize(Roles ="user, admin")]
        [HttpPost("AddMembership/{courseId}")]
        public async Task<ActionResult> Post(int courseId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return BadRequest();

            var userClaims = identity.Claims;
            var userId = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == "id")?.Value);

            var createdMembership = await serviceMembership.CreateNewMembershipAsync(courseId, userId);

            if (createdMembership is null)
            {
                return BadRequest();
            }

            return Ok(createdMembership);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceMembership.DeleteMembershipAsync(id);

            if (entity == null)
            {
                return BadRequest("Passed id is invalid");
            }

            return Ok();
        }

    }
}






   