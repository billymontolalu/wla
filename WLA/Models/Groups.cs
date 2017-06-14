using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLA.Models
{
    public partial class GroupModel
    {
        public int ActivityGroupId { get; set; }
        public string ActivityGroup { get; set; }
        public double Effective_Working_Hours { get; set; }
        public double FTE { get; set; }
    }

    public class Groups
    {
        
        public GroupModel[] GroupModel { get; set; }
        public WLAHeader WLAHeader { get; set; }

    }
}