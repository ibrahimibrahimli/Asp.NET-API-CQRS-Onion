using Application.Abstractions;
using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.Facebook;
using Application.Exceptions.User;
using Application.Helpers;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }
        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        FullName = name,
                        UserName = email

                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);

                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
                return token;
            }
            throw new Exception("Invalid external authentification");
        }
        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings.Facebook.Client_ID"]}&client_secret={_configuration["ExternalLoginSettings.Facebook.Client_Secret"]}&grant_type=client_credentials");
            FacebookTokenDto? facebookTokenDto = JsonSerializer.Deserialize<FacebookTokenDto>(accessTokenResponse);
            Console.WriteLine(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient
                .GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookTokenDto?.AccessToken}");

            FacebookUserAccessTokenValidator? userAccessTokenValidator =
                JsonSerializer.Deserialize<FacebookUserAccessTokenValidator>(userAccessTokenValidation);

            if (userAccessTokenValidator.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");
                FacebookUserInfoResponse? response = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var userLoginInfo = new UserLoginInfo("FACEBOOK", userAccessTokenValidator.Data.UserId, "FACEBOOK");

                AppUser? user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

                return await CreateUserExternalAsync(user, response.Email, response.Name, userLoginInfo, accessTokenLifeTime);

            }
            throw new Exception("Invalid external authentification");

        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings.Google.App_Id"] }
            };
            Payload payload = await ValidateAsync(idToken, settings);
            var userLoginInfo = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            AppUser? user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Email, payload.Name, userLoginInfo, accessTokenLifeTime);
        }


        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifetime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;

            }
            throw new AuthentificationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);
            if (user is not null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            else
                throw new NotFoundUserException();
        }

        public async Task ResetPasswordAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                //byte[] resetTokenBytes = Encoding.UTF8.GetBytes(passwordResetToken);
                //passwordResetToken = WebEncoders.Base64UrlEncode(resetTokenBytes);

                string encodedToken = passwordResetToken.UrlEncode();

                await _mailService.SendResetPasswordMailAsync(email, user.Id, encodedToken);
            }
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //string decodedToken = Encoding.UTF8.GetString(tokenBytes);
                string decodedToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedToken);
            }
            return false;
        }
    }
}
