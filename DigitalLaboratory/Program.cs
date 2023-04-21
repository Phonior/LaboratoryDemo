using DigitalLaboratory.BLL;
using Newtonsoft.Json.Serialization;

namespace DigitalLaboratory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //依赖注入
            //加入单例，实例创建后一直在内存保留
            builder.Services.AddSingleton<IUserBLL, UserBLL>();
            builder.Services.AddSingleton<IAO_ElectroCoilTempBLL, AO_ElectroCoilTempBLL>();
            builder.Services.AddSingleton<IAO_OxygenFlowBLL, AO_OxygenFlowBLL>();
            //加入单例，一次请求中实例一直保留
            //builder.Services.AddScoped();
            //使用实例后会立即注销
            //builder.Services.AddTransient();

            //Enable CORS
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            //builder.Services.AddCors(c => c.AddPolicy("any", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

            //JSON Serializer
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());

            //实例化
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //启动权限验证
            app.UseAuthorization();

            //WebAPI映射到Controller
            app.MapControllers();

            app.Run();
        }
    }
}