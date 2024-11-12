using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace FileUploadApp.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //IFormFile reprezenátlja a HTTP-ből érkezik , és saját adatfolyammal rendelkezik: a biztosított streamet használja, ne tölti be teljes egészében a memóriába
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            // Fájl célhelyének beállítása a wwwroot/uploads mappában
            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var filePath = Path.Combine(uploadsPath, file.FileName);

            // Fájl mentése közvetlenül a lemezre, memóriában való tárolás nélkül, és ő itt egy fájlt hozz létre amegadott elérési útvonalon
            using var fileStream = new FileStream(filePath, FileMode.Create);
            //file az adatafolyamból közvetlenül a fileStream adatfolyamra másolom át

            // Az adatfolyam közvetlenül a bufferedStream-be másolása, hogy elkerüljük a memória túlterhelését
            using var bufferedStream = new BufferedStream(fileStream, 8192);
            await file.CopyToAsync(bufferedStream);

            // Visszaadjuk a relatív útvonalat
            return $"/uploads/{file.FileName}";
        }
    }
}
