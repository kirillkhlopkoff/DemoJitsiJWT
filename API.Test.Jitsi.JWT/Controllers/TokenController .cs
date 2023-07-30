using API.Test.Jitsi.JWT.Data;
using API.Test.Jitsi.JWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static API.Test.Jitsi.JWT.PrivateKeyHelper;

namespace API.Test.Jitsi.JWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TokenController(ApplicationDbContext context, UserManager<UserModel> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        // POST api/token
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] TokenRequestModel model)
        {
            UserModel currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            try
            {
                /*// Генерация и сохранение новой пары ключей (PKCS#8 формат).
                PrivateKeyHelper.GenerateAndSavePrivateKey("D:/private_key.pem", PKType.PKCS8);*/

                // Чтение закрытого ключа из файла. (изменено на PKCS1, он читает .pem)
                var rsaPrivateKey = PrivateKeyHelper.ReadPrivateKeyFromFile("D:/testKey.pem", PKType.PKCS1);
                string apiKey = "vpaas-magic-cookie-856f5d9e781e4301b2f71f256ea4911e/f94ef3";
                string appId = "vpaas-magic-cookie-856f5d9e781e4301b2f71f256ea4911e";

                // Создание нового JaaSJwtBuilder и настройка параметров токена, а затем подпись с использованием закрытого ключа.
                var token = JaaSJwtBuilder.Builder()
                                .WithDefaults()
                                .WithExpTime(DateTime.UtcNow.AddSeconds(JaaSJwtBuilder.EXP_TIME_DELAY_SEC))
                                .WithNbfTime(DateTime.UtcNow.AddSeconds(-JaaSJwtBuilder.NBF_TIME_DELAY_SEC))
                                .WithLiveStreamingEnabled(true)
                                .WithRecordingEnabled(true)
                                .WithOutboundCallEnabled(true)
                                .WithTranscriptionEnabled(true)
                                .WithModerator(model.isModerator = currentUser.isModerator)
                                .WithRoomName("*")
                                .WithUserId(System.Guid.NewGuid().ToString())
                                .WithApiKey(model.ApiKey = apiKey)
                                .WithUserName(model.UserName = currentUser.UserName)
                                .WithUserEmail(model.UserEmail = currentUser.Email)
                                .WithUserAvatar(model.UserAvatar = currentUser.UserAvatar)
                                .WithAppID(model.AppID = appId)
                                .WithModerator(model.isModerator = currentUser.isModerator)
                                .SignWith(rsaPrivateKey);

                var tokenValue = new TokenValue
                {
                    Id = Guid.NewGuid(),
                    Value = token
                };
                _context.TokenValues.Add(tokenValue);
                await _context.SaveChangesAsync();

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/token/allIds
        [HttpGet("allIds")]
        public IActionResult GetAllTokenIds()
        {
            var tokenIds = _context.TokenValues.Select(t => t.Id).ToList();
            return Ok(tokenIds);
        }

        // GET api/token/{id}
        [HttpGet("{id}")]
        public IActionResult GetTokenById(Guid id)
        {
            var tokenValue = _context.TokenValues.Find(id);
            if (tokenValue == null)
            {
                return NotFound();
            }

            return Ok(tokenValue.Value);
        }


        //************HARDCOOOOOOOOOOOOOOOOOODEEEEEEEEEEEEEEE**************************

        // POST api/token/doctor
        [HttpPost("doctor")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GenerateTokenDoctorAsync([FromBody] TokenRequestModel model)
        {
            UserModel currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            try
            {
                /*// Генерация и сохранение новой пары ключей (PKCS#8 формат).
                PrivateKeyHelper.GenerateAndSavePrivateKey("D:/private_key.pem", PKType.PKCS8);*/

                // Чтение закрытого ключа из файла. (изменено на PKCS1, он читает .pem)
                var rsaPrivateKey = PrivateKeyHelper.ReadPrivateKeyFromFile("D:/testKey.pem", PKType.PKCS1);
                string apiKey = "vpaas-magic-cookie-856f5d9e781e4301b2f71f256ea4911e/f94ef3";
                string appId = "vpaas-magic-cookie-856f5d9e781e4301b2f71f256ea4911e";

                // Создание нового JaaSJwtBuilder и настройка параметров токена, а затем подпись с использованием закрытого ключа.
                var token = JaaSJwtBuilder.Builder()
                                /*.WithDefaults()*/
                                .WithExpTime(DateTime.UtcNow.AddSeconds(JaaSJwtBuilder.EXP_TIME_DELAY_SEC))
                                .WithNbfTime(DateTime.UtcNow.AddSeconds(-JaaSJwtBuilder.NBF_TIME_DELAY_SEC))
                                .WithLiveStreamingEnabled(true)
                                .WithRecordingEnabled(true)
                                .WithOutboundCallEnabled(true)
                                .WithTranscriptionEnabled(true)
                                .WithModerator(model.isModerator = true)
                                .WithRoomName("*")
                                .WithUserId(System.Guid.NewGuid().ToString())
                                .WithApiKey(model.ApiKey = apiKey)
                                .WithUserName(model.UserName = "Doctor")
                                .WithUserEmail(model.UserEmail = "doctor@gmail.com")
                                .WithUserAvatar(model.UserAvatar = "https://naked-science.ru/wp-content/uploads/2016/04/article_entweeklyhires02.jpg")
                                .WithAppID(model.AppID = appId)
                                /*.WithModerator(model.isModerator = currentUser.isModerator)*/
                                .SignWith(rsaPrivateKey);

                var tokenValue = new TokenValue
                {
                    Id = Guid.NewGuid(),
                    Value = token
                };
                _context.TokenValues.Add(tokenValue);
                await _context.SaveChangesAsync();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/token/doctor
        [HttpPost("pacient")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GenerateTokenPacientAsync([FromBody] TokenRequestModel model)
        {
            UserModel currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            try
            {
                /*// Генерация и сохранение новой пары ключей (PKCS#8 формат).
                PrivateKeyHelper.GenerateAndSavePrivateKey("D:/private_key.pem", PKType.PKCS8);*/

                // Чтение закрытого ключа из файла. (изменено на PKCS1, он читает .pem)
                var rsaPrivateKey = PrivateKeyHelper.ReadPrivateKeyFromFile("D:/testKey.pem", PKType.PKCS1);
                string apiKey = "vpaas-magic-cookie-856f5d9e781e4301b2f71f256ea4911e/f94ef3";
                string appId = "vpaas-magic-cookie-856f5d9e781e4301b2f71f256ea4911e";

                // Создание нового JaaSJwtBuilder и настройка параметров токена, а затем подпись с использованием закрытого ключа.
                var token = JaaSJwtBuilder.Builder()
                                /*.WithDefaults()*/
                                .WithExpTime(DateTime.UtcNow.AddSeconds(JaaSJwtBuilder.EXP_TIME_DELAY_SEC))
                                .WithNbfTime(DateTime.UtcNow.AddSeconds(-JaaSJwtBuilder.NBF_TIME_DELAY_SEC))
                                .WithLiveStreamingEnabled(true)
                                .WithRecordingEnabled(true)
                                .WithOutboundCallEnabled(true)
                                .WithTranscriptionEnabled(true)
                                .WithModerator(model.isModerator = false)
                                .WithRoomName("*")
                                .WithUserId(System.Guid.NewGuid().ToString())
                                .WithApiKey(model.ApiKey = apiKey)
                                .WithUserName(model.UserName = "Pacient")
                                .WithUserEmail(model.UserEmail = "pacient@gmail.com")
                                .WithUserAvatar(model.UserAvatar = "https://www.factroom.ru/wp-content/uploads/2017/08/Depositphotos_138447330_l-2015-730x487.jpg")
                                .WithAppID(model.AppID = appId)
                                /*.WithModerator(model.isModerator = currentUser.isModerator)*/
                                .SignWith(rsaPrivateKey);

                var tokenValue = new TokenValue
                {
                    Id = Guid.NewGuid(),
                    Value = token
                };
                _context.TokenValues.Add(tokenValue);
                await _context.SaveChangesAsync();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
