using HotelManagerFull.Share.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HotelManagerFull.Web.Infrastructure
{
    /// <summary>
    /// ConfigureService
    /// </summary>
    public static class ConfigureService
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; //get this from somewhere secure
        private static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        /// <summary>
        /// InitAuthentication
        /// </summary>
        /// <param name="services"></param>
        public static void InitAuthentication(this IServiceCollection services)
        {
            var configuration = ServiceLocator.Current.GetInstance<IConfiguration>();
            // Register the ConfigurationBuilder instance of FacebookAuthSetting
            //services.Configure<FacebookAuthSettings>(configuration.GetSection(nameof(FacebookAuthSettings)));
            // Get options from app settings
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var a = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SigningKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));
            });

            var appSettingOptions = configuration.GetSection(nameof(AppSetting));
            services.Configure<AppSetting>(options =>
            {
                options.DomainFile = appSettingOptions[nameof(AppSetting.DomainFile)];
                options.ImageBase = appSettingOptions[nameof(AppSetting.ImageBase)];
            });

            // add identity
            //var builderDuke = services.AddIdentityCore<UserProfile>(o =>
            //{
            //    // configure identity options
            //    o.Password.RequireDigit = false;
            //    o.Password.RequireLowercase = false;
            //    o.Password.RequireUppercase = false;
            //    o.Password.RequireNonAlphanumeric = false;
            //    o.Password.RequiredLength = 6;
            //}).AddRoles<IdentityRole>();

            //builderDuke = new IdentityBuilder(builderDuke.UserType, typeof(IdentityRole), builderDuke.Services);
            //builderDuke.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
            //services.Configure<FacebookAuthSettings>(configuration.GetSection(nameof(FacebookAuthSettings)));
        }
    }
}
