using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mockify.API.Models;

namespace Mockify.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MockDataController : ControllerBase
    {
        [HttpGet("getUserMock/{limit}")]
        public IActionResult GetUsersMock(int limit)
        {
            var fakeUser = new Faker<User>(locale: "en_IND")
            .RuleFor(u => u.Id, f => f.Random.Number(1, limit))
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.MiddleName, f=> f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .Generate(limit);            

            return Ok(fakeUser);
        }
    }
}
