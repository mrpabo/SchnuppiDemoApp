using SchnuppiDemoApp.Entities;
using SchnuppiDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchnuppiDemoApp.Managers
{
    public class LogDataManager
    {
        public List<Models.LogData> GetLogDatas(SearchFilter searchFilter)
        {
            using (var db = new SchnuppiEntities())
            {
                var query = db.LogDatas.AsQueryable();

                if (String.IsNullOrEmpty(searchFilter.ProductionOrder) == false)
                {
                    query = query.Where(x => x.ProductionOrderNumber == searchFilter.ProductionOrder);
                }
                if (String.IsNullOrEmpty(searchFilter.ProducerNumber) == false)
                {
                    query = query.Where(x => x.ProducerNumber.Contains(searchFilter.ProducerNumber));
                }
                if (searchFilter.DateFrom != null)
                {
                    query = query.Where(x => x.TimeStamp >= searchFilter.DateFrom);
                }
                if (searchFilter.DateTo != null)
                {
                    query = query.Where(x => x.TimeStamp <= searchFilter.DateTo);
                }

                return query.ToList().Select(x => Models.LogData.FromEntity(x)).ToList();
            }
        }


        public Models.LogData UpdateLogData(int id, Models.LogData model)
        {
            using (var db = new SchnuppiEntities())
            {
                var LogDataToUpdate = db.LogDatas.Find(id);
                if (LogDataToUpdate == null)
                {
                    return null;
                }

                LogDataToUpdate.ProductionOrderNumber = model.ProductionOrderNumber;
                LogDataToUpdate.ProducerNumber = model.ProducerNumber;
                LogDataToUpdate.SerialNumber = model.SerialNumber;
                LogDataToUpdate.TesterUserName = model.TesterUserName;
                LogDataToUpdate.MachineName = model.MachineName;

                db.SaveChanges();

                var updateLogData = db.LogDatas.Find(id);

                return Models.LogData.FromEntity(updateLogData);
            }
        }

    }
}
