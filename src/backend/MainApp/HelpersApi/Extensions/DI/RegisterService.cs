using System.Reflection;
using Application.Contracts.IRepositories;
using Application.Contracts.IRepositories.IBaseRepository;
using Application.Contracts.IRepositories.IBaseRepository.ICrud;
using Application.Contracts.IServices;
using FluentValidation;
using Infrastructure.DataAccess;
using Infrastructure.Extensions.Authentication;
using Infrastructure.ImplementationContract.Repositories;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Infrastructure.ImplementationContract.Repositories.BaseRepository.Crud;
using Infrastructure.ImplementationContract.Services;
using MainApp.HelpersApi.Extensions.FluentValidation;
using MainApp.HelpersApi.Extensions.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MainApp.HelpersApi.Extensions.DI;

public static class RegisterService
{
    public static IServiceCollection AddServices(this WebApplicationBuilder builder)
    {
        //config for authentication in swagger
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the Bearer scheme \r\n\r\n" +
                              "Example:\"Bearer 1234aaabbbccc\" "
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            // Добавляем требования безопасности для всех эндпоинтов
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    new string[] { }
                }
            });
        });


        //registration controller
        builder.Services.AddControllers();


        builder.Services.AddDbContext<DataContext>(x =>
        {
            x.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            x.LogTo(Console.WriteLine);
        });

        //fluent validation ? for automatic validation ,need use mediatr.
        //builder.Services.AddValidatorsFromAssembly(typeof(Application.Application).Assembly);


        //add authentication methods
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthenticationService.GetSymmetricSecurityKey(builder.Configuration["JWT:key"]!),
                    ValidateIssuerSigningKey = true,
                };
            });

        //add  authorization
        builder.Services.AddAuthorization();


        //registration generic repository
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IGenericAddRepository<>), typeof(GenericAddRepository<>));
        builder.Services.AddScoped(typeof(IGenericUpdateRepository<>), typeof(GenericUpdateRepository<>));
        builder.Services.AddScoped(typeof(IGenericDeleteRepository<>), typeof(GenericDeleteRepository<>));
        builder.Services.AddScoped(typeof(IGenericFindRepository<>), typeof(GenericFindRepository<>));

        //registration repository
        builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IChatRepository, ChatRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        //registration services
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<IChatService, ChatService>();
        builder.Services.AddScoped<ICourierService, CourierService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<Seeder>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IUserService, UserService>();

        //registration validation
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            x.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // добавляем сервисы CORS
        builder.Services.AddCors();

        return builder.Services;
    }

    public static async Task<WebApplication> UseMiddlewares(this WebApplication app)
    {
        using (IServiceScope scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            await seeder.InitialAsync();
        }

        try
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseStaticFiles();
            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();
            });
            await app.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return app;
    }
}