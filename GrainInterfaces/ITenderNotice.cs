using GocTenderNotices.Contracts.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface ITenderNotice : IGrainWithStringKey
    {
        Task ProcessUpdate(TenderNoticeState state, ProcurementStatus status);
    }
}
