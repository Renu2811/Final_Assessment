using MVCAirLine.Models;
using MVCAirLine.Repository;
namespace MVCAirLine.DataServices
{
    public class AirLineDataService : IDataRepository<AirViewModel>
    {
        public void Add(AirViewModel airLine)
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<AirViewModel> GetTestAirLine()
        {
            return new List<AirViewModel>()
            {
                new AirViewModel()
                {
                    AirLineId=1,
                    AirLineName="British Airways",
                    FromCity="Hyderabad",
                    ToCity="Albany",
                    Fare=198976,
                    AirLineImage=""

                },
                 new AirViewModel()
                {

                    AirLineId=2,
                    AirLineName="United Airways",
                    FromCity="Banglore",
                    ToCity="Mumbai",
                    Fare=5600,
                    AirLineImage=""
                },
                  new AirViewModel()
                {

                    AirLineId=3,
                    AirLineName="Emirates",
                    FromCity="Dubai",
                    ToCity="Chicago",
                    Fare=137876,
                    AirLineImage=""
                },
                   new AirViewModel()
                {

                    AirLineId=1,
                    AirLineName="Delta Airways",
                    FromCity="Hyderabad",
                    ToCity="Dubai",
                    Fare=298976,
                    AirLineImage=""
                },
                    new AirViewModel()
                {

                    AirLineId=1,
                    AirLineName="British Airways",
                    FromCity="Delhi",
                    ToCity="Hyderabad",
                    Fare=11989,
                    AirLineImage=""
                },
            };
        }

        public IEnumerable<AirViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
