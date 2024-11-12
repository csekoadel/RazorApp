using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileUploadApp.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
