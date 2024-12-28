using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebOdev.Controllers
{
    public class YapayZekaController : Controller
    {
        private readonly HttpClient _httpClient;

        public YapayZekaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
        }

        [HttpGet]
        [Authorize(Roles = "Musteri, Calisan, Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Musteri, Calisan, Admin")]
        public async Task<IActionResult> Gonder(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                TempData["Message"] = "Lütfen bir dosya seçin.";
                return RedirectToAction("Index");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var imageBytes = stream.ToArray();
                var base64Image = Convert.ToBase64String(imageBytes);

                var request = new
                {
                    messages = new[]
                    {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = "Give me a haircut idea with only 1 sentence please."
                            },
                            new
                            {
                                type = "image_url",
                                image_url = new
                                {
                                    url = $"data:image/jpeg;base64,{base64Image}",
                                }
                            }
                        }
                    }
                },
                    model = "llama-3.2-11b-vision-preview",
                    temperature = 1,
                    max_tokens = 1024,
                    top_p = 1,
                    stream = false,
                    stop = (object?)null
                };

                string jsonString = JsonSerializer.Serialize(request);
                HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Yapay zeka API'sine bağlanılamadı.";
                    return RedirectToAction("Index");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseContent);
                var data = jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content");

                var serializedString = JsonSerializer.Serialize(data);
                var deserializedString = JsonSerializer.Deserialize<string>(serializedString);
                TempData["Sonuc"] = deserializedString;
                return RedirectToAction("Sonuc");
            }
            
        }

        [Authorize(Roles = "Musteri, Calisan, Admin")]
        [HttpGet]
        public IActionResult Sonuc()
        {
            return View();
        }
    }
}
