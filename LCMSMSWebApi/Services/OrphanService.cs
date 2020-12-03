using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Helpers;
using LCMSMSWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Services
{
    public class OrphanService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PictureService _pictureService;
        private readonly IPictureStorageService _pictureStorageService;
        private readonly string _placeholderPic = "no_image_found_300x300.jpg";

        public OrphanService(ApplicationDbContext dbContext, 
            PictureService pictureService,
            IPictureStorageService pictureStorageService)
        {
            _dbContext = dbContext;
            _pictureService = pictureService;
            _pictureStorageService = pictureStorageService;
        }

        public async Task<PagedList<Orphan>> GetOrphansPagedListAsync(OrphanParameters orphanParameters)
        {

            // TODO
            // check for search, filter, sort, etc.

            return await PagedList<Orphan>
                .ToPagedListAsync(_dbContext.Orphans
                .AsNoTracking()
                .Include("Guardian")
                .Include("Narrations")
                .Include("Academics")
                .OrderBy(o => o.LastName),
                orphanParameters.PageNumber, orphanParameters.PageSize);
        }

        public object GetMetaData(PagedList<Orphan> orphans)
        {
            return new
            {
                orphans.TotalCount,
                orphans.PageSize,
                orphans.PageNumber,
                orphans.HasNext,
                orphans.HasPrevious
            };
        }

        public void SetProfilePicUrlForOrphans(List<OrphanDTO> orphans)
        {
            orphans.ForEach(orphan =>
            {
                orphan.ProfilePicUrl = string.IsNullOrWhiteSpace(orphan.ProfilePicFileName)
              ? $"{ _pictureStorageService.BaseUrl }{ _placeholderPic }"
              : $"{ _pictureStorageService.BaseUrl }{ orphan.ProfilePicFileName }";
            });
        }

    }
}
