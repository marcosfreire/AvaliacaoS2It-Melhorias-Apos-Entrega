using System;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Desafio.s2.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Desafio.s2.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Desafio.s2.Domain.Core.Notifications;
using Desafio.s2.Infra.CrossCutting.Identity.Models;
using Desafio.s2.Infra.CrossCutting.Identity.Authorization;
using Desafio.s2.Infra.CrossCutting.Identity.Models.AccountViewModels;

namespace Desafios2.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMediatorHandler _mediator;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(INotificationHandler<DomainNotification> notifications,
                                    IUser user,
                                    IMediatorHandler mediator,
                                    UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    ILoggerFactory loggerFactory,
                                    TokenDescriptor tokenDescriptor) : base(notifications, user, mediator)
        {
            _mediator = mediator;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenDescriptor = tokenDescriptor;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login() => Response("Login");

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                var response = GerarTokenUsuario(model);
                return Response(response);
            }

            NotificarErro(result.ToString(), "Falha ao realizar o login");
            return Response(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // todo :  criar jwt token
                return Response("Usuario criado com sucesso!");
            }

            AdicionarErrosIdentity(result);
            return Response(model);
        }
        
        private async Task<object> GerarTokenUsuario(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            AdicionarClaims(user, userClaims);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = BuildSecutiryToken(identityClaims, handler);

            // todo : recuperar usuario da base para incluir informacoes no payload ( analisar se informacoes precisam ser consumidas pelo front )
            var response = new
            {
                access_token = handler.WriteToken(securityToken),
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }

        private SecurityToken BuildSecutiryToken(ClaimsIdentity identityClaims, JwtSecurityTokenHandler handler)
        {
            var signingConf = new SigningCredentialsConfiguration();

            return handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });
        }

        private static void AdicionarClaims(ApplicationUser user, System.Collections.Generic.IList<Claim> userClaims)
        {
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        }

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}