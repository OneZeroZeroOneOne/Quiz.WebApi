using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Tests.WebApi.Bll.Services;
using Tests.WebApi.Dal.Contexts;
using Tests.WebApi.Dal.Models;

namespace Tests.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly AttachmentPathProvider _attachmentPathProvider;
        private readonly MainContext _context;

        public AvatarController(MainContext context, IWebHostEnvironment appEnvironment, AttachmentPathProvider attachmentPathProvider)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _attachmentPathProvider = attachmentPathProvider;
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            var ext = Path.GetExtension(uploadedFile.FileName);
            var fileName = DateTime.UtcNow.ToFileTimeUtc();

            string path = Path.Combine(Path.Combine(_attachmentPathProvider.GetPath(), "Files"), fileName + ext);
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            Avatar file = new Avatar { Path = "Files/" + fileName + ext,  Name = uploadedFile.FileName };

            _context.Add(file);

            await _context.SaveChangesAsync();

            return Ok(file);
        }
    }
}
