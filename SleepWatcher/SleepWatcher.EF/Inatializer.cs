using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Entites;

namespace SleepWatcher.EF
{
    class Inatializer :DropCreateDatabaseAlways <SleepWatcherDbContext>
    {
        
    }
}
