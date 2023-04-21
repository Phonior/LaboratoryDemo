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

            //����ע��
            //���뵥����ʵ��������һֱ���ڴ汣��
            builder.Services.AddSingleton<IUserBLL, UserBLL>();
            builder.Services.AddSingleton<IAO_ElectroCoilTempBLL, AO_ElectroCoilTempBLL>();
            builder.Services.AddSingleton<IAO_OxygenFlowBLL, AO_OxygenFlowBLL>();
            //���뵥����һ��������ʵ��һֱ����
            //builder.Services.AddScoped();
            //ʹ��ʵ���������ע��
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

            //ʵ����
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //����Ȩ����֤
            app.UseAuthorization();

            //WebAPIӳ�䵽Controller
            app.MapControllers();

            app.Run();
        }
    }
}