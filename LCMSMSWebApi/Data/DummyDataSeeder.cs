using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LCMSMSWebApi.Data
{
    public class DummyDataSeeder
    {
        private readonly ModelBuilder _modelBuilder;

        public DummyDataSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
    }
}
