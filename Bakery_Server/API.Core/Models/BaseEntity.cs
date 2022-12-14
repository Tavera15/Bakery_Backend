using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public string mID { get; set; }
        public DateTime mTimeEntered { get; set; }
        public DateTime mTimeModified { get; set; }
        public string name { get; set; }

        public BaseEntity()
        {
            mID = Guid.NewGuid().ToString();
            mTimeEntered = DateTime.Now;
            mTimeModified = DateTime.Now;
            name = "";
        }
    }
}
