using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Data
{
    public class DummyDataSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        private static readonly Random Random = new Random();

        private readonly string _folderName = "DummyData";

        public DummyDataSeeder(ApplicationDbContext dbContext, 
            IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }



        public void SeedAllDummyData()
        {
            //SeedOrphans();
            //SeedGuardians();
            //SeedSponsors();
            //SeedNarrations();
            // SeedPictures();
            //SeedOrphanSponsors();

            // SeedNewOrphanStatus();
        }

        public void SeedNewOrphanStatus()
        {
            var orphans = _dbContext.Orphans.ToList();
            string[] statuses = new string[] { "Active", "Inactive", "Active-In School", "Active-Not In School", "Inactive-Married", "Inactive-Working", "Inactive-Deceased", "Unknown" };
            foreach (var orphan in orphans)
            {
                orphan.LCMStatus = statuses[Random.Next(statuses.Length)];
            }
            _dbContext.SaveChanges();
        }

        public void SeedOrphans()
        {
            string fileName = "orphans_mock_data.json";
            string folderPath = Path.Combine(_env.WebRootPath, _folderName);

            string filePath = Path.Combine(folderPath, fileName);

            string dummyData = System.IO.File.ReadAllText(filePath);

            var deserializedObj = JsonConvert.DeserializeObject<List<OrphanDTO>>(dummyData);

            foreach (var obj in deserializedObj)
            {
                _dbContext.Orphans.Add(new Orphan()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    EntryDate = obj.EntryDate,
                    MiddleName = obj.MiddleName,
                    Gender = obj.Gender,
                    DateOfBirth = obj.DateOfBirth,
                    LCMStatus = obj.LCMStatus,
                    ProfileNumber = obj.ProfileNumber,
                });
            }

            _dbContext.SaveChanges();
        }

        public void SeedGuardians()
        {
            string fileName = "guardians_mock_data.json";
            string folderPath = Path.Combine(_env.WebRootPath, _folderName);

            string filePath = Path.Combine(folderPath, fileName);

            string dummyData = System.IO.File.ReadAllText(filePath);

            var deserializedObj = JsonConvert.DeserializeObject<List<GuardianDTO>>(dummyData);

            foreach (var obj in deserializedObj)
            {
                _dbContext.Guardians.Add(new Guardian()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    EntryDate = obj.EntryDate,
                    Location = obj.Location
                });
            }

            _dbContext.SaveChanges();
        }


        public void SeedSponsors()
        {
            string fileName = "sponsors_mock_data.json";
            string folderPath = Path.Combine(_env.WebRootPath, _folderName);

            string filePath = Path.Combine(folderPath, fileName);

            string dummyData = System.IO.File.ReadAllText(filePath);

            var deserializedObj = JsonConvert.DeserializeObject<List<SponsorDTO>>(dummyData);

            foreach (var obj in deserializedObj)
            {
                _dbContext.Sponsors.Add(new Sponsor()
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    EntryDate = obj.EntryDate,
                    Address = obj.Address,
                    City = obj.City,
                    State = obj.State,
                    ZipCode = obj.ZipCode,
                    MainPhone = obj.MainPhone,
                    Email = obj.Email
                });
            }

            _dbContext.SaveChanges();
        }

        public async Task SeedNarrations()
        {
            string fileName = "narrations_mock_data.json";
            string folderPath = Path.Combine(_env.WebRootPath, _folderName);

            string filePath = Path.Combine(folderPath, fileName);

            string dummyData = System.IO.File.ReadAllText(filePath);

            var deserializedObj = JsonConvert.DeserializeObject<List<NarrationDTO>>(dummyData);

            var orphans = _dbContext.Orphans.ToList();
            //var guardians = _dbContext.Guardians.ToList();

            foreach (var obj in deserializedObj)
            {
                await _dbContext.Narrations.AddAsync(new Narration()
                {
                    Subject = obj.Subject,
                    Note = obj.Note,
                    EntryDate = obj.EntryDate,
                    // GuardianID = guardians[Random.Next(guardians.Count)].GuardianID
                    OrphanID = orphans[Random.Next(orphans.Count())].OrphanID
                });
            }

            await _dbContext.SaveChangesAsync();
        }

        //public void SeedPictures()
        //{
        //    string fileName = "pictures_mock_data.json";
        //    string folderPath = Path.Combine(_env.WebRootPath, _folderName);

        //    string filePath = Path.Combine(folderPath, fileName);

        //    string dummyData = System.IO.File.ReadAllText(filePath);

        //    var deserializedObj = JsonConvert.DeserializeObject<List<PictureDto>>(dummyData);

        //    foreach (var obj in deserializedObj)
        //    {
        //        _dbContext.Pictures.Add(new Picture()
        //        {
        //            PictureUri = obj.PictureUri,
        //            Caption = obj.Caption,
        //            EntryDate = obj.EntryDate
        //        });
        //    }

        //    //var pictures = _dbContext.Pictures.ToList();

        //    //int index = 0;
        //    //foreach (var pic in pictures)
        //    //{
        //    //    pic.EntryDate = deserializedObj[index].EntryDate;
        //    //    EntityEntry dbEntityEntry = _dbContext.Entry(pic);
        //    //    dbEntityEntry.State = EntityState.Modified;
        //    //    index++;
        //    //}

        //    _dbContext.SaveChanges();
        //}

        //public void SeedOrphanPictures()
        //{
        //    var orphans = _dbContext.Orphans.ToList();
        //    var pictures = _dbContext.Pictures.ToList();

        //    Orphan orphan;
        //    foreach (var picture in pictures)
        //    {
        //        orphan = orphans[Random.Next(orphans.Count)];
        //        _dbContext.OrphanPictures.Add(new OrphanPicture
        //        {
        //            OrphanID = orphan.OrphanID,
        //            ProfilePictureID = picture.ProfilePictureID,
        //            EntryDate = GetRandomDate(2019)
        //        });
        //    }

        //    _dbContext.SaveChanges();
        //}

        public void SeedOrphanSponsors()
        {
            var orphans = _dbContext.Orphans.ToList();
            var sponsors = _dbContext.Sponsors.ToList();

            Orphan orphan;
            foreach (var sponsor in sponsors)
            {
                orphan = orphans[Random.Next(orphans.Count)];
                _dbContext.OrphanSponsors.Add(new OrphanSponsor
                {
                    OrphanID = orphan.OrphanID,
                    SponsorID = sponsor.SponsorID,
                    EntryDate = GetRandomDate(2019)
                });
            }

            _dbContext.SaveChanges();
        }

        public void SeedAcademicsForeignKey()
        {
            var academics = _dbContext.Academics.ToList();
            var orphans = _dbContext.Orphans.ToList();

            int counter = 0;
            foreach (var academic in academics)
            {
                academic.OrphanID = orphans[counter].OrphanID;
                counter++;
            }

            _dbContext.SaveChanges();
        }

        private static DateTime GetRandomDate(int startYear)
        {
            int year = Random.Next(startYear, DateTime.UtcNow.Year + 1);
            int month = Random.Next(1, DateTime.UtcNow.Month);
            int day = Random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }
    }
}
