using BeSmart.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusTestController : ControllerBase
    {
        private readonly IServiceStatusTest serviceStatusTest;
        private readonly IServiceTest serviceTest;

        public StatusTestController(IServiceStatusTest serviceStatusTest, IServiceTest serviceTest)
        {
            this.serviceStatusTest = serviceStatusTest;
            this.serviceTest = serviceTest;
        }

        //[Authorize(Roles ="user")]
        [HttpPost("StartTest/{statusThemeId, testId}")]
        public async Task<ActionResult> StartTest(int statusThemeId, int testId)
        {
            await serviceStatusTest.StartTestAsync(statusThemeId, testId);

            var test = await serviceTest.GetTestWithQuestionsAsync(testId);

            if (test == null) return BadRequest("Passed testId is invalid");

            return Ok(test);
        }

        //[Authorize(Roles = "user")]
        [HttpPost("FinishTheTest/{statusTestId, testId}")]
        public async Task<ActionResult> FinishTheTest(int statusTestId)
        {
            var finishedTest = await serviceStatusTest.FihishTheTestAsync(statusTestId);

            if (finishedTest == null)
                return BadRequest("Passed statusTestId is invalid");

            if (finishedTest.TestStatus != "Пройден")
                return Ok("You haven't answered to all the questions");
                    
            return Ok(finishedTest); 
        }
    }
}
