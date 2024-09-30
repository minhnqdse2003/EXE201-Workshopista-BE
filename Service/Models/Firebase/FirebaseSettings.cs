using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Firebase
{
    public class FirebaseSettings
    {
        public string ServiceAccountKeyPath { get; set; }
        public string StorageBucket { get; set; }
        public string ProjectId { get; set; }
        public string ServiceAccountId { get; set; }
    }
}
