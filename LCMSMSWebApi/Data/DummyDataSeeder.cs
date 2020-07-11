using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LCMSMSWebApi.Data
{
    public class DummyDataSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        private static Random _random = new Random();

        private string _folderName = "DummyData";

        public DummyDataSeeder(ApplicationDbContext dbContext, 
            IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public void SeedAllDummyData()
        {
            SeedGuardians();
            SeedSponsors();
            SeedNarrations();
            SeedPictures();
            // SeedOrphanPictures();
            // SeedOrphanSponsors();
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

        public void SeedNarrations()
        {
            string fileName = "narrations_mock_data.json";
            string folderPath = Path.Combine(_env.WebRootPath, _folderName);

            string filePath = Path.Combine(folderPath, fileName);

            string dummyData = System.IO.File.ReadAllText(filePath);

            var deserializedObj = JsonConvert.DeserializeObject<List<NarrationDTO>>(dummyData);

            //var orphans = _dbContext.Orphans.ToList();
            var guardians = _dbContext.Guardians.ToList();

            foreach (var obj in deserializedObj)
            {
                _dbContext.Narrations.Add(new Narration()
                {
                    Subject = obj.Subject,
                    Note = obj.Note,
                    EntryDate = obj.EntryDate,
                    GuardianID = guardians[_random.Next(guardians.Count)].GuardianID
                });
            }

            _dbContext.SaveChanges();
        }

        public void SeedPictures()
        {
            string fileName = "pictures_mock_data.json";
            string folderPath = Path.Combine(_env.WebRootPath, _folderName);

            string filePath = Path.Combine(folderPath, fileName);

            string dummyData = System.IO.File.ReadAllText(filePath);

            var deserializedObj = JsonConvert.DeserializeObject<List<PictureDTO>>(dummyData);

            foreach (var obj in deserializedObj)
            {
                _dbContext.Pictures.Add(new Picture()
                {
                    PictureUri = obj.PictureUri,
                    Caption = obj.Caption,
                    EntryDate = obj.EntryDate
                });
            }

            //var pictures = _dbContext.Pictures.ToList();

            //int index = 0;
            //foreach (var pic in pictures)
            //{
            //    pic.EntryDate = deserializedObj[index].EntryDate;
            //    EntityEntry dbEntityEntry = _dbContext.Entry(pic);
            //    dbEntityEntry.State = EntityState.Modified;
            //    index++;
            //}

            _dbContext.SaveChanges();
        }

        public void SeedOrphanPictures()
        {
            var orphans = _dbContext.Orphans.ToList();
            var pictures = _dbContext.Pictures.ToList();

            Orphan orphan;
            foreach (var picture in pictures)
            {
                orphan = orphans[_random.Next(orphans.Count)];
                _dbContext.OrphanPictures.Add(new OrphanPicture
                {
                    OrphanID = orphan.OrphanID,
                    PictureID = picture.PictureID,
                    EntryDate = GetRandomDate(2019)
                });
            }

            _dbContext.SaveChanges();
        }

        public void SeedOrphanSponsors()
        {
            var orphans = _dbContext.Orphans.ToList();
            var sponsors = _dbContext.Sponsors.ToList();

            Orphan orphan;
            foreach (var sponsor in sponsors)
            {
                orphan = orphans[_random.Next(orphans.Count)];
                _dbContext.OrphanSponsors.Add(new OrphanSponsor
                {
                    OrphanID = orphan.OrphanID,
                    SponsorID = sponsor.SponsorID,
                    EntryDate = GetRandomDate(2019)
                });
            }

            _dbContext.SaveChanges();
        }

        private static DateTime GetRandomDate(int startYear)
        {
            int year = _random.Next(startYear, DateTime.Now.Year + 1);
            int month = _random.Next(1, DateTime.Now.Month);
            int day = _random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }
    }
}
