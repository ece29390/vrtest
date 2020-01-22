using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRTest.Entities;

namespace VRTest.DataAccess
{
    public class NPVDataAccess : INPVDataAccess
    {
        private readonly VRTestDbContext _context;

        public NPVDataAccess(VRTestDbContext context)
        {
            _context = context;
        }
        public async Task<NPVPreviousRequest> GetNPVPreviousRequestBy(NPVPreviousRequest request)
        {
            var entity = _context
                .NPVPreviousRequests
                .Include(pr=>pr.NPVPreviousResults)
                .Where(pr => pr.CashFlowsDescription.Equals(request.CashFlowsDescription)
            && pr.IncrementRate.Equals(request.IncrementRate)
            && pr.InitialCost.Equals(request.InitialCost)
            && pr.LowerBoundDiscountRate.Equals(request.LowerBoundDiscountRate)
            && pr.UpperBoundDiscountRate.Equals(request.UpperBoundDiscountRate))
            
            .FirstOrDefault();
            return entity;

        }

        public async Task<NPVPreviousRequest> SaveNPVPreviousRequest(NPVPreviousRequest request)
        {
            var entity = await _context.NPVPreviousRequests.AddAsync(request, CancellationToken.None);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
