using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileUploadApp.Services;
using System.Threading.Tasks;

namespace FileUploadApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFileUploadService _fileUploadService;

        public IndexModel(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public string? FilePath { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null)
            {
                FilePath = await _fileUploadService.UploadFileAsync(Upload);
            }
            return Page();
        }
    }
}
