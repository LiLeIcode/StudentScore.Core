using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentScore.Extensions.Authorizations;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Repository;
using StudentScore.Service;
using Swashbuckle.AspNetCore.Filters;

namespace StudentScore.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.EnableEndpointRouting = false; }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //services.AddControllers();
            services.AddDbContext<DbContext>(option => option.UseSqlServer(@"Data Source=.;Initial Catalog=StudentScoreSqlServerCore;Integrated Security=True;uid=sa;pwd=root"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStudentInfoRepository, StudentInfoRepository>();
            services.AddScoped<IReportCardRepository, ReportCardRepository>();
            services.AddScoped<IStudentClassRepository, StudentClassRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStudentInfoService, StudentInfoService>();
            services.AddScoped<IStudentClassService, StudentClassService>();
            services.AddScoped<IReportCardService, ReportCardService>();


            services.AddControllers();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
                o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ConfigField.Iss,
                        ValidateAudience = true,
                        ValidAudience = ConfigField.Aud,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigField.Secret)),
                        ValidateLifetime = true,
                        RequireExpirationTime = true
                    };
                });
            
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRole",
                    option => option.RequireRole("admin"));
                options.AddPolicy("UserRole",policy=>policy.RequireRole("user","admin"));
                options.AddPolicy("OrdinaryRole", policy=>policy.RequireRole("ordinary"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = $"StudentScore.Core 接口文档-NetCore3.1",
                    Description = $"StudentScore.Core HttpApi v1",
                    Contact = new OpenApiContact()
                        {Name = "lilei", Email = "2424117373@qq.com", Url = new Uri("http://www.baidu.com")},
                    License = new OpenApiLicense() {Name = "lilei许可证", Url = new Uri("http://www.baidu.com")}
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath,
                    "D:/CSharperWeb/Lreandotnet.Core/StudentScore.Core/StudentScore.Core.xml");
                c.IncludeXmlComments(xmlPath);

                #region 加锁

                var apiSecurityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt 默认参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置（请求头）
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("oauth2", apiSecurityScheme);
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion

            });

            services.AddCors(c =>
            {
                //允许任何地址访问
                c.AddPolicy("cors", policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
            });

            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentScore.Core HttpApi v1");
                    c.RoutePrefix = string.Empty;
                });
            }



            app.UseAuthentication();//鉴权，检测有没有登录，登录的是谁

            app.UseRouting();

            app.UseCors("cors");

            app.UseAuthorization();//授权，检测用户用户是什么权限

            app.UseMvc();
        }
    }
}
