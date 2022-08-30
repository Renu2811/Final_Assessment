using MVCAirLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace AirLine_TestCases.TestData
{
    

    public class AirLineTestData
    {
        public class UniversityTestData
        {
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
        }

    }
}
