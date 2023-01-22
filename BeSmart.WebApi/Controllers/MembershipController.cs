﻿using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Membership;
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
        private readonly IUserService userService;
        public MembershipController(IServiceMembership serviceMembership, IUserService userService)
        {
            this.serviceMembership = serviceMembership;
            this.userService = userService;
        }

        [Authorize(Roles = "admin")]
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

        //[Authorize(Roles = "user, admin")]
        [HttpGet(nameof(GetMembershipsForUser))]
        public async Task<ActionResult<List<MembershipDTO>>> GetMembershipsForUser()
        {
            //var userId = userService.GetCurrentUserId(HttpContext);

            //if (userId == 0) return BadRequest("");
            int userId = 1;

            var memberships = await serviceMembership.GetAllMembershipsForUserAsync(userId);

            if (memberships == null)
            {
                return NoContent();
            }

            return Ok(memberships);
        }

        //[Authorize(Roles = "user, admin")]
        [HttpGet("GetMembershipWithThemes/{membershipId}")]
        public async Task<ActionResult<Membership>> GetMembershipsWithThemesForUser(int membershipId)
        {
            var membership = await serviceMembership.GetMembershipWithThemesForUserAsync(membershipId);

            if (membership == null)
            {
                return NoContent();
            }

            return Ok(membership);
        }

        //[Authorize(Roles ="user, admin")]
        [HttpPost("StartNewCourse/{courseId}")]
        public async Task<ActionResult> StartNewCourse(int courseId) // TODO проверка на валидность courseId
        {
            //var userId = userService.GetCurrentUserId(HttpContext);

            //if (userId == 0) return BadRequest();
            int userId = 1;

            bool isExisted = serviceMembership.CheckMembershipAsync(courseId, userId);
            if (isExisted) return Ok("You already have passed this course");

            var createdMembership = await serviceMembership.CreateNewMembershipAsync(courseId, userId);

            if (createdMembership is null)
            {
                return BadRequest("Passed courseId is not valid");
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






   