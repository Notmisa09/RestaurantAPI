using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Core.Application.Dto.Account;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Domain.Settings;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Restaurant.Persistance.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager,
                            IOptions<JWTSettings> jwtSettings,
                            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
        }


        //REGISTRATIONUSER
        public async Task<ServiceResult> RegisterUserAsync(RegisterRequest request, string origin, string UserRoles)
        {
            ServiceResult response = new()
            {
                HasError = false,
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if(userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = "The email is already taken";
            }

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if(userWithSameUserName != null )
            {
                response.HasError = true;
                response.Error = "The username is already taken";
            }

            var user = new AppUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if(!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred while trying to resgiter your user";
                return response;
            }
            return response;
        }

        //CONFIRMACCOUNT
        public async Task<string> ConfrimAccountAsync(string UserId, string token)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded) 
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred wgile confirming {user.Email}.";
            }
        }

        //FORGOTPASSWORD
        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ServiceResult response = new()
            {
                HasError = false,
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null) 
            {
                response.HasError = true;
                response.Error = $"No accounts registered with {request.Email}";
                return response;
            };

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred while reset password";
                return response;
            }

            return response;
        }

        //AUTHENTICATION
        public async Task<AuthenticationResponse> AuthetnticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null ) 
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email} ";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) 
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.JWTtoken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Reles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        //SINGOUT
        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #region Private Methods

        private async Task<string> SendVerificationEmailUri(AppUser user , string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles) 
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim("uid",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecutiryToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials : singingCredentials
                );

            return jwtSecutiryToken;
        }

        private RefreshJWT GenerateRefreshToken()
        {
            return new RefreshJWT
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow

            };
        }

        private string RandomTokenString()
        {
            using var rngCrytoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCrytoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }
        #endregion

    }
}
