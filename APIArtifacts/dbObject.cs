using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIArtifacts
{
    public class dbObject
    {
        private Guid _id;
        private DateTimeOffset _createdOn;

        public Guid id
        {
            get => _id == Guid.Empty ? Guid.NewGuid() : _id;
            set => _id = value;
        }
        public Guid UserId { get; set; }
        public Dealership dealership { get; set; }
        public List<Department> departments { get; set; }
        public DealershipFacility dealershipFacility { get; set; }
        public DealershipThirdParty dealershipThirdParty { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset createdOn
        {
            get => _createdOn == DateTimeOffset.MinValue ? DateTimeOffset.Now : _createdOn;
            set => _createdOn = value;
        }
    }

    public class Dealership
    {
        public string Action { get; set; }
        public DealershipDetail dealershipDetail { get; set; }
    }

    public class DealershipDetail
    {
        public string lookersId { get; set; }
        public string group { get; set; }
        public string brand { get; set; }
        public string franchiseName { get; set; }
        public string franchiseDirector { get; set; }
        public string generalManager { get; set; }
        public Address address { get; set; }
        public string dealershipEmailAddress { get; set; }
        public string weblink { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public List<string> dealershipPhotoUrl { get; set; }
        public string longitudeCoordinate { get; set; }
        public string latitudeCoordinate { get; set; }
    }

    public class Address
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string town { get; set; }
        public string county { get; set; }
        public string postcode { get; set; }
    }

    public class Department
    {
        public string departmentName { get; set; }
        public string departmentManager { get; set; }
        public string emailAddress { get; set; }
        public string telephoneNumber { get; set; }
        public DealershipOpsTime dealershipOpsTime { get; set; }
    }

    public class DealershipOpsTime
    {
        public string openingTimeMonday { get; set; }
        public string openingTimeTuesday { get; set; }
        public string openingTimeWednesday { get; set; }
        public string openingTimeThursday { get; set; }
        public string openingTimeFriday { get; set; }
        public string openingTimeSaturday { get; set; }
        public string openingTimeSunday { get; set; }
    }

    public class DealershipFacility
    {
        public string onSiteCustomerParking { get; set; }
        public string disabledCustomerParking { get; set; }
        public string newCar { get; set; }
        public string usedCar { get; set; }
        public string motability { get; set; }
        public string servicing { get; set; }
        public string partsAccessories { get; set; }
        public string cafe { get; set; }
        public string freeWifi { get; set; }
        public string toilet { get; set; }
        public string softPlayArea { get; set; }
    }

    public class DealershipThirdParty
    {
        public string ivendiId { get; set; }
        public string citnow { get; set; }
        public string whatCar { get; set; }
        public string calltrack { get; set; }
        public string judgeServicesId { get; set; }
        public string autoTraderId { get; set; }
        public string aAId { get; set; }
        public string carGurusId { get; set; }
        public string ebayId { get; set; }
        public string everythingMotoringId { get; set; }
        public string exchangeMartId { get; set; }
        public string gumtreeId { get; set; }
        public string motorsCoUkId { get; set; }
        public string pistonHeadsId { get; set; }
        public string racId { get; set; }
        public string trustedDealerId { get; set; }
        public string usedCarsNiId { get; set; }
    }
}
