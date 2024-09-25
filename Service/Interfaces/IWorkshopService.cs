using Repository.Models;
using Service.Models;
using Service.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IWorkshopService
    {
        Task<ApiResponse<IEnumerable<WorkShopResponseModel>>> GetFilter(WorkshopFilterModel filterModel);
        ApiResponse<WorkShopResponseModel> GetWorkshopById(Guid id);
        Task<ApiResponse<WorkShopResponseModel>> AddWorkshop(WorkShopCreateRequestModel workshopCreateDto,string email);
    }

}
