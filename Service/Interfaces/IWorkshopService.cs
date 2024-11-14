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
        Task<ApiResponse<IEnumerable<WorkShopResponseModel>>> GetAll();

        ApiResponse<WorkShopResponseModel> GetWorkshopById(Guid id);

        Task<ApiResponse<WorkShopResponseModel>> AddWorkshop(WorkShopCreateRequestModel workshopCreateDto,string email);
        Task<ApiResponse<bool>> DeleteWorkshop(string id);
        Task<ApiResponse<WorkShopResponseModel>> UpdateWorkshop(WorkShopUpdateRequestModel workshopUpdateDto,string id);
        Task<ApiResponse<List<WorkshopImageResponseModel>>> GetWorkShopBanner();
        Task<ApiResponse<bool>> UpdateWorkshopImageStatus(string imageId);
    }

}
