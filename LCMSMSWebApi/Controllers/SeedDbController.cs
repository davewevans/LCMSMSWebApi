using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace LCMSMSWebApi.Controllers
{
    public class OrphanProfilePictureTem9uhtieuhgt
    {
        public int OrphanId { get; set; }
        public int PicId { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SeedDbController : ControllerBase
    {
        private readonly LocalApplicationDbContext _srcDbContext;
        private readonly ApplicationDbContext _destDbContext;

        private readonly IWebHostEnvironment _env;

       

        //private PaulDbContext _paulDbContext;
        public SeedDbController(LocalApplicationDbContext dbContext, 
            ApplicationDbContext destDbContext,
            IWebHostEnvironment env)
        {
            _srcDbContext = dbContext;
            _destDbContext = destDbContext;

            // _paulDbContext = paulDbContext;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            // PopulateRealData();

            // PopulateProfilePicIds();

            // var seeder = new DummyDataSeeder(_dbContext, _env);
            //seeder.SeedAllDummyData();

            // seeder.SeedGuardians();
            // seeder.SeedSponsors();
            // await seeder.SeedNarrations();
            // seeder.SeedPictures();
            // seeder.SeedOrphanSponsors();

            // seeder.SeedAcademicsForeignKey();

            // BuildLocalOrphanDB();

            // BuildAzureOrphanDB();


            //var orphanPics = _dbContext.Pictures;
            //foreach (var pic in orphanPics)
            //{
            //    _dbContext.Pictures.Remove(pic);
            //}
            //await _dbContext.SaveChangesAsync();

            return Ok();
        }


        private void PopulateProfilePicIds()
        {
            //var orphansSrc = _srcDbContext.Orphans.ToList();
            var orphansDest = _destDbContext.Orphans.ToList();
         
            var orphanProfilePics = new List<OrphanProfilePictureTem9uhtieuhgt>()
            {
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1120, PicId=1 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1301, PicId=2 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1239, PicId=4 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1303, PicId=6 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1024, PicId=8 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1120, PicId=10 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1223, PicId=12 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1138, PicId=13 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1260, PicId=14 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1167, PicId=16 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1154, PicId=19 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1294, PicId=22 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1135, PicId=25 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1282, PicId=27 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1242, PicId=29 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1010, PicId=31 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1098, PicId=33 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1055, PicId=35 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1258, PicId=37 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1157, PicId=40 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1287, PicId=42 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1227, PicId=45 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1001, PicId=48 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1076, PicId=50 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1086, PicId=52 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1186, PicId=53 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1013, PicId=55 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1197, PicId=57 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1062, PicId=59 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1256, PicId=61 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1212, PicId=62 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1226, PicId=65 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1042, PicId=67 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1292, PicId=69 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1145, PicId=72 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1224, PicId=74 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1291, PicId=77 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1189, PicId=79 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1214, PicId=81 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1007, PicId=84 },
                new OrphanProfilePictureTem9uhtieuhgt  {OrphanId=1098, PicId=86 },              
            };


            foreach (var orphanProfilePic in orphanProfilePics)
            {
                var orphan = orphansDest.FirstOrDefault(x => x.OrphanID == orphanProfilePic.OrphanId);
                orphan.ProfilePictureID = orphanProfilePic.PicId;
            }

            _destDbContext.SaveChanges();
        }

        private void PopulateRealData()
        {
            var orphans = _srcDbContext.Orphans.ToList();
            var academics = _srcDbContext.Academics.ToList();
            var narrations = _srcDbContext.Narrations.ToList();
            var guardians = _srcDbContext.Guardians.ToList();


            var sponsors = _srcDbContext.Sponsors.ToList();
            var pictures = _srcDbContext.Pictures.ToList();
            var documents = _srcDbContext.Documents.ToList();
            var orphanSponsors = _srcDbContext.OrphanSponsors.ToList();
            var orphanProfilePics = _srcDbContext.OrphanProfilePics.ToList();



            foreach (var orphan in orphans)
            {
                _destDbContext.Orphans.Add(new Orphan
                {
                    OrphanID = orphan.OrphanID,
                    FirstName = orphan.FirstName,
                    MiddleName = orphan.MiddleName,
                    LastName = orphan.LastName,
                    Gender = orphan.Gender,
                    LCMStatus = orphan.LCMStatus,
                    ProfileNumber = orphan.ProfileNumber,
                    DateOfBirth = orphan.DateOfBirth,
                    EntryDate = orphan.EntryDate,
                    ProfilePictureID = orphan.ProfilePictureID
                    // GuardianID = null // orphan.GuardianID
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orphans ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orphans OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }



            foreach (var pic in pictures)
            {
                _destDbContext.Pictures.Add(new Picture
                {
                    PictureID = pic.PictureID,
                    PictureFileName = pic.PictureFileName,
                    Caption = pic.Caption,
                    CreatedAt = pic.CreatedAt,
                    OrphanID = pic.OrphanID
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Pictures ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Pictures OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }




            foreach (var sponsor in sponsors)
            {
                _destDbContext.Sponsors.Add(new Sponsor
                {
                    SponsorID = sponsor.SponsorID,
                    FirstName = sponsor.FirstName,
                    LastName = sponsor.LastName,
                    Address = sponsor.Address,
                    City = sponsor.City,
                    State = sponsor.State,
                    ZipCode = sponsor.ZipCode,
                    Email = sponsor.Email,
                    MainPhone = sponsor.MainPhone,
                    EntryDate = sponsor.EntryDate
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Sponsors ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Sponsors OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }




            foreach (var doc in documents)
            {
                _destDbContext.Documents.Add(new Document
                {
                    DocumentID = doc.DocumentID,
                    FileName = doc.FileName,
                    CreatedAt = doc.CreatedAt,
                    OrphanID = doc.OrphanID,
                    SponsorID = doc.SponsorID
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Documents ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Documents OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }



            foreach (var os in orphanSponsors)
            {
                _destDbContext.OrphanSponsors.Add(new OrphanSponsor
                {
                    OrphanID = os.OrphanID,
                    SponsorID = os.SponsorID,
                    EntryDate = os.EntryDate
                });
            }
            _destDbContext.SaveChanges();



            foreach (var opic in orphanProfilePics)
            {
                _destDbContext.OrphanProfilePics.Add(new OrphanProfilePic
                {
                    OrphanProfilePicID = opic.OrphanProfilePicID,
                    OrphanID = opic.OrphanID,
                    PicUrl = opic.PicUrl
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.OrphanProfilePics ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.OrphanProfilePics OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }





            foreach (var academic in academics)
            {
                _destDbContext.Academics.Add(new Academic
                {
                    AcademicID = academic.AcademicID,
                    Grade = academic.Grade,
                    KCPE = academic.KCPE,
                    KCSE = academic.KCSE,
                    School = academic.School,
                    EntryDate = academic.EntryDate,
                    OrphanID = academic.OrphanID
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Academics ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Academics OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }

            foreach (var narration in narrations)
            {
                if (narration.OrphanID == null) continue;

                _destDbContext.Narrations.Add(new Narration
                {
                    NarrationID = narration.NarrationID,
                    Subject = narration.Subject,
                    Note = narration.Note,
                    EntryDate = narration.EntryDate,
                    OrphanID = narration.OrphanID,
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Narrations ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Narrations OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }

            foreach (var guardian in guardians)
            {
                _destDbContext.Guardians.Add(new Guardian
                {
                    GuardianID = guardian.GuardianID,
                    FirstName = guardian.FirstName,
                    LastName = guardian.LastName,
                    EntryDate = guardian.EntryDate,
                    Location = guardian.Location,
                });
            }

            try
            {
                _destDbContext.Database.OpenConnection();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Guardians ON");
                _destDbContext.SaveChanges();
                _destDbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Guardians OFF");
            }
            finally
            {
                _destDbContext.Database.CloseConnection();
            }



           var destOrphans = _destDbContext.Orphans.ToList();
            foreach (var destOrphan in destOrphans)
            {
                var srcOrphan = orphans.FirstOrDefault(x => x.OrphanID == destOrphan.OrphanID);
                if (srcOrphan?.GuardianID != null)
                    destOrphan.GuardianID = srcOrphan.GuardianID;
            }
            _destDbContext.SaveChanges();

        }

        //public void BuildAzureOrphanDB()
        //{
        //    // Orphans
        //    // Sponsors
        //    // Guardians
        //    // Narrations
        //    // Pictures
        //    // OrphanSponsors
        //    // Academics

        //    var inContext = _srcDbContext;
        //    var outContext = _srcDbContext;

        //    var inRecs = (from r in inContext.Academics select r);
        //    foreach (var r in inRecs)
        //    {
        //        outContext.Academics.Add(r);
        //    }
        //    outContext.Database.OpenConnection();
        //    try
        //    {
        //        outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Academics ON");
        //        outContext.SaveChanges();
        //        outContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Academics OFF");
        //    }
        //    finally
        //    {
        //        outContext.Database.CloseConnection();
        //    }
    
    }
}
