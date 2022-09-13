using ELISExtension;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
builder.Services.AddMvc();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseDefaultFiles(new DefaultFilesOptions()
{
    DefaultFileNames = new List<string>() { "index.html" }
});

app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

dynamic? o = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
AppConfig.ELISAPIURL = o.ELISAPIURL;
AppConfig.ELISAPIKEY = o.ELISAPIKEY;
AppConfig.ELISAPISECRET = o.ELISAPISECRET;

app.Run("http://0.0.0.0:80");
