using AutoMapper;
using GocTenderNotices.Contracts.State;
using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.DTO;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenderNoticeController : ControllerBase
    {
        private readonly ILogger<TenderNoticeController> _logger;
        private readonly IClusterClient _clusterClient;
        private readonly IMapper _mapper;

        public TenderNoticeController(
            ILogger<TenderNoticeController> logger, 
            IClusterClient clusterClient,
            IMapper mapper)
        {
            _logger = logger;
            _clusterClient = clusterClient;
            _mapper = mapper;
        }

        [HttpGet("Active")]
        public async Task<IEnumerable<TenderNoticeDto>> GetActiveAsync()
        {
            var tenderNotice = _clusterClient.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Active.ToString());
            return _mapper.Map<IEnumerable<TenderNoticeDto>>(await tenderNotice.GetState());
        }
        [HttpGet("Expired")]
        public async Task<IEnumerable<TenderNoticeDto>> GetExpiredAsync()
        {
            var tenderNotice = _clusterClient.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Expired.ToString());
            return _mapper.Map<IEnumerable<TenderNoticeDto>>(await tenderNotice.GetState());
        }
        [HttpGet("Amended")]
        public async Task<IEnumerable<TenderNoticeDto>> GetAmendedAsync()
        {
            var tenderNotice = _clusterClient.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Amended.ToString());
            return _mapper.Map<IEnumerable<TenderNoticeDto>>(await tenderNotice.GetState());
        }
        [HttpGet("Awarded")]
        public async Task<IEnumerable<TenderNoticeDto>> GetAwardedAsync()
        {
            var tenderNotice = _clusterClient.GetGrain<ITenderNoticeSummary>(ProcurementStatus.Awarded.ToString());
            return _mapper.Map<IEnumerable<TenderNoticeDto>>(await tenderNotice.GetState());
        }

        [HttpPost(Name = "PostTenderNotice")]
        public async Task<ActionResult<TenderNoticeDto>> PostTenderNotice(TenderNoticeDto tenderNoticeDto)
        {
            if(tenderNoticeDto?.Id == null)
            {
                return BadRequest("Missing ID");
            }
            TenderNoticeState state;
            try
            {
                state = _mapper.Map<TenderNoticeState>(tenderNoticeDto);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var tenderNotice = _clusterClient.GetGrain<ITenderNotice>(state.FeedGuid);
            await tenderNotice.ProcessUpdate(state, state.Status);

            return tenderNoticeDto;
        }
    }
}