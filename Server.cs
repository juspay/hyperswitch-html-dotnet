using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace HyperswitchExample
{
  public class Program
  {
    public static void Main(string[] args)
    {
      WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://0.0.0.0:4242")
        .UseWebRoot("public")
        .UseStartup<Startup>()
        .Build()
        .Run();
    }
  }

  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().AddNewtonsoftJson();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
      app.UseRouting();
      app.UseStaticFiles();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

  }

  [Route("create-payment")]
  [ApiController]
  public class PaymentIntentApiController : Controller
  {

    [HttpPost]
    public async Task<ActionResult> CreateAsync(PaymentIntentCreateRequest request)
    {
        string HYPER_SWITCH_API_KEY = "HYPERSWITCH_API_KEY";
        string HYPER_SWITCH_API_BASE_URL = "https://sandbox.hyperswitch.io/payments";

        var payload = new { amount = CalculateOrderAmount(request.Items), currency = "USD" };

        using (var httpClient = new System.Net.Http.HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("api-key", HYPER_SWITCH_API_KEY);

            var jsonPayload = JsonConvert.SerializeObject(payload);

            var content = new System.Net.Http.StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(HYPER_SWITCH_API_BASE_URL, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                return Json(new {client_secret = responseData.client_secret});
            }
            else
            {
                return Json(new {error = "Request failed"});
            }
        }
    }

    private int CalculateOrderAmount(Item[] items)
    {
      return 1400;
    }

    public class Item
    {
      [JsonProperty("id")]
      public string Id { get; set; }
    }

    public class PaymentIntentCreateRequest
    {
      [JsonProperty("items")]
      public Item[] Items { get; set; }
    }
  }
}