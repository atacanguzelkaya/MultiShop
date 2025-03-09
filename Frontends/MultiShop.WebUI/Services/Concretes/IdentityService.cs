using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.Dtos.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using System.Security.Claims;
using MultiShop.WebUI.Settings;
using Duende.IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MultiShop.WebUI.Services.Concretes
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<bool> GetRefreshToken()
        {
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                RefreshToken = refreshToken,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            var authenticationToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            };

            var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            var properties = result.Properties;
            properties.StoreTokens(authenticationToken);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

            return true;
        }

        public async Task<bool> SignIn(SignInDto signInDto)
        {
            try
            {
                var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = _serviceApiSettings.IdentityServerUrl,
                    Policy = new DiscoveryPolicy
                    {
                        RequireHttps = false
                    }
                });

                if (discoveryEndPoint.IsError)
                {
                    throw new Exception($"Discovery document error: {discoveryEndPoint.Error}");
                }

                var passwordTokenRequest = new PasswordTokenRequest
                {
                    ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                    ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                    UserName = signInDto.Username,
                    Password = signInDto.Password,
                    Address = discoveryEndPoint.TokenEndpoint
                };

                var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

                if (token.IsError)
                {
                    throw new Exception($"Token request error: {token.Error}");
                }

                var userInfoRequest = new UserInfoRequest
                {
                    Token = token.AccessToken,
                    Address = discoveryEndPoint.UserInfoEndpoint
                };

                var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

                if (userValues.IsError)
                {
                    throw new Exception($"User info request error: {userValues.Error}");
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authenticationProperties = new AuthenticationProperties();

                authenticationProperties.StoreTokens(new List<AuthenticationToken>()
        {
            new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
            new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
            new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
            }
        });

                authenticationProperties.IsPersistent = false;

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
