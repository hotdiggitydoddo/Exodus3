using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exodus3.Domain;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

namespace Exodus3.Core
{

    public class SermonItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }
        public string AudioSrcUrl { get; set; }
        public DateTime UpdatedOn { get; set; }

        [ForeignKey(typeof(SeriesItem))]
        public int SeriesItemId { get; set; }

        [ManyToOne]
        public SeriesItem SeriesItem { get; set; }
    }

    public class SeriesItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedOn { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<SermonItem> Sermons { get; set; }
    }

    public class Exodus3Database
    {
        //this will need to get read out of the phone's keyval store and persisted there after a sync
        //perhaps by way of triggeing an event that the a consumer listens to on iOS and Android

        //or store/retrive in SQLite table -- maybe a property on the account?
        //or a json file
        private DateTimeOffset _lastSync = DateTimeOffset.MinValue;
        private string _dbPath;
        private ISQLitePlatform _sqlitePlatform;

        public Exodus3Database(string dbPath, ISQLitePlatform platform)
        {
            _dbPath = dbPath;
            _sqlitePlatform = platform;

            using (var db = new SQLiteConnection(_sqlitePlatform, _dbPath))
            {
                db.CreateTable<SermonItem>();
                db.CreateTable<SeriesItem>();
            };
        }

        public Task<SermonItem> GetLastSermon()
        {
            return Task.Run(() =>
            {
                using (var db = new SQLiteConnection(_sqlitePlatform, _dbPath))
                {
                    return db.GetWithChildren<SermonItem>(2);
                }
            });
           
           
          //  return _db.Table<SermonItem>().OrderBy(x => x.CreatedOn).FirstOrDefaultAsync();
        }

        public async Task<bool> SyncCloudAndLocal()
        {
            var svc = new DataService();

            var remoteSeriesItems = await svc.GetRemoteStuff();




            using (var db = new SQLiteConnection(_sqlitePlatform, _dbPath))
            {
                var allLocalSeries = db.GetAllWithChildren<SeriesItem>();

                foreach (var remoteSeries in remoteSeriesItems)
                {
                    //insert any new series
                    if (!allLocalSeries.Any(x => x.Id == remoteSeries.Id))
                    {
                        var newSeries = new SeriesItem
                        {
                            Id = remoteSeries.Id,
                            Name = remoteSeries.Name,
                            Description = remoteSeries.Description,
                            Sermons = remoteSeries.Sermons.Select(x => new SermonItem
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Summary = x.Summary,
                                AudioSrcUrl = x.AudioSrcUrl,
                                UpdatedOn = x.UpdatedOn
                            }).ToList()
                        };

                        db.InsertWithChildren(newSeries);

                    }
                }
                return true;
            }
        }
    }
   
}
