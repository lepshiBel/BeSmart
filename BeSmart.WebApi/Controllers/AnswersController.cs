using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : BeSmartDbController<Answer, AnswerRepository>
    {
        public AnswersController(AnswerRepository repository) : base(repository)
        {    
        }
    }
}
