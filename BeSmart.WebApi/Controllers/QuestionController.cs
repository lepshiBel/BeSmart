using BeSmart.Domain.Models;
using BeSmart.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BeSmartDbController<Question, QuestionRepository>
    {
        public QuestionController(QuestionRepository repository) : base(repository)
        {

        }
    }
}
