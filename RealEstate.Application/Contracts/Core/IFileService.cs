using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Contracts.Core
{
    public interface IFileService
    {
        Task<string> CreateImage(IFormFile img);
        Task<string> DeleteImage(string url);
    }
}
