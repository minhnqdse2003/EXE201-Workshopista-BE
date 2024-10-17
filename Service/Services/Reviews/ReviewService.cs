using AutoMapper;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;
using Service.Interfaces;
using Service.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateReview(ReviewCreateModel model, string token)
        {
            var userEmail = JwtDecode.DecodeToken(token, ClaimTypes.Email);
            var user = await _unitOfWork.Users.GetUserByEmail(userEmail);
            Review newReview = _mapper.Map<Review>(model);
            newReview.ParticipantId = user.UserId;
            await _unitOfWork.Reviews.Add(newReview);
        }

        public async Task<List<Review>> GetAllReviewOfWorkshop(Guid workshopId)
        {
            var list = await _unitOfWork.Reviews.GetReviewByWorkshopId(workshopId);
            return list;
        }
    }
}
