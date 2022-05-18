using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tasktest.Data;
using tasktest.DTOs;
using tasktest.Models;

namespace tasktest.Controllers
{
        public class MyClass1
        {
            public string? CateGory { get; set; }
            //public int? Id { get; set; }
        }
        public class MyClass2
        {
            public string? CateGory { get; set; }
            public string KeyName { get; set; }
        }

        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly DataContext _context;

            public UserController(DataContext context)
            {
                _context = context;
            }

     
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUsers()
            {
                List<userTest> usertest = await _context.userTest.ToListAsync();


                if (usertest.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseDto
                    {
                        Message = "Test List",
                        Success = true,
                        Payload = usertest
                    });
                }

                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "No Test List",
                    Success = false,
                    Payload = null
                });

            }
    
            [HttpPatch("UpdateUserTest")]
            public async Task<ActionResult<ResponseDto>> PutUserTest([FromBody] userTest input)
            {
                if (input.Category == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                    {
                        Message = " category is null",
                        Success = false,
                        Payload = null
                    });
                }
                if (input.Keyname == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                    {
                        Message = " keyname is null",
                        Success = false,
                        Payload = null
                    });
                }

               
                userTest usertest = await _context.userTest.Where(i => i.Category == input.Category).FirstOrDefaultAsync();
                if (usertest == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                    {
                        Message = "this usertest not listed your db",
                        Success = false,
                        Payload = null
                    });
                }

     
                usertest.Keyname = input.Keyname;
                usertest.Value = input.Value;
                usertest.Status = input.Status;
                _context.userTest.Update(usertest);
                bool isSaved = await _context.SaveChangesAsync() > 0;

                if (isSaved == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                    {
                        Message = "updating Unsuccesfull",
                        Success = false,
                        Payload = null
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "updating complete",
                    Success = true,
                    Payload = null
                });
            }


            [HttpPost("CreateEmployee")]
            public async Task<ActionResult<ResponseDto>> PostUserTest([FromBody] userTest input)
            {
                if (input.Keyname == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                    {
                        Message = " keyname is null",
                        Success = false,
                        Payload = null
                    });
                }

                userTest usertest = await _context.userTest.Where(i => i.Category == input.Category).FirstOrDefaultAsync();
                if (usertest != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                    {
                        Message = "already exist",
                        Success = false,
                        Payload = null
                    });
                }


                _context.userTest.Add(input);
                bool isSaved = await _context.SaveChangesAsync() > 0;

                if (isSaved == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                    {
                        Message = "creating error",
                        Success = false,
                        Payload = null
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "creating done",
                    Success = true,
                    Payload = new { input.Category }
                });
            }

            // DELETE
            [HttpDelete("DeleteEmployee")]
            public async Task<ActionResult<ResponseDto>> DeleteUserTest([FromBody] MyClass1 input)
            {
                if (input.CateGory == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                    {
                        Message = " category is null",
                        Success = false,
                        Payload = null
                    });
                }

                userTest usertest = await _context.userTest.Where(i => i.Category == input.CateGory).FirstOrDefaultAsync();
                if (usertest == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                    {
                        Message = "not exist your db",
                        Success = false,
                        Payload = null
                    });
                }

                _context.userTest.Remove(usertest);
                bool isSaved = await _context.SaveChangesAsync() > 0;

                if (isSaved == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                    {
                        Message = "deleting error",
                        Success = false,
                        Payload = null
                    });
                }

                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "deleted",
                    Success = true,
                    Payload = new { input.CateGory } // optional, can be null too like update
                });
            }

            private bool UserTestExists(string? category)
            {
                return _context.userTest.Any(e => e.Category == category);
            }


        }

}
