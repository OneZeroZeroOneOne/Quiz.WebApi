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
    public class ResumeController : ControllerBase
    {
        private readonly AttachmentPathProvider _attachmentPathProvider;
        private readonly MainContext _context;

        public ResumeController(MainContext context, AttachmentPathProvider attachmentPathProvider)
        {
            _context = context;
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

            var file = new Resume { Path = "Files/" + fileName + ext, Name = uploadedFile.FileName };

            _context.Add(file);

            await _context.SaveChangesAsync();

            return Ok(file);
        }
    }
}
