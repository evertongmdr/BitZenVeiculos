using AutoMapper;
using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using static BitZenVeiculos.Domain.DTOs.UserDTO;

namespace BitZenVeiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [AllowAnonymous]

        public async Task <IActionResult> CreateUser([FromBody] User user)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
 

            if (await _userRepository.EmailExists(user.Email))
            {
                ModelState.AddModelError("", $"O e-mail {user.Email} já está cadastrado, tente outro");
                return StatusCode(409, ModelState);
            }
                
            if (!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao salvar usuário {user.FullName} ");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUser", new { userId = user.Id }, user);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(LoginResponseDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [AllowAnonymous]

        public IActionResult LoginUser([FromBody] LoginRequestDTO userLogin)
        {
            var userEntity = _userRepository.LoginUser(userLogin);

            if (userEntity == null)
                return NotFound();

            var loginResponse =  _mapper.Map<LoginResponseDTO>(userEntity);
            loginResponse.token = GenerateToken();

            return Ok(loginResponse);
        }

        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
