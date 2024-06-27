using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchnuppiDemoApp.Models
{
    public class LogData
    {
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public string ProductionOrderNumber { get; set; }
        public string ProducerNumber { get; set; }
        public string SerialNumber { get; set; }
        public string TesterUserName { get; set; }
        public string MachineName { get; set; }

        public static LogData FromEntity(SchnuppiDemoApp.Entities.LogData entity)
        {
            return new LogData()
            {
                ID = entity.ID,
                DeviceID = entity.DeviceID,
                TimeStamp = entity.TimeStamp,
                ProductionOrderNumber = entity.ProductionOrderNumber,
                ProducerNumber = entity.ProducerNumber,
                SerialNumber = entity.SerialNumber,
                TesterUserName = entity.TesterUserName,
                MachineName = entity.MachineName
            };
        }
    }
}
