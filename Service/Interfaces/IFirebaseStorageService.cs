using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFirebaseStorageService
    {
        Task<List<string>> UploadFile(List<IFormFile> file);
    }
}
