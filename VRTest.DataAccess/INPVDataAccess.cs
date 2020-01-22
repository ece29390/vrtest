using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VRTest.Entities;

namespace VRTest.DataAccess
{
    public interface INPVDataAccess
    {
        Task<NPVPreviousRequest> GetNPVPreviousRequestBy(NPVPreviousRequest request);
        Task<NPVPreviousRequest> SaveNPVPreviousRequest(NPVPreviousRequest request);
    }
}
