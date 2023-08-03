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

        [HttpGet(Name = "GetActive")]
        public async Task<IEnumerable<TenderNoticeState>> GetActiveAsync()
        {
            return new List<TenderNoticeState>();
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