using JES.Models;

namespace JES.ViewModels
{
    public class OrganogramViewModel
    {
        public List<OrgNode> FactoryNodes { get; set; }
        public List<OrgNode> OfficeNodes { get; set; }
        public List<ShiftInfo> ShiftInfoList { get; set; }
    }
}
