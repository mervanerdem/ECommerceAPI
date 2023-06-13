using AutoMapper;
using Azure;
using ECommerceAPI.Base;
using ECommerceAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Business
{
    public class BaseService<TEntity, TRequest, TResponse> : IBaseService<TEntity, TRequest, TResponse> where TEntity : BaseModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public ApiResponse Delete(int Id)
        {
            try
            {
                var entity = unitOfWork.Repository<TEntity>().GetByIdAsNoTracking(Id);
                if (entity is null)
                {
                    //Log.Warning("Record not found for Id " + Id);
                    return new ApiResponse("Record not found");
                }

                unitOfWork.Repository<TEntity>().DeleteById(Id);
                unitOfWork.Complete();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Delete Exception");
                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse<List<TResponse>> GetAll()
        {
            try
            {
                var entityList = unitOfWork.Repository<TEntity>().GetAll();
                var mapped = mapper.Map<List<TEntity>, List<TResponse>>(entityList);
                return new ApiResponse<List<TResponse>>(mapped);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "GetAll Exception");
                return new ApiResponse<List<TResponse>>(ex.Message);
            }
        }

        public ApiResponse<TResponse> GetById(int id)
        {
            try
            {
                var entity = unitOfWork.Repository<TEntity>().GetById(id);
                if (entity is null)
                {
                    //Log.Warning("Record not found for Id " + id);
                    return new ApiResponse<TResponse>("Record not found");
                }

                var mapped = mapper.Map<TEntity, TResponse>(entity);
                return new ApiResponse<TResponse>(mapped);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "GetById Exception");
                return new ApiResponse<TResponse>(ex.Message);
            }
        }

        public ApiResponse Insert(TRequest request)
        {
            try
            {
                var entity = mapper.Map<TRequest, TEntity>(request);
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = "sim@sim.com";

                unitOfWork.Repository<TEntity>().Insert(entity);
                unitOfWork.Complete();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Insert Exception");
                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse Update(int Id, TRequest request)
        {
            try
            {
                var entity = mapper.Map<TRequest, TEntity>(request);

                var exist = unitOfWork.Repository<TEntity>().GetByIdAsNoTracking(Id);
                if (exist is null)
                {
                   //Log.Warning("Record not found for Id " + Id);
                    return new ApiResponse("Record not found");
                }

                entity.Id = Id;
                entity.UpdatedAt = DateTime.UtcNow;

                unitOfWork.Repository<TEntity>().Update(entity);
                unitOfWork.Complete();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
               // Log.Error(ex, "Update Exception");
                return new ApiResponse(ex.Message);
            }
        }
    }
}
